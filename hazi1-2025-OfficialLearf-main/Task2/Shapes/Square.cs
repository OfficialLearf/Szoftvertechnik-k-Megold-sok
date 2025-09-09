using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    class Square : ShapeBase
    {
        public double Side { get; set; }

        public Square(double x, double y, double side) : base(x, y)
        {
            this.Side = side;
        }
        public override double GetArea()
        {
            return Math.Pow(this.Side, 2);
        }
        public override string GetType()
        {
            return "Square";
        }
    }
}
