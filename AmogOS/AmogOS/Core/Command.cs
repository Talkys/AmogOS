using System;
using System.Collections.Generic;
using System.Text;

namespace AmogOS.Core
{
    public abstract class Command
    {
        /*Atributos*/
        public string Name;
        public string Help;
        public string Usage;

        /*Execução*/
        public abstract void Execute(string line, string[] args);
    }
}
