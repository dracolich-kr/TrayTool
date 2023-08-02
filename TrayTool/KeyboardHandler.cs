using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TrayTool
{
    public class KeyboardHandler : NativeWindow
    {
        const int TOGGLE_RECORDING = 1;
        private List<Command> commands = new List<Command>();

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public KeyboardHandler()
        {
            CreateHandle(new CreateParams());
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg != 0x0312)
                return;

            int cmdId = m.WParam.ToInt32();
            commands.SingleOrDefault(cmd => cmd.COMMAND_IDENTIFIER == cmdId)?.Execute();
        }

        public void Dispose()
        {
            DestroyHandle();
        }

        public void Add(Command scmd)
        {
            RegisterHotKey(this.Handle, scmd.COMMAND_IDENTIFIER, scmd.SPECIAL_KEYS, scmd.FUNCTIONAL_KEY);
            commands.Add(scmd);
        }
    }



}