using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using AmogOS.Graphics;
using AmogOS.Hardware;
using AmogOS.VM;

namespace AmogOS.Core
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            PMFAT.Initialize();

            Shell.Initialize();

            Runner.Initialize();
        }

        protected override void Run()
        {
            Shell.GetInput();
        }

        public static void Delay(int millis) { Cosmos.HAL.Global.PIT.Wait((uint)millis); }
    }
}
