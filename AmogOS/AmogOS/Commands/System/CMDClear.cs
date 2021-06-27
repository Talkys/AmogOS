using System;
using System.Collections.Generic;
using System.Text;
using AmogOS.Core;
using AmogOS.Hardware;
using AmogOS.Graphics;

namespace AmogOS.Commands
{
    public class CMDClear : Command
    {
        public CMDClear()
        {
            this.Name = "CLS";
            this.Help = "Clears the screen";
        }

        public override void Execute(string line, string[] args)
        {
            TextGraphics.Clear(CLI.BackColor);
            Shell.DrawTitleBar();
            if (Shell.TitleBarVisible) { CLI.SetCursorPos(0, 2); }
            else { CLI.SetCursorPos(0, 0); }
        }
    }
}
