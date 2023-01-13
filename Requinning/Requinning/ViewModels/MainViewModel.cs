using Requinning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using Requinning.Themes;

namespace Requinning.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        public MainModel MainModel { get; set; }
        public SettingsViewModel Settings { get; set; }
        public EngineViewModel Engine { get; set; }
        public LoggerViewModel Logger { get; set; }

        public Theme Theme { get; set; }

        private DelegateCommand Loader { get; set; }
        public DelegateCommand ObfuscateCommand { get; set; }
        public DelegateCommand SelectModuleCommand { get; set; }
        public DelegateCommand SelectFileCommand { get; set; }
        
        public List<string> Modules { get; set; }

        public MainViewModel()
        {
            MainModel = new MainModel();

            Logger = new LoggerViewModel();
            Settings = new SettingsViewModel();
            Engine = new EngineViewModel(Logger);

            Loader = new DelegateCommand(Load);
            ObfuscateCommand = new DelegateCommand(Engine.Obfuscate);
            SelectModuleCommand = new DelegateCommand(SelectModule);
            SelectFileCommand = new DelegateCommand(SelectFile);

            Theme = new Theme();

            Modules = new List<string>();

            Loader.Execute(null);
        }

        private void Load(object param)
        {
            Theme.Apply("Purple");

            Logger.Record("Loading version...");
            Settings.LoadVersion();
            Logger.Record($"Version '{Settings.Version}' loaded");
        }

        private void SelectFile(object param)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            Logger.Record("Selecting file to obfuscate");
            if (openFileDialog.ShowDialog() == true)
            {
                MainModel.Path = openFileDialog.FileName;
                Engine.SelectFile(MainModel.Path);
                Logger.Record($"File to obfuscate selected '{MainModel.Path}'");
            } else
            {
                if (MainModel.Path == null)
                {
                    Logger.Record("No file to obfuscate selected");
                } else
                {
                    Logger.Record($"Previous file selected '{MainModel.Path}'");
                }
            }
        }

        private void UpdateModules(string module)
        {
            if (Modules.Contains(module) == false)
            {
                Logger.Record($"Adding module '{module}'...");
                Modules.Add(module);
                Logger.Record($"Module '{module}' added");
            } else
            {
                Logger.Record($"Removing module '{module}'...");
                Modules.Remove(module);
                Logger.Record($"Module '{module}' removed");
            }
        }

        private void SelectModule(object param)
        {
            if (param != null)
            {
                UpdateModules((string)param);
            }
        }
    }
}
