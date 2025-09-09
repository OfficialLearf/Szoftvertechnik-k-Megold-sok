namespace Shapes;

public class Program
{
    static void Main(string[] args)
    {
        ShapeInventory shapes = new ShapeInventory();

        shapes.AddShape(new Square(4, 3, 6));
        shapes.AddShape(new Circle(1, 2, 3));
        shapes.AddShape(new TextArea(3, 2, 31, 10));
        shapes.ListShapes();

        Console.ReadKey();
    }
}
