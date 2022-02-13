using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Pro_Ver
{
    class Figure
    {

        public char symbol;
        public List<Pixel> PixelList = new List<Pixel>();
        // Instantiate random number generator using system-supplied value as seed.
        Random rand = new Random();

        public Figure(char _symbol) //Symbol for figure
        {
            symbol = _symbol;
        }

        public void CreateDot(Pixel p)
        {
            PixelList.Add(p);
        }
        public void CreateHorisontalLine(Pixel leftPoint, Pixel rightPoint)
        {
            int y = leftPoint.y;

            for (int x = leftPoint.x; x <= rightPoint.x; x++)
            {
                PixelList.Add(new Pixel(x,y));
            }
        }
        public void CreateLine(Pixel firstPoint, int lenght, DIRECTION direction)
        {

        }

        public void CreateVerticalLine(Pixel upPoint, Pixel downPoint)
        {
            int x = upPoint.x;
            for (int y = upPoint.y; y <= downPoint.y; y++)
            {
                PixelList.Add(new Pixel(x, y));
            }
        }

        public void CreateBox(Pixel upLeft, Pixel downRight)
        {
            int leftX = upLeft.x;
            int rightX = downRight.x;

            for (int y = upLeft.y; y <= downRight.y; y++)
            {
                CreateHorisontalLine(new Pixel(leftX,y),new Pixel(rightX,y));
            }
        }
        public void CreateBox(Pixel upLeft, int width, int height)
        {

            for (int y = upLeft.y; y <= height + upLeft.y; y++)
            {
                CreateHorisontalLine(new Pixel(upLeft.x, y), new Pixel(upLeft.x+width, y));
            }
        }


        public void CreateFrame(Pixel upLeft, Pixel downRight)
        {
            CreateHorisontalLine(upLeft, new Pixel(downRight.x, upLeft.y));
            CreateHorisontalLine(new Pixel(upLeft.x, downRight.y), downRight);
            CreateVerticalLine(upLeft, new Pixel(upLeft.x, downRight.y));
            CreateVerticalLine(new Pixel(downRight.x, upLeft.y), downRight);
        }

        public void Draw()
        {
            foreach (var p in PixelList)
            {
                p.Draw(symbol);
            }
        }

        public Pixel GetNextPixel(DIRECTION direction)
        { 
            //get last pixel in list
            Pixel lastPixel = PixelList.Last();
            switch (direction)
	        {
                case DIRECTION.UP:
                    return new Pixel(lastPixel.x,lastPixel.y - 1);
                case DIRECTION.DOWN:
                    return new Pixel(lastPixel.x,lastPixel.y + 1);
                case DIRECTION.LEFT:
                    return new Pixel(lastPixel.x - 1,lastPixel.y);
                case DIRECTION.RIGHT:
                    return new Pixel(lastPixel.x + 1,lastPixel.y);
	        }
            return new Pixel(lastPixel.x + 1,lastPixel.y);
        }

        public void RemoveFirstPixel()
        {
            PixelList[0].Clear();
            PixelList.RemoveAt(0);
        }
        public bool IsHit(Pixel hitPixel)
        {
            foreach (var potentialPixel in this.PixelList)
	        {
                if(potentialPixel.IsHit(hitPixel)) return true;
	        }
            return false;
        }

        


        public void Remove(Figure removableFigure)
        {

            foreach (var removablePixel in removableFigure.PixelList)
            {
                for (int i = 0; i < PixelList.Count; i++)
                {
                    if (PixelList[i].x == removablePixel.x && PixelList[i].y == removablePixel.y)
                    {
                        PixelList.RemoveAt(i);
                        break;
                    }                  
                }
            }

        }

        public void Remove(Pixel removablePixel)
        {

            for (int i = 0; i < PixelList.Count; i++)
            {
                if (PixelList[i].x == removablePixel.x && PixelList[i].y == removablePixel.y)
                {
                    PixelList.RemoveAt(i);
                    break;
                }
            }

        }

        public void Add(Figure figure)
        {
            foreach (var figurePixel in figure.PixelList)
            {
                CreateDot(figurePixel);
            }
        }

        public Pixel GetRandomPixel()
        {
            int randomIndex = rand.Next(PixelList.Count());
            return PixelList[randomIndex];
        }

        public bool IsHitBody(Pixel head) //only for snake
        {
            for (int i = PixelList.Count-2; i >= 0; i--)
            {
                if (head.IsHit(PixelList[i]) == true) return true;
            }
            return false;
        }
    }
}
