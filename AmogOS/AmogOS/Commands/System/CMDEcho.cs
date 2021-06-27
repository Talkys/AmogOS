using System;
using System.Collections.Generic;
using System.Text;
using AmogOS.Core;
using AmogOS.Hardware;
using AmogOS.Graphics;

namespace AmogOS.Commands
{
    public class CMDEcho : Command
    {
        public CMDEcho() 
        {
            this.Name = "ECHO";
            this.Help = "Prints a line of input";
            this.Usage = "Usage: echo [text]";
        }

        public override void Execute(string line, string[] args)
        {
            CLI.WriteLine(line.Substring(5, line.Length - 5));
        }
    }
}
