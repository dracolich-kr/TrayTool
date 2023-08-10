using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TrayTool.Command;

namespace TrayTool
{
    public class ToggleCommand : Command
    {
        private Control mControl;

        public ToggleCommand(Control icon) 
            : base(0, SpecialKey.ALT + SpecialKey.CTRL, (int)System.Windows.Forms.Keys.Q)
        {
            mControl = icon;
        }

        public override void Execute()
        {
            mControl.ContextMenuStrip.Show(Cursor.Position);
        }
    }
}
