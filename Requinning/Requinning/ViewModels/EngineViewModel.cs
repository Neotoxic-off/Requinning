using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using Requinning.Models;

namespace Requinning.ViewModels
{
    public class EngineViewModel: BaseViewModel
    {
        private ProtectionsModel protections { get; set; }
        private LoggerViewModel Logger { get; set; }
        private ModuleDef Module { get; set; }
        private string path { get; set; }
        private Dictionary<string, Action> Binding { get; set; }

        public EngineViewModel(LoggerViewModel Logger)
        {
            this.Logger = Logger;
            protections = new ProtectionsModel(Logger);

            Binding = new Dictionary<string, Action>()
            {
                { "Clean Assembly", protections.CleanAssembly },
                { "Obfuscate Methods", protections.ObfuscateMethods },
                { "Obfuscate Namespaces", protections.ObfuscateNamespaces },
                { "Obfuscate Classes", protections.ObfuscateClasses },
                { "Obfuscate Types", protections.ObfuscateTypes },
                { "Obfuscate Strings", protections.ObfuscateStrings },
                { "Obfuscate Parameters", protections.ObfuscateParameters },
                { "Obfuscate Properties", protections.ObfuscateProperties },
                { "Obfuscate Events", protections.ObfuscateEvents },
                { "Remove Ctor", protections.RemoveCtor },
                { "Obfuscate Int", protections.ObfuscateInt }
            };
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
                LoadTypes();
            }
        }

        private void LoadAssembly()
        {
            if (File.Exists(path) == true)
            {
                Logger.Record("Loading file assembly...");
                Module = ModuleDefMD.Load(path);
                Module.Name = protections.Rename(Module.Name);
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

        private void LoadTypes()
        {
            Logger.Record("Loading assembly types...");
            protections.Load(Module);
            Logger.Record($"{protections.Types.Count()} Assembly types loaded");
        }
    }
}
