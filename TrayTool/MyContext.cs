using System.Diagnostics;
using YamlDotNet.RepresentationModel;

namespace TrayTool
{
    internal class MyContext : ApplicationContext
    {
        private NotifyIcon mTrayIcon = new NotifyIcon();
        private Control mControl = new Control();

        private List<Node> mNodes = new List<Node>();
        private KeyboardHandler mKeyboardHandler = new KeyboardHandler();

        public MyContext()
        {
            Load();

            // Initialize Tray Icon
            mTrayIcon.ContextMenuStrip = new ContextMenuStrip();
            mTrayIcon.Icon = new Icon("default.ico");
            mTrayIcon.Visible = true;



            foreach(var node in mNodes)
            {
                var temp = Create(node);
                mTrayIcon.ContextMenuStrip.Items.Add(temp);
            }

            mTrayIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Exit", null, Exit));

            mControl.ContextMenuStrip = mTrayIcon.ContextMenuStrip;
            mControl.CreateControl();

            mKeyboardHandler.Add(new ToggleCommand(mControl));

        }

        public void Load()
        {
            var reader = File.OpenText("config.yaml");
            var yaml = new YamlStream();
            yaml.Load(reader);

            var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

            var roots = mapping.Children;
            if (roots == null)
                return;

            foreach (YamlSequenceNode root in roots.Values)
            {
                foreach (var node in root.Children)
                {
                    Node temp = LoadChild(node);
                    mNodes.Add(temp);
                }
            }
        }

        private Node LoadChild(YamlNode param)
        {
            String node_name = param[new YamlScalarNode("Name")].ToString();
            String exec_file_name = String.Empty;
            try
            {
                exec_file_name = param[new YamlScalarNode("ExecuteFileName")].ToString();
            }
            catch (Exception)
            {
            }

            String icon_file_name = param[new YamlScalarNode("IconFileName")].ToString();
            Node temp = new Node(node_name, exec_file_name, icon_file_name);

            try
            {
                var child_node = param[new YamlScalarNode("ChiledNodes")] as YamlSequenceNode;
                if (child_node == null)
                    return temp;
                
                var ddd = child_node as YamlSequenceNode;
                foreach (var node in ddd.Children)
                {
                    temp.ChiledNode.Add(LoadChild(node));
                }

            }
            catch (Exception)
            {
            }

            return temp;
        }

        private MyMenuItem Create(Node node)
        {
            Bitmap? image = null;

            if (node.IconFileName != string.Empty)
                image = new Bitmap(node.IconFileName);
            else
                image = null;

            MyMenuItem menu = new MyMenuItem(node.NodeName, image, FileExecute);
            menu.FIleName = node.ExecuteFileName;

            foreach(var chiled in node.ChiledNode)
            {
                menu.DropDownItems.Add(Create(chiled));
            }

            return menu;
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

            if (item.FIleName == String.Empty)
                return;

            if (false == File.Exists(item.FIleName))
                return;

            var process = Process.Start(item.FIleName);
        }

    }
}
