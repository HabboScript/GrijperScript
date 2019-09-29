using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrijperScript
{
    public enum ScriptState
    {
        none,
        selectSwitch,
        SelectCorner,
        selectTegel0,
        selectTegel1,
        grijpenAuto,
        grijpSpeler
    }
}
