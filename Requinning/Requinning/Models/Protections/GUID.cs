using dnlib.DotNet;
using Requinning.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requinning.Models.Protections
{
    public class GUID: ProtectionBaseModel
    {
        public GUID(LoggerViewModel logger, ModuleDef Module) : base("Spoof Guid", logger, Module)
        {

        }        

        public override void Execute()
        {
            Logger.Record("Spoofing guid...");

            Module.Mvid = GUIDSpoof();

            Logger.Record("Guid spoofed");
        }
    }
}
