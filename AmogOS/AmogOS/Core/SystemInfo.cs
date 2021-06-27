using System;
using System.Collections.Generic;
using System.Text;
using AmogOS.Core;
using AmogOS.Graphics;
using AmogOS.Hardware;

namespace AmogOS.Core
{
    class SystemInfo
    {
        // Sistema
        public static string OSName = "AmogOS";
        public static double OSVersion = 1.0;
        public static string KernelVersion = "562021";

        // ram
        public static uint TotalRAM { get { return Cosmos.Core.CPU.GetAmountOfRAM(); } }

        // configurações
        public static string DefaultConfig =
        "back_col,0" + "\n" +
        "text_col,15" + "\n" +
        "titlebar_col,9" + "\n" +
        "title_col,0" + "\n" +
        "time_col,15" + "\n" +
        "titlebar_show,1" + "\n";

        // mostrar informações do sistema
        public static void ShowInfo()
        {
            CLI.WriteLine("                  #X@@@#", Color.Blue);
            CLI.WriteLine("                X#z+!;/FQQ", Color.Blue);
            CLI.WriteLine("                    !/^`  @:", Color.Blue);
            CLI.WriteLine("               Qp        `@@", Color.Blue);
            CLI.WriteLine("              o@@@@Q#Q@@@@@@,", Color.Blue);
            CLI.WriteLine("              @@@@@@@@@@@@@@y", Color.Blue);
            CLI.WriteLine("             /@@@@@@@@@@@@@@g", Color.Blue);
            CLI.WriteLine("             Q@@@@@@@@@@@@@@Q", Color.Blue);
            CLI.WriteLine("             @@@@@@@@@@@@@@@@", Color.Blue);
            CLI.WriteLine("            t@@X       !@@@@@", Color.Blue);
            CLI.WriteLine("            Q@@         Q@@@@", Color.Blue);
            CLI.WriteLine("           :@@@|        Q@@@@", Color.Blue);
            CLI.WriteLine("       `a0WDe?          @@@@Q", Color.Blue);
            CLI.WriteLine("                    Q@@@@@QD", Color.Blue);
            CLI.WriteLine(OSName, Color.Blue);
            CLI.Write("version=" + OSVersion.ToString(), Color.White);
            CLI.WriteLine("    |    user kit " + KernelVersion, Color.Gray);
            ShowRAM();
            CLI.Write("\n");
        }

        // mostrar ram
        public static void ShowRAM() { CLI.WriteLine(TotalRAM.ToString() + "MB RAM"); }

        // carregar arquivo de configuração do sistema
        public static void LoadConfig(string file, bool clear)
        {
            if (!PMFAT.FileExists(file)) { CLI.WriteLine("Could not locate configuration file!", Color.Red); }
            else
            {

                try
                {
                    string[] lines = PMFAT.ReadLines(file);

                    foreach (string line in lines)
                    {
                        string[] args = line.Split(',');

                        // ler atributos de configuração
                        if (args[0] == "back_col") { CLI.BackColor = (Color)int.Parse(args[1]); }
                        if (args[0] == "text_col") { CLI.ForeColor = (Color)int.Parse(args[1]); }
                        if (args[0] == "titlebar_col") { Shell.TitleBarColor = (Color)int.Parse(args[1]); }
                        if (args[0] == "title_col") { Shell.TitleColor = (Color)int.Parse(args[1]); }
                        if (args[0] == "time_col") { Shell.DateTimeColor = (Color)int.Parse(args[1]); }
                        if (args[0] == "titlebar_show") { Shell.TitleBarVisible = DataUtils.IntToBool(int.Parse(args[1])); }
                    }

                    // resetar tela
                    if (clear) { Shell.DrawFresh(); }
                    else { Shell.DrawTitleBar(); }
                }
                // Deu merda!
                catch (Exception ex)
                {
                    CLI.WriteLine("Error loading system configuration file!", Color.Red);
                    CLI.Write("[INTERNAL] ", Color.Red); CLI.WriteLine(ex.Message);
                }
            }
        }

        public static void SaveConfig(string file)
        {
            string[] lines = new string[6];

            // escrever atributos
            lines[0] = "back_col," + ((int)(CLI.BackColor)).ToString();
            lines[1] = "text_col," + ((int)(CLI.ForeColor)).ToString();
            lines[2] = "titlebar_col," + ((int)(Shell.TitleBarColor)).ToString();
            lines[3] = "title_col," + ((int)(Shell.TitleColor)).ToString();
            lines[4] = "time_col," + ((int)(Shell.DateTimeColor)).ToString();
            lines[5] = "titlebar_show," + (Shell.TitleBarVisible ? "1" : "0");

            // tentar salvamento
            try { PMFAT.WriteAllLines(file, lines); }
            // deu merda
            catch (Exception ex)
            {
                CLI.WriteLine("Error occured while trying to save system configuration!", Color.Red);
                CLI.Write("[INTERNAL] ", Color.Red); CLI.WriteLine(ex.Message);
            }
        }
    }
}
