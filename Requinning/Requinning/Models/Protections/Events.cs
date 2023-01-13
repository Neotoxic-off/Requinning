using dnlib.DotNet;
using Requinning.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requinning.Models.Protections
{
    public class Events: ProtectionBaseModel
    {
        public Events(LoggerViewModel logger, ModuleDef Module) : base("Rename Events", logger, Module)
        {

        }

        public override void Execute()
        {
            Logger.Record("Renaming events...");

            foreach (TypeDef type in map.Types)
            {
                if (Blacklists.Type(type) == false)
                {
                    foreach (EventDef _event in type.Events)
                    {
                        _event.Name = Rename(_event.Name);
                    }
                }
            }

            Logger.Record("Events renamed");
        }
    }
}
