using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrayTool
{
    public abstract class Command
    {
        public static class SpecialKey
        {
            public const int NOMOD = 0x0000;
            public const int ALT = 0x0001;
            public const int CTRL = 0x0002;
            public const int SHIFT = 0x0004;
            public const int WIN = 0x0008;

            public const int WM_HOTKEY_MSG_ID = 0x0312;
        }

        public readonly int COMMAND_IDENTIFIER;
        public readonly int SPECIAL_KEYS;
        public readonly int FUNCTIONAL_KEY;

        protected Command(int commandId, int specialKeys, int functionalKey)
        {
            this.COMMAND_IDENTIFIER = commandId;
            this.SPECIAL_KEYS = specialKeys;
            this.FUNCTIONAL_KEY = functionalKey;

        }

        public abstract void Execute();
    }
}
