using dnlib.DotNet;
using Requinning.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requinning.Models.Protections
{
    public class Classes: ProtectionBaseModel
    {
        public Classes(LoggerViewModel logger, ModuleDef Module) : base("Rename Classes", logger, Module)
        {

        }

        public override void Execute()
        {
            Logger.Record("Renaming classes...");

            foreach (TypeDef type in map.Types)
            {
                if (Blacklists.Type(type) == false)
                {
                    type.Name = Rename(type.Name);
                }
            }

            Logger.Record("Classes renamed");
        }
    }
}
