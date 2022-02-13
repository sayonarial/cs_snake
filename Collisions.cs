using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Pro_Ver
{
    class Collisions
    {
        public List<Figure> CollisionList = new List<Figure>();

        public void Add(Figure newFigure)
        {
            CollisionList.Add(newFigure);
        }
        public void Remove(Figure deletedFigure)
        {
            try
            {
                CollisionList.Remove(deletedFigure);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public void Show()
        {
            foreach (var collision in CollisionList)
            {
                collision.Draw();
            }
        }

       public bool IsHit(Pixel hitPixel)
        {
            foreach (var collisionFigure in CollisionList)
            {
                if (collisionFigure.IsHit(hitPixel)) return true;
            }
            return false;
        }
        
    }
}
