using System;
using System.Collections.Generic;
using System.Text;
using AmogOS.Core;
using AmogOS.Hardware;
using AmogOS.Graphics;

namespace AmogOS.Commands
{
    public class CMDShutdown : Command
    {
        public CMDShutdown()
        { 
            this.Name = "SHUTDOWN";
            this.Help = "Turn off the computer";
        }

        public override void Execute(string line, string[] args)
        {
            Cosmos.System.Power.Shutdown();
        }
    }
}
