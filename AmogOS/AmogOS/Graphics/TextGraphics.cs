using System;
using System.Collections.Generic;
using System.Text;
using AmogOS.Hardware;

namespace AmogOS.Graphics
{
    class TextGraphics
    {
        /*Setar fundo e limpar a tela*/
        public static void Clear(Color c) { CLI.BackColor = c; Console.Clear(); }

        /*Desenhar caractere em branco*/
        public static void SetPixel(int x, int y, Color c) { DrawChar(x, y, ' ', Color.Black, c); }

        /*Desenhar linha horizontal*/
        public static void DrawLineH(int x, int y, int w, char c, Color fg, Color bg)
        {
            char drawChar = c;
            if (c < 32 || c >= 127) { drawChar = ' '; }
            for (int i = 0; i < w; i++) { DrawChar(x + i, y, drawChar, fg, bg); }
        }

        /*Desenhar linha vertical*/
        public static void DrawLineV(int x, int y, int h, char c, Color fg, Color bg)
        {
            char drawChar = c;
            if (c < 32 || c >= 127) { drawChar = ' '; }
            for (int i = 0; i < h; i++) { DrawChar(x, y + i, drawChar, fg, bg); }
        }

        /*Preencher retângulo*/
        public static void FillRect(int x, int y, int w, int h, char c, Color fg, Color bg)
        {
            char drawChar = c;
            if (c < 32 || c >= 127) { drawChar = ' '; } // Trocar caractere inválido por branco

            // Preencher com caractere
            for (int i = 0; i < w * h; i++)
            {
                int xx = x + (i % w);
                int yy = y + (i / w);
                if (xx * yy < CLI.Width * CLI.Height) { DrawChar(xx, yy, drawChar, fg, bg); }
            }
        }

        /*Desenhar retângulo*/
        public static void DrawRect(int x, int y, int w, int h, char c, Color fg, Color bg)
        {
            // Trocar caractere inválido por branco
            char drawChar = c;
            if (c < 32 || c >= 127) { drawChar = ' '; }

            // Linhas horizontais
            DrawLineH(x, y, w, c, fg, bg);
            DrawLineH(x, y + h, w, c, fg, bg);

            // Linhas verticais
            DrawLineV(x, y, h, c, fg, bg);
            DrawLineV(x + w, y, h, c, fg, bg);
        }

        /*Desenhar caractere na tela*/
        public static bool DrawChar(int x, int y, char c, Color fg, Color bg)
        {
            if (x >= 0 && x < CLI.Width && y >= 0 && y < CLI.Height)
            {
                int oldX = CLI.CursorX, oldY = CLI.CursorY;
                CLI.SetCursorPos(x, y);
                CLI.Write(c.ToString(), fg, bg);
                CLI.SetCursorPos(oldX, oldY);
                return true;
            }
            else { return false; }
        }

        /*Desenhar string na tela*/
        public static void DrawString(int x, int y, string txt, Color fg, Color bg)
        {
            for (int i = 0; i < txt.Length; i++) { DrawChar(x + i, y, txt[i], fg, bg); }
        }
    }
}
