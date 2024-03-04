﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Section5PoC.Presentation
{
    public partial class Form1 : Form
    {
        private string BuildOfMaterialsFolder = @"U:\Data\SaberWiP\2_Users\designs\BSA\Boms";

        private List<string> folderNames;
        private List<string> folderPaths;

        private Extractor extractor;
        private WCSPP_Convertor convertor;

        private static List<Wire> extractedWires;
        private static List<Component> extractedComponents;
        private static List<Bundle> extractedBundles;

        public Form1()
        {
            extractor = new Extractor();

            InitializeComponent();
            //GetImmediateSubfolders(BuildOfMaterialsFolder, out folderNames, out folderPaths);
            AddNamesToListBox();
        }

        private void AddNamesToListBox()
        {
            foreach(string name in folderNames)
            {
                schematicsListBox.Items.Add(name);
            }
        }

        static void GetImmediateSubfolders(string folderPath, out List<string> folderNames, out List<string> folderPaths)
        {
            folderNames = new List<string>();
            folderPaths = new List<string>();

            try
            {
                // Get all immediate subfolders
                string[] subfolders = Directory.GetDirectories(folderPath);

                foreach (string subfolder in subfolders)
                {
                    // Extract folder name and path
                    string folderName = Path.GetFileName(subfolder);

                    // Add to lists
                    folderPaths.Add(subfolder);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
       

        private void schematicsListBox_DoubleClick_1(object sender, EventArgs e)
        {
            // Get the selected index
            int selectedIndex = schematicsListBox.SelectedIndex;

            if (selectedIndex >= 0 && selectedIndex < folderPaths.Count)
            {
                // Get the corresponding folder path
                string selectedFolderPath = folderPaths[selectedIndex];

                // Search for the latest .txt file containing "_DSI" in the selected folder
                string[] txtFiles = Directory.GetFiles(selectedFolderPath, "*_DSI*.txt");

                if (txtFiles.Length > 0)
                {
                    // Sort files by creation time and get the latest one
                    string latestTxtFile = txtFiles.OrderByDescending(f => new FileInfo(f).CreationTime).First();

                    // Do something with the latest .txt file, for example, display its path
                    MessageBox.Show($"Latest .txt file in {selectedFolderPath} is: {latestTxtFile}");
                    string fileName = Path.GetFileNameWithoutExtension(latestTxtFile).Replace("_DSI", "");
                    ExtractAndOpenExcel(fileName);
                }
                else
                {
                    MessageBox.Show($"No matching .txt files found in {selectedFolderPath}");
                }
            }
        }

        private void ExtractAndOpenExcel(string textFilePath)
        {
            try
            {
                extractedWires = extractor.ExtractWiresFromFile(textFilePath);
                extractedComponents = extractor.ExtractComponentsFromFile(textFilePath);
                extractedBundles = extractor.ExtractBundlesFromFile(textFilePath);

                convertor = new WCSPP_Convertor(extractedWires, extractedComponents);

                convertor.ConvertListToWCSPPExcelFile(extractedWires, extractedComponents, extractedBundles);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
