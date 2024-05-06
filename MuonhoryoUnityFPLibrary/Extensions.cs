using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuonhoryoLibrary.Unity.COM
{
    public static class Extensions
    {
        public static void UnparsedModuleActivityChanging(this object module,bool activity)
        {
            if(module is IActiveModule parsedMod)
                parsedMod.IsActive=activity;
        }
    }
}
