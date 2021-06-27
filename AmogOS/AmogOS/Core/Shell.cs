using System;
using System.Collections.Generic;
using System.Text;
using AmogOS.Hardware;
using AmogOS.Graphics;

namespace AmogOS.Core
{
    class Shell
    {
        /*Configuração*/
        public static Color TitleBarColor = Color.DarkBlue;
        public static Color DateTimeColor = Color.White;
        public static Color TitleColor = Color.Blue;
        public static bool TitleBarVisible = true;
        public static string TitleBarText = "AmogOS";
        public static string TitleBarTime = RTC.getDateFormatted() + " " + RTC.getTimeFormatted();

        /*Lista de comandos*/
        public static List<Command> Commands = new List<Command>();

        /*Inicializar*/
        public static void Initialize()
        {
            /*Adicionar comandos*/
            AddCommands();

            /*Ler configurações*/
            SystemInfo.LoadConfig(PMFAT.ConfigFile, true);

            /*Limpar*/ 
            DrawFresh();

            /*Mostrar informações*/
            SystemInfo.ShowInfo();
        }

        /*Preencher lista de comandos*/
        private static void AddCommands()
        {
            // sistema
            Commands.Add(new Commands.CMDClear());
            Commands.Add(new Commands.CMDEcho());
            Commands.Add(new Commands.CMDColors());
            Commands.Add(new Commands.CMDInfo());
            Commands.Add(new Commands.CMDHelp());
            Commands.Add(new Commands.CMDReboot());
            Commands.Add(new Commands.CMDShutdown());

            // configurações
            Commands.Add(new Commands.CMDForeColor());
            Commands.Add(new Commands.CMDBackColor());
            Commands.Add(new Commands.CMDTitleBarColor());
            Commands.Add(new Commands.CMDTitleColor());
            Commands.Add(new Commands.CMDTimeColor());
            Commands.Add(new Commands.CMDTitleBar());

            // sistema de arquivos
            Commands.Add(new Commands.CMDSetDir());
            Commands.Add(new Commands.CMDListDir());
            Commands.Add(new Commands.CMDMakeDir());
            Commands.Add(new Commands.CMDDelDir());
            Commands.Add(new Commands.CMDCutFile());
            Commands.Add(new Commands.CMDCopyFile());
            Commands.Add(new Commands.CMDDelFile());

            // programas
            Commands.Add(new Commands.CMDEdit());
            Commands.Add(new Commands.CMDRun());
            Commands.Add(new Commands.CMDAsm());
        }

        /*Limpar tela, setar barra de título e posicionar cursor*/
        public static void DrawFresh()
        {
            // limpar tela
            TextGraphics.Clear(CLI.BackColor);

            // Desenhar barra de título
            if (TitleBarVisible) { DrawTitleBar(); CLI.SetCursorPos(0, 2); }
            else { CLI.SetCursorPos(0, 0); }
        }

        /*Desenhar barra de título*/
        public static void DrawTitleBar()
        {
            TextGraphics.DrawLineH(0, 0, CLI.Width, ' ', Color.Black, TitleBarColor); // draw background
            DrawTime(); // desenhar hora
            TextGraphics.DrawString(CLI.Width - TitleBarText.Length, 0, TitleBarText, TitleColor, TitleBarColor); // draw title
        }

        /*Desenhar hora*/
        public static void DrawTime() { TextGraphics.DrawString(0, 0, TitleBarTime, DateTimeColor, TitleBarColor); }

        /*Aceitar comandos*/
        public static void GetInput()
        {
            /*resetar título*/
            TitleBarText = "AmogOS";

            /*Desenhar ponteiro*/
            if (PMFAT.CurrentDirectory == @"0:\") { CLI.Write("root", Color.Blue); CLI.Write(":- ", Color.White); }
            else
            {
                CLI.Write("root", Color.Blue); CLI.Write("@", Color.White);
                CLI.Write(PMFAT.CurrentDirectory.Substring(3, PMFAT.CurrentDirectory.Length - 3), Color.Yellow);
                CLI.Write(":- ", Color.White);
            }

            /*Receber input*/
            string input = CLI.ReadLine();
            ParseCommand(input);
        }

        /*Converter comando*/
        private static void ParseCommand(string line)
        {
            // Se o comando entrou
            if (line.Length > 0)
            {
                string[] args = line.Split(' ');
                bool error = true;
                for (int i = 0; i < Commands.Count; i++)
                {
                    // Validar
                    if (args[0].ToUpper() == Commands[i].Name)
                    {
                        // Executar e finalizar
                        Commands[i].Execute(line, args);
                        error = false;
                        break;
                    }
                }

                // Comando inválido
                if (error) { CLI.WriteLine("Invalid command or program!", Color.Red); }
            }

            // Continuar recebendo comandos
            DrawTitleBar();
            GetInput();
        }

        /*Pegar comando da lista*/
        public static Command GetCommand(string cmd)
        {
            for (int i = 0; i < Commands.Count; i++)
            {
                if (Commands[i].Name == cmd.ToUpper()) { return Commands[i]; }
            }
            return null;
        }
    }
}
