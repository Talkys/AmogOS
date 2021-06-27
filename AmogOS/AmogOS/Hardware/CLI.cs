using System;
using System.Collections.Generic;
using System.Text;
using AmogOS.Graphics;

namespace AmogOS.Hardware
{
    class CLI
    {
        /*Tela*/
        public static int Width { get { return 80; } }
        public static int Height { get { return 25; } }
        public static Color BackColor = Color.Black;

        /*Cursor*/
        public static int CursorX { get { return Console.CursorLeft; } set { Console.CursorLeft = value; } }
        public static int CursorY { get { return Console.CursorTop; } set { Console.CursorTop = value; } }
        public static bool CursorVisible { get { return Console.CursorVisible; } set { Console.CursorVisible = value; } }
        public static Color ForeColor = Color.White;

        /*Input do teclado*/
        public static string ReadLine() { return Console.ReadLine(); }
        public static int Read() { return Console.Read(); }
        public static ConsoleKeyInfo ReadKey(bool hide) { return Console.ReadKey(hide); }

        /*Limpar a tela*/
        public static void ForceClear(Color c)
        {
            ConsoleColor old = Console.BackgroundColor;
            Console.BackgroundColor = (ConsoleColor)c;
            Console.Clear();
            Console.BackgroundColor = old;
        }

        /*Escrever string na linha*/
        public static void Write(string txt) { Console.Write(txt); }
        public static void Write(string txt, Color fg)
        {
            Console.ForegroundColor = (ConsoleColor)fg;
            Console.Write(txt);
            Console.ForegroundColor = (ConsoleColor)ForeColor;
        }
        public static void Write(string txt, Color fg, Color bg)
        {
            Console.ForegroundColor = (ConsoleColor)fg;
            Console.BackgroundColor = (ConsoleColor)bg;
            Console.Write(txt);
            Console.ForegroundColor = (ConsoleColor)ForeColor;
            Console.BackgroundColor = (ConsoleColor)BackColor;
        }

        /*Escrever string em nova linha*/
        public static void WriteLine(string txt) { Console.WriteLine(txt); }
        public static void WriteLine(string txt, Color fg)
        {
            Console.ForegroundColor = (ConsoleColor)fg;
            Console.WriteLine(txt);
            Console.ForegroundColor = (ConsoleColor)ForeColor;
        }
        public static void WriteLine(string txt, Color fg, Color bg)
        {
            Console.ForegroundColor = (ConsoleColor)fg;
            Console.BackgroundColor = (ConsoleColor)bg;
            Console.WriteLine(txt);
            Console.ForegroundColor = (ConsoleColor)ForeColor;
            Console.BackgroundColor = (ConsoleColor)BackColor;
        }

        /*Setar a posição do cursor*/
        public static void SetCursorPos(int x, int y)
        {
            int cx = x, cy = y;
            if (x < 0) { cx = 0; }
            if (y < 0) { cy = 0; }
            if (x >= Width) { cx = Width - 1; }
            if (y >= Height) { cy = Height - 1; }
            CursorX = cx; CursorY = cy;
        }

        /*Converter nome de cor para valor*/
        public static Color StringToColor(string color)
        {
            string c = color.ToLower();
            if (c == "black")       { return Color.Black; }
            if (c == "blue")        { return Color.Blue; }
            if (c == "cyan")        { return Color.Cyan; }
            if (c == "darkblue")    { return Color.DarkBlue; }
            if (c == "darkcyan")    { return Color.DarkCyan; }
            if (c == "darkgray")    { return Color.DarkGray; }
            if (c == "darkgreen")   { return Color.DarkGreen; }
            if (c == "darkred")     { return Color.DarkRed; }
            if (c == "darkyellow")  { return Color.DarkYellow; }
            if (c == "gray")        { return Color.Gray; }
            if (c == "green")       { return Color.Green; }
            if (c == "red")         { return Color.Red; }
            if (c == "white")       { return Color.White; }
            if (c == "yellow")      { return (Color)14; }
            return (Color)40;
        }
    }
}
