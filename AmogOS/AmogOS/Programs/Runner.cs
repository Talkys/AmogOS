using System;
using System.Collections.Generic;
using System.Text;
using AmogOS.Core;
using AmogOS.Hardware;

namespace AmogOS.VM
{
    public static class Runner
    {
        public static bool IsRunning = false;

        // inicializar
        public static void Initialize()
        {
            // iniciar instruction set
            Instructions.Initialize();

            // iniciar memory
            Memory.Initialize();
            Memory.Clear();

            // iniciar cpu
            CPU.Initialize();
            CPU.Halt();
        }

        // reset
        public static void Reset(bool clearRAM)
        {
            if (clearRAM) { Memory.Clear(); }
            CPU.Reset();
            CPU.Halt();
        }

        // stop
        public static void Stop()
        {
            CPU.Halt();
        }

        // start
        public static void Start()
        {
            Reset(false);
            CPU.Continue();
            IsRunning = true;

            while (true)
            {
                CPU.Clock();

                // sair
                if (!IsRunning) { break; }
            }

            // voltar
            SystemInfo.LoadConfig(PMFAT.ConfigFile, false);
            Shell.DrawTitleBar();
            Shell.GetInput();
        }
    }
}
