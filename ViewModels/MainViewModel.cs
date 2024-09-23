using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Windows;

namespace Superpad.Models
{
    public partial class MainViewModel : ObservableObject
    {
        // Observable properties 
        [ObservableProperty]
        private string fileName;

        [ObservableProperty]
        private string filePath;

        [ObservableProperty]
        private string fileContent;

        private readonly IFileService _fileService;

        // Commands for Toolbar Actions
        public RelayCommand NewCommand { get; }
        public RelayCommand OpenCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand SaveAsCommand { get; }
        public RelayCommand CutCommand { get; }
        public RelayCommand CopyCommand { get; }
        public RelayCommand PasteCommand { get; }
        public RelayCommand UndoCommand { get; }
        public RelayCommand RedoCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand HelpCommand { get; }

        // Constructor
        public MainViewModel(IFileService fileService)
        {
            // Interface initialization
            _fileService = fileService;

            // Command Initialization
            NewCommand = new RelayCommand(CreateNewFile);
            OpenCommand = new RelayCommand(OpenFile);
            SaveCommand = new RelayCommand(SaveFile);
            SaveAsCommand = new RelayCommand(SaveFileAs);
            CutCommand = new RelayCommand(CutText);
            CopyCommand = new RelayCommand(CopyText);
            PasteCommand = new RelayCommand(PasteText);
            UndoCommand = new RelayCommand(Undo);
            RedoCommand = new RelayCommand(Redo);
            PrintCommand = new RelayCommand(Print);
            HelpCommand = new RelayCommand(Help);
        }

        // Methods for Commands
        private void CreateNewFile()
        {
            _fileService.CreateFile();
            FileName = _fileService.FileName; 
            FilePath = _fileService.FilePath; 
        }

        private void OpenFile()
        {
            _fileService.OpenFile();
            FileName = _fileService.FileName; 
            FilePath = _fileService.FilePath; 
        }

        private void SaveFile()
        {
            _fileService.FileContent = FileContent;
            _fileService.SaveFile();
            FileName = _fileService.FileName;
            FilePath = _fileService.FilePath;
        }

        private void SaveFileAs()
        {
            _fileService.CreateFile();
            FileName = _fileService.FileName; 
            FilePath = _fileService.FilePath; 
        }

        private void CutText()
        {
            if (!string.IsNullOrEmpty(FileContent))
            {
                System.Windows.Clipboard.SetText(FileContent);

                FileContent = string.Empty;
            }
        }

        private void CopyText()
        {
            if (!string.IsNullOrEmpty(FileContent))
            {
                System.Windows.Clipboard.SetText(FileContent);
            }

        }

        private void PasteText()
        {
            if (System.Windows.Clipboard.ContainsText())
            {
                FileContent += System.Windows.Clipboard.GetText();
            }
        }

        private void Undo()
        {
            // Logic for Undo
        }

        private void Redo()
        {
            // Logic for Redo
        }

        private void Print()
        {
            // Logic for Print
        }

        private void Help()
        {
            // Logic for Help
        }
    }
}
