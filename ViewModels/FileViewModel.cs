using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Diagnostics;

namespace Superpad.ViewModels
{
    public class FileViewModel : ObservableObject, IFileService
    {
        private string fileName = string.Empty;
        private string filePath = string.Empty;
        private string fileContent = string.Empty;

        // Properties for FileName and FilePath
        public string FileName
        {
            get => fileName;
            set => SetProperty(ref fileName, value);
        }

        public string FilePath
        {
            get => filePath;
            set => SetProperty(ref filePath, value);
        }

        public string FileContent
        {
            get => fileContent;
            set
            {
                SetProperty(ref fileContent, value);
            }
        }


        public void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                // Use properties to trigger UI updates
                FilePath = openFileDialog.FileName;
                FileName = Path.GetFileName(FilePath);
                FileContent = File.ReadAllText(FilePath);
            }
        }


        public void CreateFile()
        {
            Debug.WriteLine("CreateFile Pressed");
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.FileName = "NewFile.txt"; 

                if (saveFileDialog.ShowDialog() == true)
                {
                    FilePath = saveFileDialog.FileName;
                    FileName = Path.GetFileName(FilePath);

                    using (StreamWriter writer = new StreamWriter(FilePath))
                    {
                        writer.Write(FileContent);
                    }

                    MessageBox.Show("File created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        public void SaveFile()
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    using (StreamWriter writer = new StreamWriter(FilePath, false)) // 'false' to overwrite, 'true' to append
                    {
                        writer.Write(FileContent);
                    }

                    Debug.WriteLine($"Values Overwritten {(string.IsNullOrWhiteSpace(FileContent) ? "0" : FileContent)}");


                    MessageBox.Show("File saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    CreateFile();
                }
            }
            catch(Exception ex )
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }


    }
}
