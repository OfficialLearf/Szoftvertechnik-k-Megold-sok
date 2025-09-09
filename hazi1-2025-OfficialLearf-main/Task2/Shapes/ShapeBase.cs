using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
   public abstract class ShapeBase : IShapes {
        public double X { get; set; }
        public double Y { get; set; }

        protected ShapeBase(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
        public abstract double GetArea();
        public abstract string GetType();

    }
}
