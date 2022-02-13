using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Pro_Ver
{
    public enum DIRECTION
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    class Snake:Figure
    {

        DIRECTION direction = 0;
        public int length = 0;
        public Pixel head;
        int pressCounts = 0;

        public Snake(Pixel tail, int length,char symbol, DIRECTION direction = DIRECTION.RIGHT):base(symbol)
        {
            this.length = length;
            this.direction = direction;
            CreateDot(tail);
            for (int s = 0; s < length; s++)
			{
                CreateDot(GetNextPixel(direction));
			}
            
        }
        public void Move()
        {
            //check for input
            HandleControl();
            // create next point
            head = GetNextPixel(direction);

            CreateDot(head);
            //delete first in array
            RemoveTail();
        }
        public void Show()
        {
            Draw();
        }
        void HandleControl()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (direction == DIRECTION.RIGHT) break;
                        direction = DIRECTION.LEFT;
                        pressCounts++;
                        break;
                    case ConsoleKey.RightArrow:
                        if (direction == DIRECTION.LEFT) break;
                        direction = DIRECTION.RIGHT;
                        pressCounts++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (direction == DIRECTION.DOWN) break;
                        direction = DIRECTION.UP;
                        pressCounts++;
                        break;
                    case ConsoleKey.DownArrow:
                        if (direction == DIRECTION.UP) break;
                        direction = DIRECTION.DOWN;
                        pressCounts++;
                        break;
                }
            }
        }

        public bool IsHit(Figure hitFigure)
        {
            return head.IsHit(hitFigure);
        }

        public bool IsHit(Collisions collisions)
        {
            return collisions.IsHit(head);
        }

        public bool IsHitItself()
        {
            return IsHitBody(head);
        }

        void Reverse()
        {

        }
        

        void RemoveTail()
        {
            RemoveFirstPixel();
        }

        public void Add(Pixel newBodyPart)
        {
            CreateDot(newBodyPart);
        }

        public int GetPressCounts()
        {
            return pressCounts;
        }
    }
}
