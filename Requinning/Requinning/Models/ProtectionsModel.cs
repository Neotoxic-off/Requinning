using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

using dnlib.DotNet;
using dnlib.DotNet.Emit;
using Requinning.ViewModels;

namespace Requinning.Models
{
    public class ProtectionsModel: BaseModel
    {
        private Dictionary<string, string> Renamed { get; set; }
        public IEnumerable<TypeDef> Types { get; set; }
        private Random random { get; set; }
        private ModuleDef _module;
        private LoggerViewModel Logger { get; set; }
        public ModuleDef Module
        {
            get { return _module; }
            set { SetProperty(ref _module, value); }
        }

        public ProtectionsModel(LoggerViewModel Logger)
        {
            this.Logger = Logger;

            random = new Random();
            Renamed = new Dictionary<string, string>();
        }

        private bool Blacklisted(TypeDef type)
        {
            List<bool> list = new List<bool>()
            {
                (string.IsNullOrEmpty(type.Name)),
                (type.IsRuntimeSpecialName),
                (type.IsSpecialName),
                (type.Name.StartsWith("<")),
                (type.Name.Contains("Resources")),
                (type.Name.Contains("__")),
                (type.Name == "<Module>"),
                (type.Name == "Default"),
                (type.Namespace.Length < 1)
            };

            foreach (bool status in list)
                if (status == true)
                    return (true);
            return (false);
        }

        private bool MethodBlacklisted(MethodDef method)
        {
            List<bool> list = new List<bool>()
            {
                (method.IsConstructor),
                (method.IsSpecialName),
                (method.Name.StartsWith("<")),
                (method.Name.Contains("Resources")),
                (method.Name.Contains("__")),
                (method.Name == "<Module>"),
                (method.Name == "Default"),
                (method.IsVirtual)
            };

            foreach (bool status in list)
                if (status == true)
                    return (true);
            return (false);
        }

        private bool PropertyBlacklisted(PropertyDef property)
        {
            List<bool> list = new List<bool>()
            {
                (property.IsSpecialName),
                (property.Name.StartsWith("<")),
                (property.Name.Contains("Resources")),
                (property.Name.Contains("__")),
                (property.Name == "<Module>"),
                (property.Name == "Default")
            };

            foreach (bool status in list)
                if (status == true)
                    return (true);
            return (false);
        }

        private string Encode(string data)
        {
            SHA256Managed crypt = new SHA256Managed();
            byte[] crypto = null;
            string hashed = "";

            crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(data));
            foreach (byte theByte in crypto)
            {
                hashed += $"\\re{theByte.ToString("x2")}";
            }

            return (hashed);
        }

        public string Rename(string name)
        {
            string hashed = "";
            string[] parts = null;
            bool skip = true;

            if (Renamed.ContainsKey(name) == false)
            {
                if (name.Contains(".") == true)
                {
                    parts = name.Split('.');
                    foreach (string part in parts)
                    {
                        if (skip == true)
                        {
                            skip = false;
                        } else
                        {
                            hashed += '.';
                        }
                        hashed += Encode(part);
                    }
                } else
                {
                    hashed = Encode(name);
                }

                Renamed.Add(name, hashed);
                return (hashed);
            }
            return (Renamed[name]);
        }

        public void CleanAssembly()
        {
            Logger.Record("Cleanning assembly...");

            foreach (TypeDef type in Types)
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (Blacklisted(type) == false)
                    {
                        method.Body.SimplifyBranches();
                        method.Body.OptimizeBranches();
                    }
                }
            }

            Logger.Record("Assembly cleanned");
        }

        public void ObfuscateMethods()
        {
            foreach (TypeDef type in Types)
            {
                if (Blacklisted(type) == false)
                {
                    foreach (MethodDef method in type.Methods)
                    {
                        if (MethodBlacklisted(method) == false)
                        {
                            method.Name = Rename(method.Name);
                        }
                    }
                }
            }
        }

        public void ObfuscateNamespaces()
        {
            foreach (TypeDef type in Types)
            {
                if (Blacklisted(type) == false)
                {
                    type.Namespace = Rename(type.Namespace);
                }
            }
        }

        public void ObfuscateProperties()
        {
            foreach (TypeDef type in Types)
            {
                if (Blacklisted(type) == false)
                {
                    foreach (PropertyDef propertyDef in type.Properties)
                    {
                        if (PropertyBlacklisted(propertyDef) == false)
                        {
                            propertyDef.Name = Rename(propertyDef.Name);
                        }
                    }
                }
            }
        }

        public void ObfuscateTypes()
        {
            foreach (TypeDef type in Types)
            {
                if (Blacklisted(type) == false)
                {
                    foreach (FieldDef field in type.GetFields(type.Name))
                    {
                        field.Name = Rename(field.Name);
                    }
                }
            }
        }

        public void ObfuscateParameters()
        {
            foreach (TypeDef type in Types)
            {
                if (Blacklisted(type) == false)
                {
                    foreach (MethodDef method in type.Methods)
                    {
                        if (MethodBlacklisted(method) == false)
                        {
                            foreach (Parameter param in method.Parameters)
                            {
                                param.Name = Rename(param.Name);
                            }
                        }
                    }
                }
            }
        }

        public void ObfuscateEvents()
        {
            foreach (TypeDef type in Types)
            {
                if (Blacklisted(type) == false)
                {
                    foreach (EventDef _event in type.Events)
                    {
                        _event.Name = Rename(_event.Name);
                    }
                }
            }
        }

        public void ObfuscateStrings()
        {
            foreach (TypeDef type in Types)
            {
                if (Blacklisted(type) == false)
                {
                    foreach (MethodDef method in type.Methods)
                    {
                    }
                }
            }
        }

        public void ObfuscateInt()
        {
            foreach (TypeDef type in Types)
            {
                if (Blacklisted(type) == false)
                {
                    foreach (MethodDef method in type.Methods)
                    {
                        
                    }
                }
            }
        }

        public void ObfuscateClasses()
        {
            foreach (TypeDef type in Types)
            {
                if (Blacklisted(type) == false)
                {
                    type.Name = Rename(type.Name);
                }
            }
        }

        public void RemoveCtor()
        {
            foreach (MethodDef method in Module.GlobalType.Methods)
            {
                if (method.Name == ".ctor")
                {
                    Module.GlobalType.Remove(method);
                }
            }
        }

        public void Load(ModuleDef Module)
        {
            this.Module = Module;
            this.Types = Module.GetTypes();
        }
    }
}
