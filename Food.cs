using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Pro_Ver
{
    class Food:Pixel
    {
        char foodChar;
        public Food(char foodChar,int x = 0, int y = 0) : base(x,y)
        {
            this.foodChar = foodChar;
        }
        public void Spawn(Figure availableSpace)
        {
            Pixel randPix = availableSpace.GetRandomPixel();
            x = randPix.x;
            y = randPix.y;
            Draw(foodChar);
        }
    }
}
