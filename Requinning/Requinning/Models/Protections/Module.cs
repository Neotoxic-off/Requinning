using dnlib.DotNet;
using Requinning.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requinning.Models.Protections
{
    public class Module: ProtectionBaseModel
    {
        public Module(LoggerViewModel logger, ModuleDef Module) : base("Rename Module", logger, Module)
        {

        }

        public override void Execute()
        {
            Logger.Record("Renaming module...");

            Module.Name = Rename(Module.Name);

            Logger.Record("Module renamed");
        }
    }
}
