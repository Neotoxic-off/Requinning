using dnlib.DotNet;
using Requinning.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requinning.Models.Protections
{
    public class Fields : ProtectionBaseModel
    {
        public Fields(LoggerViewModel logger, ModuleDef Module) : base("Rename Fields", logger, Module)
        {

        }

        public override void Execute()
        {
            Logger.Record("Renaming fields...");

            foreach (TypeDef type in map.Types)
            {
                if (Blacklists.Type(type) == false)
                {
                    foreach (FieldDef field in type.GetFields(type.Name))
                    {
                        field.Name = Rename(field.Name);
                    }
                }
            }

            Logger.Record("Fields renamed");
        }
    }
}
