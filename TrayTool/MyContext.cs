using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrayTool
{
    internal class MyContext : ApplicationContext
    {
        private NotifyIcon mTrayIcon = new NotifyIcon();
        private Control? mControl = null;

        private KeyboardHandler keyboardHandler;
        public MyContext()
        {
            keyboardHandler = new KeyboardHandler();

            // Initialize Tray Icon
            mTrayIcon.ContextMenuStrip = new ContextMenuStrip();
            mTrayIcon.Icon = new Icon("default.ico");
            mTrayIcon.Visible = true;
            Bitmap? image = null;

            try
            {
                //image = new Bitmap("default.ico");
            }
            catch 
            {

            }

            MyMenuItem temp = new MyMenuItem("Test", image, FileExecute);
            temp.FIleName = "EEE.bat";
            mTrayIcon.ContextMenuStrip.Items.Add(temp);
            mTrayIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Exit", null, Exit));

            mControl = new Control();
            mControl.ContextMenuStrip = mTrayIcon.ContextMenuStrip;
            mControl.CreateControl();

            keyboardHandler.Add(new ToggleCommand(mControl));

        }

        public void Exit(object? sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            mTrayIcon.Visible = false;

            Application.Exit();
        }

        public void FileExecute(object? sender, EventArgs e)
        {
            MyMenuItem? item = sender as MyMenuItem;
            if (item == null)
                return;

            if (false == File.Exists(item.FIleName))
                return;

            var process = Process.Start(item.FIleName);
        }

    }
}
