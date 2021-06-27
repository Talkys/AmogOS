using System;
using System.Collections.Generic;
using System.Text;
using AmogOS.Core;
using AmogOS.Hardware;
using AmogOS.Graphics;

namespace AmogOS.Commands
{
    public class CMDBackColor : Command
    {
        public CMDBackColor()
        {
            this.Name = "BG";
            this.Help = "Changes the background color";
        }

        public override void Execute(string line, string[] args)
        {
            int newColor = 40;
            string colString = "";
            if (args.Length == 2)
            {
                colString = args[1];
                newColor = (int)CLI.StringToColor(args[1]);
                if (newColor != 40) { CLI.BackColor = (Color)newColor; CLI.WriteLine("Changed background color to " + colString, Color.Green); }
                else { CLI.WriteLine(colString + " is not a valid color!", Color.Red); }
            }
            else { CLI.WriteLine("Invalid arguments!", Color.Red); }
            SystemInfo.SaveConfig(PMFAT.ConfigFile);
        }
    }
}
