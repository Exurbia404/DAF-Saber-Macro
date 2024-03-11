using System;
using System.Threading.Tasks;
using Microsoft.InformationProtection;
using Microsoft.InformationProtection.Exceptions;
using Microsoft.InformationProtection.File;
using Microsoft.InformationProtection.Protection;

namespace List_Sensitivity_Labels
{
    class Program
    {
        private const string clientId = "<application-id>";
        private const string appName = "<friendly-name>";

        static void Main(string[] args)
        {
            // Initialize Wrapper for File SDK operations.
            MIP.Initialize(MipComponent.File);

            // Create ApplicationInfo, setting the clientID from Microsoft Entra App Registration as the ApplicationId.
            ApplicationInfo appInfo = new ApplicationInfo()
            {
                ApplicationId = clientId,
                ApplicationName = appName,
                ApplicationVersion = "1.0.0"
            };

            // Instantiate the AuthDelegateImpl object, passing in AppInfo.
            AuthDelegateImplementation authDelegate = new AuthDelegateImplementation(appInfo);

            // Create MipConfiguration Object
            MipConfiguration mipConfiguration = new MipConfiguration(appInfo, "mip_data", LogLevel.Trace, false);

            // Create MipContext using Configuration
            MipContext mipContext = MIP.CreateMipContext(mipConfiguration);

            // Initialize and instantiate the File Profile.
            // Create the FileProfileSettings object.
            // Initialize file profile settings to create/use local state.
            var profileSettings = new FileProfileSettings(mipContext,
                                     CacheStorageType.OnDiskEncrypted,
                                     new ConsentDelegateImplementation());

            // Load the Profile async and wait for the result.
            var fileProfile = Task.Run(async () => await MIP.LoadFileProfileAsync(profileSettings)).Result;

            // Create a FileEngineSettings object, then use that to add an engine to the profile.
            // This pattern sets the engine ID to user1@tenant.com, then sets the identity used to create the engine.
            var engineSettings = new FileEngineSettings("user1@tenant.com", authDelegate, "", "en-US");
            engineSettings.Identity = new Identity("user1@tenant.com");

            var fileEngine = Task.Run(async () => await fileProfile.AddEngineAsync(engineSettings)).Result;

            // List sensitivity labels from fileEngine and display name and id
            foreach (var label in fileEngine.SensitivityLabels)
            {
                Console.WriteLine(string.Format("{0} : {1}", label.Name, label.Id));

                if (label.Children.Count != 0)
                {
                    foreach (var child in label.Children)
                    {
                        Console.WriteLine(string.Format("{0}{1} : {2}", "\t", child.Name, child.Id));
                    }
                }
            }

            // Application Shutdown
            // handler = null; // This will be used in later quick starts.
            fileEngine = null;
            fileProfile = null;
            mipContext.ShutDown();
            mipContext = null;
        }
    }
}
