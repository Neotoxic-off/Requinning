using dnlib.DotNet;
using Requinning.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requinning.Models.Protections
{
    public class Parameters: ProtectionBaseModel
    {
        public Parameters(LoggerViewModel logger, ModuleDef Module) : base("Rename Parameters", logger, Module)
        {

        }

        public override void Execute()
        {
            Logger.Record("Renaming parameters...");

            foreach (TypeDef type in map.Types)
            {
                if (Blacklists.Type(type) == false)
                {
                    foreach (MethodDef method in type.Methods)
                    {
                        if (Blacklists.Method(method) == false)
                        {
                            foreach (Parameter param in method.Parameters)
                            {
                                param.Name = Rename(param.Name);
                            }
                        }
                    }
                }
            }

            Logger.Record("Parameters renamed");
        }
    }
}
