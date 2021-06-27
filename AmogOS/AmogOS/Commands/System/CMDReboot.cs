using System;
using System.Collections.Generic;
using System.Text;
using AmogOS.Core;
using AmogOS.Hardware;
using AmogOS.Graphics;

namespace AmogOS.Commands
{
    public class CMDReboot : Command
    {
        public CMDReboot() 
        {
            this.Name = "REBOOT";
            this.Help = "Reboots the computer";
        }

        public override void Execute(string line, string[] args)
        {
            Cosmos.System.Power.Reboot();
        }
    }
}
