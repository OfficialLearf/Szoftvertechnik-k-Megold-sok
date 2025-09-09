using Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    class TextArea : Textbox, IShapes
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double width { get; set; }
        public double height { get; set; }

        public TextArea(double x, double y, double width, double height) : base((int)x, (int)y, (int)width, (int)height)
        {
            this.X = x;
            this.Y = y;
            this.width = width;
            this.height = height;
        }

        public double GetArea()
        {
            return this.width * this.height;
        }

        public string GetType()
        {
            return "TextArea";
        }
    }
}
