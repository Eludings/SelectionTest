using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.IO;



namespace SelectionTest
{

    /*Possible new stuff to add:
        Filter to choose which file type to randomly open from
        
  */
    public partial class MainWindow : Window
    {
        List<string> testi = new List<string>();
        string[] extensions = new[] { ".mkv", ".avi", ".mp4" };  // List of video format extensions
        public MainWindow()
        {
            
            InitializeComponent();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            //Randomly select video from the list of files searched earlier and open it in default player
            
            List<FileInfo> filutest =  GetFileList(extensions,Teksti.Text);
            string[] a =  filutest.Select(f => f.FullName).ToArray();
            string fullPath;
            Random r = new Random();
            int randomtesti = r.Next(0, a.Length);
            fullPath = Path.GetFullPath(a[randomtesti]);
            System.Diagnostics.Process.Start(fullPath);
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            // Open file search window to select folder to search for video files
            FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();
            openFileDialog1.Description = "Valitse kansio";
            openFileDialog1.ShowNewFolderButton = false;
            
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var Folderpath = openFileDialog1.SelectedPath;
                Teksti.Text = Folderpath;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        // Get and make list of directories and then go through them to and grab all files containing correct extensions for video files
        // Probably has easier and more efficient way to handle this but this seems fast enough
        public static List<FileInfo> GetFileList(string[] fileSearchPattern, string rootFolderPath)
        {
            DirectoryInfo rootDir = new DirectoryInfo(rootFolderPath);

            List<DirectoryInfo> dirList = new List<DirectoryInfo>(rootDir.GetDirectories("*", SearchOption.AllDirectories));
            dirList.Add(rootDir);

            List<FileInfo> fileList = new List<FileInfo>();

            foreach (DirectoryInfo dir in dirList)
            {
                fileList.AddRange(dir.GetFiles("*", SearchOption.TopDirectoryOnly).Where(f => fileSearchPattern.Contains(f.Extension.ToLower())).ToArray());
            }


            return fileList;
        }
        
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 mywindow = new Window1();
            mywindow.ShowDialog();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            //TODO
        }
    }
}
