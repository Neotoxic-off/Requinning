using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using Requinning.Models;

namespace Requinning.ViewModels
{
    public class EngineViewModel: BaseViewModel
    {
        private Models.Protections.Collection Collection { get; set; }
        private LoggerViewModel Logger { get; set; }
        private ModuleDef Module { get; set; }
        private string path { get; set; }
        private Dictionary<string, Action> Binding { get; set; }

        public EngineViewModel(LoggerViewModel Logger)
        {
            this.Logger = Logger;
        }

        public void SelectFile(string path)
        {
            this.path = path;
        }

        public void Obfuscate(object param)
        {
            List<string> items = (List<string>)param;

            Load();

            if (Module != null)
            {
                foreach (string item in items)
                {
                    Logger.Record($"Running {item}...");
                    Binding[item]();
                }
                Logger.Record("All protections executed");
                SaveAssembly();
            }
        }

        private void Load()
        {
            LoadAssembly();

            if (Module != null)
            {
                LoadProtections();
            }
        }

        private void LoadProtections()
        {
            Collection = new Models.Protections.Collection(Logger, Module);

            Binding = new Dictionary<string, Action>()
            {
                { Collection.Assembly.Name, Collection.Assembly.Execute },
                { Collection._Module.Name, Collection._Module.Execute },
                { Collection.Methods.Name, Collection.Methods.Execute },
                { Collection.Namespaces.Name, Collection.Namespaces.Execute },
                { Collection.Classes.Name, Collection.Classes.Execute },
                { Collection.Fields.Name, Collection.Fields.Execute },
                { Collection.Strings.Name, Collection.Strings.Execute },
                { Collection.Parameters.Name, Collection.Parameters.Execute },
                { Collection.Properties.Name, Collection.Properties.Execute },
                { Collection.Events.Name, Collection.Events.Execute },
                { Collection.Ctor.Name, Collection.Ctor.Execute },
                { Collection.Ints.Name, Collection.Ints.Execute }
            };
        }

        private void LoadAssembly()
        {
            if (File.Exists(path) == true)
            {
                Logger.Record("Loading file assembly...");
                Module = ModuleDefMD.Load(path);
                Logger.Record("File assembly loaded");
            } else
            {
                Logger.Record($"File not found '{path}'");
            }
        }

        private void SaveAssembly()
        {
            string extension = ".rqng.exe";
            string file = $"{this.path}{extension}";

            if (File.Exists(file) == true)
            {
                Logger.Record("Removing previous obfuscation...");
                File.Delete(file);
                Logger.Record("Previous obfuscation removed");
            }
            Logger.Record("Saving obfuscated file assembly...");
            Module.Write(file);
            Logger.Record("Obfuscated file assembly saved");
        }
    }
}
