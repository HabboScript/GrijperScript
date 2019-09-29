using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrijperScript
{
    public class Config
    {
        public int SwitchId { get; set; }
        public Location Corner { get; set; } = new Location();
        public int PlayerTile0 { get; set; }
        public int PlayerTile1 { get; set; }
        public bool Hit { get; set; } = false;

    }
}
