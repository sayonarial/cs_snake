using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Pro_Ver
{
    
    class Pixel
    {
        public int x = 0;
        public int y = 0;
        public char symbol;
        public ConsoleColor bColor;

        public Pixel(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Draw(char symbol, ConsoleColor bColor = ConsoleColor.Black)
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = bColor;
            Console.Write(symbol);
        }
        public void Clear()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(' ');
        }
        public bool IsHit(Pixel p)
        {
            if (x == p.x && y == p.y) return true;
            else return false;
        }
        public bool IsHit(Figure hitFigure)
        {
            return hitFigure.IsHit(this);
        }
    }
}
