using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Requinning.ViewModels;

namespace Requinning.Models.Protections
{
    public class Assembly: ProtectionBaseModel
    {
        public Assembly(LoggerViewModel logger, ModuleDef Module) : base("Clean Assembly", logger, Module)
        {

        }

        public override void Execute()
        {
            Logger.Record("Cleanning assembly...");

            foreach (TypeDef type in map.Types)
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (Blacklists.Type(type) == false)
                    {
                        method.Body.SimplifyBranches();
                        method.Body.OptimizeBranches();
                    }
                }
            }

            Logger.Record("Assembly cleanned");
        }
    }
}
