using System;
using System.Collections.Generic;
using System.Text;
using AmogOS.Core;
using AmogOS.Hardware;
using AmogOS.Graphics;

namespace AmogOS.Commands
{
    public class CMDInfo : Command
    {
        public CMDInfo()
        {
            this.Name = "INFO";
            this.Help = "Shows operating system and hardware information";
        }

        public override void Execute(string line, string[] args)
        {
            CLI.WriteLine("");
            SystemInfo.ShowInfo();
        }
    }
}
