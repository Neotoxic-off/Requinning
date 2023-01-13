using dnlib.DotNet;
using Requinning.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requinning.Models.Protections
{
    public class Properties: ProtectionBaseModel
    {
        public Properties(LoggerViewModel logger, ModuleDef Module) : base("Rename Properties", logger, Module)
        {

        }

        public override void Execute()
        {
            Logger.Record("Renaming properties...");

            foreach (TypeDef type in map.Types)
            {
                if (Blacklists.Type(type) == false)
                {
                    foreach (PropertyDef propertyDef in type.Properties)
                    {
                        if (Blacklists.Property(propertyDef) == false)
                        {
                            propertyDef.Name = Rename(propertyDef.Name);
                        }
                    }
                }
            }

            Logger.Record("Properties renamed");
        }
    }
}
