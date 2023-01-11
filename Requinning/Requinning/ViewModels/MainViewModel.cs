using Requinning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace Requinning.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        public MainModel MainModel { get; set; }
        public SettingsViewModel Settings { get; set; }
        public EngineViewModel Engine { get; set; }
        public LoggerViewModel Logger { get; set; }

        private DelegateCommand Loader { get; set; }
        public DelegateCommand ObfuscateCommand { get; set; }
        public DelegateCommand SelectFileCommand { get; set; }
        

        public MainViewModel()
        {
            MainModel = new MainModel();

            Logger = new LoggerViewModel();
            Settings = new SettingsViewModel();
            Engine = new EngineViewModel(Logger);

            Loader = new DelegateCommand(Load);
            ObfuscateCommand = new DelegateCommand(Engine.Obfuscate);
            SelectFileCommand = new DelegateCommand(SelectFile);

            Loader.Execute(null);
        }

        private void Load(object param)
        {
            Logger.Log = "Loading version...";
            Settings.LoadVersion();
            Logger.Log = $"Version '{Settings.Version}' loaded";
        }

        private void SelectFile(object param)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            Logger.Log = "Selecting file to obfuscate";
            if (openFileDialog.ShowDialog() == true)
            {
                MainModel.Path = openFileDialog.FileName;
                Engine.SelectFile(MainModel.Path);
                Logger.Log = $"File to obfuscate selected '{MainModel.Path}'";
            } else
            {
                if (MainModel.Path == null)
                {
                    Logger.Log = "No file to obfuscate selected";
                } else
                {
                    Logger.Log = $"Previous file selected '{MainModel.Path}'";
                }
            }
        }
    }
}
