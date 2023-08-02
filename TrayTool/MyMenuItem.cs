using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrayTool
{
    public class MyMenuItem : ToolStripMenuItem
    {
        public String FIleName { get; set; } = String.Empty;

        public MyMenuItem(string menu_name, Image? image, EventHandler handler)
            : base(menu_name, image, handler)
        {
        }
    }
}
