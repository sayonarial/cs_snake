using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Pro_Ver
{
    static public class Terminal
    {
        public static void CenteredText(string text,int windowSizeX,int height)
        {
            Console.SetCursorPosition(windowSizeX / 2 - text.Length / 2, height);
            Console.Write(text);
        }
    }
}
