using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrayTool
{
    public class Node
    {
        public String NodeName { get; init; }
        public String ExecuteFileName { get; init; }
        public String IconFileName { get; init; }

        public List<Node> ChiledNode { get; set; } = new List<Node>();

        public Node(String node_name, String file_name, String icon_file_name)
        {
            NodeName = node_name;
            ExecuteFileName = file_name;
            IconFileName = icon_file_name;
        }
    }
}
