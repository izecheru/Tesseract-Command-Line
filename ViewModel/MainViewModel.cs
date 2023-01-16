using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Tesseract_OCR.Core;

namespace Tesseract_OCR.ViewModel
{
    internal class MainViewModel:ObservableObject
    {
        // bill path e calea catre folderul unde sunt toate documentele scanate
        private string? _billPath;
        private bool _canStart = false;

        // save path este calea catre folderul unde se vor salva toate rezultatele
        private string? _savePath;

        // cmdproc este un proces nou creat odata cu pornirea aplicatiei care asteapta
        // ca userul sa apese pe start sa porneasca tesseractul si sa genereze toate
        // rezultatele
        Process? cmdProc;

        private List<Files> _files = new List<Files>();

        // pretty self explanatory names for variables
        private string? _currentFile;
        private int _totalNumberOfFiles=0;
        private int _processedFiles=0;

        public int ProcessedFiles { 
            get { return _processedFiles; } 
            set { _processedFiles = value; OnPropertyChanged(); } 
        }
        public string BillPath
        { 
            get { return _billPath; } 
            set { _billPath = value; OnPropertyChanged(); } 
        }
        public string SavePath
        { 
            get { return _savePath; } 
            set { _savePath = value; OnPropertyChanged(); } 
        }


        public string CurrentFile
        {
            get { return _currentFile; }
            set { _currentFile = value; OnPropertyChanged(); }
        }

        public int TotalNumberOfFiles
        {
            get { return _totalNumberOfFiles; }
            set { _totalNumberOfFiles = value; OnPropertyChanged(); }
        }
        

        
        //public ICommand SelectBillFolder_Command { get; }
        //public ICommand SelectPathFolder_Command { get; }
        public ICommand StartTesseract_Command { get; }

        public MainViewModel()
        {
            //SelectBillFolder_Command = new RelayCommand(Execute_SelectBillFolder);
            //SelectPathFolder_Command = new RelayCommand(Execute_SelectPathFolder);
            StartTesseract_Command = new RelayCommand(Execute_StartTesseract, CanExecute_StartTesseract);
        }

        private bool CanExecute_StartTesseract(object arg)
        {
            return true;
        }
        string FileNameStrip(string fileName)
        {
            string temp = "";
            int index = fileName.Length - 1;
            while (index != 0)
            {
                if (fileName[index] == '\\')
                {
                    index++;
                    break;
                }
                index--;
            }
            for (int i = index; i < fileName.Length; i++)
            {
                temp += fileName[i];
            }
            return temp;
        }

        string FileTypeStrip(string fileName)
        {
            string temp = "";
            int index = fileName.Length - 1;
            while (index != 0)
            {
                if (fileName[index] == '.')
                {
                    index++;
                    break;
                }
                index--;
            }
            for (int i = index; i < fileName.Length; i++)
            {
                temp += fileName[i];
            }
            return temp;
        }
        private void Execute_StartTesseract(object obj)
        {
            // we start the cmd process
            cmdProc = new Process();
            cmdProc.StartInfo.FileName = "cmd.exe";
            cmdProc.StartInfo.CreateNoWindow = false;
            //cmdProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            cmdProc.StartInfo.UseShellExecute = false;
            cmdProc.StartInfo.RedirectStandardOutput = false;
            cmdProc.StartInfo.RedirectStandardInput = true;
            cmdProc.StartInfo.WorkingDirectory = System.IO.Directory.GetCurrentDirectory();
            cmdProc.Start();
            //---------------
            string root = Directory.GetCurrentDirectory();
            string[] fileEntries = Directory.GetFiles(root);
            string temp;
            foreach(string fileEntry in fileEntries)
            {
                temp = FileTypeStrip(fileEntry);
                if(temp == "jpg" || temp=="png")
                {
                    TotalNumberOfFiles++;
                    _files.Add(new Files(FileNameStrip(fileEntry),FileTypeStrip(fileEntry)));
                }
            }
            Trace.WriteLine(_totalNumberOfFiles);
            for(int i=0;i<_totalNumberOfFiles;i++)
            {
                ProcessedFiles++;
                CurrentFile = _files[i]._fileName;
                cmdProc.StandardInput.WriteLine($"tesseract.exe {_files[i]._fileName} {_files[i]._fileName}_output -l ron");
            }
            cmdProc.StandardInput.Flush();
            cmdProc.StandardInput.Close();
            cmdProc.WaitForExit();
            
        }

        /* functions if we want to do something with buttons
        while(!File.Exists($"{Directory.GetCurrentDirectory()}\\{_files[i]._fileName}_output.txt"))
        {
            continue;
        }
                
        _files[i]._isFileProcessed = true;
        private void Execute_SelectPathFolder(object obj)
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            dialog.SelectedPath = "";
            dialog.UseDescriptionForTitle = true;
            dialog.ShowNewFolderButton = true;
            dialog.Description = @"Select target folder";
            dialog.ShowDialog();
            _savePath = dialog.SelectedPath;
        }

        private void Execute_SelectBillFolder(object obj)
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            dialog.SelectedPath = "";
            dialog.UseDescriptionForTitle = true;
            dialog.ShowNewFolderButton = true;
            dialog.Description = @"Select target folder";
            dialog.ShowDialog();
            _billPath = dialog.SelectedPath;
        }
        */
    }
}
