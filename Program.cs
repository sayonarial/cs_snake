using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snake_Pro_Ver
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            Console.Title = "Super Snake Game";
            
            Game Game = new Game(90,26); //create game window

            while (true)
            {
                Game.GameLoop();
            }
            
           
        }
    }
}
