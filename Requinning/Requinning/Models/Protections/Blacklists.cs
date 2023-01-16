using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requinning.Models.Protections
{
    public class Blacklists
    {
        public static bool Type(TypeDef type)
        {
            List<bool> list = new List<bool>()
            {
                (string.IsNullOrEmpty(type.Name)),
                (type.IsRuntimeSpecialName),
                (type.IsSpecialName),
                (type.Name.StartsWith("<")),
                (type.Name.Contains("Resources")),
                (type.Name.Contains("__")),
                (type.Name == "<Module>"),
                (type.Name == "Default"),
                (type.Namespace.Length < 1)
            };

            foreach (bool status in list)
                if (status == true)
                    return (true);
            return (false);
        }

        public static bool Method(MethodDef method)
        {
            List<bool> list = new List<bool>()
            {
                (method.IsConstructor),
                (method.IsSpecialName),
                (method.Name.StartsWith("<")),
                (method.Name.Contains("Resources")),
                (method.Name.Contains("__")),
                (method.Name == "<Module>"),
                (method.Name == "Default"),
                (method.IsVirtual)
            };

            foreach (bool status in list)
                if (status == true)
                    return (true);
            return (false);
        }

        public static bool Property(PropertyDef property)
        {
            List<bool> list = new List<bool>()
            {
                (property.IsSpecialName),
                (property.Name.StartsWith("<")),
                (property.Name.Contains("Resources")),
                (property.Name.Contains("__")),
                (property.Name == "<Module>"),
                (property.Name == "Default")
            };

            foreach (bool status in list)
                if (status == true)
                    return (true);
            return (false);
        }
    }
}
