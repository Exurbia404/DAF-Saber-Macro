using Logging;

namespace Presentation
{
    internal static class Program
    {
        //TODO: can probably shloink this to the MainForm.cs

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Logger mainLogger = new Logger();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Settings settings = new Settings(mainLogger);
            Application.Run(new PanelForm(mainLogger, settings));
        }
    }
}