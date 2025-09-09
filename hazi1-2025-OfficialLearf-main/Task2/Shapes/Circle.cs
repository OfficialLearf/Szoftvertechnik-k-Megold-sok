using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    class Circle : ShapeBase
    {
        public double Radius { get; set; }

        public Circle(double x, double y, double radius) : base(x, y)
        {
            this.Radius = radius;
        }
        public override double GetArea()
        {
            return Math.PI * Math.Pow(this.Radius, 2);
        }

        public override string GetType()
        {
            return "Circle";
        }
    }
}
