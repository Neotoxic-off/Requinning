using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Requinning.ViewModels;

namespace Requinning.Models.Protections
{
    public class Ctor: ProtectionBaseModel
    {
        public Ctor(LoggerViewModel logger, ModuleDef Module) : base("Remove Ctor", logger, Module)
        {

        }

        public override void Execute()
        {
            Logger.Record("Removing .ctor...");

            foreach (MethodDef method in Module.GlobalType.Methods)
            {
                if (method.Name == ".ctor")
                {
                    Module.GlobalType.Remove(method);
                }
            }

            Logger.Record("Ctor removed");
        }
    }
}
