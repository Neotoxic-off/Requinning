using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace Requinning.ViewModels
{
    public class EngineViewModel: BaseViewModel
    {
        private LoggerViewModel Logger { get; set; }
        private Random random = new Random();
        private string path { get; set; }

        public void SelectFile(string path)
        {
            this.path = path;
        }

        private string Rename()
        {
            int size = 128;
            int[] range = { 48, 90, 97, 122 };
            string name = "";
   
            for (int i = 0; i < size; i++)
            {
                if (random.Next(0, 1) == 0)
                {
                    name += random.Next(range[0], range[1]);
                } else
                {
                    name += random.Next(range[2], range[3]);
                }
            }

            return (name);
        }

        public void Obfuscate(object param)
        {

            ModuleDef md = ModuleDefMD.Load(path);

            Logger.Log = md.ToString();
        }

        public EngineViewModel(LoggerViewModel Logger)
        {
            this.Logger = Logger;
        }
    }
}
