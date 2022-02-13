using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Pro_Ver
{
    class FoodCourt : Figure
    {
        public FoodCourt(char symbol):base(symbol){

        }

        public void SpawnFood(Figure availableSpace,int amount) {

            for (int i = 0; i < amount; i++)
            {
                Food foodPoint = new Food(symbol);
                foodPoint.Spawn(availableSpace);
                PixelList.Add(new Pixel(foodPoint.x, foodPoint.y));
            }
        }
        
    }
}
