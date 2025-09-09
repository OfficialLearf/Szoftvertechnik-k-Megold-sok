using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    class ShapeInventory 
    {
        List<IShapes> shapes = new List<IShapes>();

        public void AddShape(IShapes shape)
        {
            shapes.Add(shape);
        }

        public void ListShapes()
        {
            shapes.ForEach(shape => Console.WriteLine($"Tipus: {shape.GetType()}\t  Koordinata: (x: {shape.X}, y: {shape.Y}) \t Terulet: {shape.GetArea():F2}"));
        }
    }
}
