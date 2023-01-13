using dnlib.DotNet;
using Requinning.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Requinning.Models.Protections
{
    public class ProtectionBaseModel: BaseModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public LoggerViewModel Logger { get; set; }

        public ModuleDef Module { get; set; }

        private Random random { get; set; }

        public Map map { get; set; }

        public ProtectionBaseModel(string name, LoggerViewModel logger, ModuleDef Module)
        {
            this.Logger = logger;
            this.Module = Module;
            Name = name;
            random = new Random();
            map = new Map()
            {
                Types = Module.GetTypes(),
                Renamed = new Dictionary<string, string>()
            };
        }

        public virtual void Execute()
        {
            // execute protection
            return;
        }

        public Guid GUIDSpoof()
        {
            string buffer = "";
            int[] ascii =
            {
                48,
                57,
                97,
                122
            };
            int[] pattern = {
                8,
                4,
                4,
                4,
                12
            };

            foreach (int patternIndex in pattern)
            {
                for (int i = 0; i < patternIndex; i++)
                {
                    buffer += (random.Next(0, 1) == 0 ? (char)random.Next(ascii[0], ascii[1]) : (char)random.Next(ascii[2], ascii[3]));
                }
                if (patternIndex != pattern[pattern.Length - 1])
                    buffer += "-";
            }

            return (Guid.Parse(buffer));
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

            if (map.Renamed.ContainsKey(name) == false)
            {
                if (name.Contains(".") == true)
                {
                    parts = name.Split('.');
                    foreach (string part in parts)
                    {
                        if (skip == true)
                        {
                            skip = false;
                        }
                        else
                        {
                            hashed += '.';
                        }
                        hashed += Encode(part);
                    }
                }
                else
                {
                    hashed = Encode(name);
                }

                map.Renamed.Add(name, hashed);
                return (hashed);
            }
            return (map.Renamed[name]);
        }
    }
}
