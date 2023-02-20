// See https://aka.ms/new-console-template for more information
using System;
public class MainClass
{

    public static void Main()
    {
        //используйте эти массивы строк для инициализации объектов мухи и паука
        var s1 = Console.ReadLine().Split(' ');
        var s2 = Console.ReadLine().Split(' ');

        var spider = new Spider(new Position(s2));
        Fly fly = new Fly(new Position(s1));

        //Вывод ответа
        if (fly.Position != null)
        {
            Console.WriteLine("Расстояние: " + spider.Distance(fly));
            Console.WriteLine("Путь: " + spider.Path(fly));
        }
    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Position(string[] coordinates)
        {
            X = int.Parse(coordinates[0]);
            Y = int.Parse(coordinates[1]);
            Z = int.Parse(coordinates[2]);
        }
    }

    public class Spider
    {
        public Position Position { get; set; }
        public Spider(Position position) { this.Position = position; }

        public double Distance(Fly fly)
        {
            //if (fly != null) 
               return Math.Sqrt(Math.Pow(this.Position.X - fly.Position.X, 2) + Math.Pow(this.Position.Y - fly.Position.Y, 2) + Math.Pow(this.Position.Z - fly.Position.Z, 2));
            //return null;
        }
        public double Path(Fly fly)
        {
           // if (fly != null)
                return ((this.Position.Z - fly.Position.Z) + Math.Sqrt(Math.Pow(this.Position.X - fly.Position.X, 2) + Math.Pow(this.Position.Y - fly.Position.Y, 2)));
          //  else return null;
        }
    }
    public class Fly
    {
        public Position? Position { get; set; }
        public Fly(Position position) 
            {
                if (position.Z == 0) this.Position = position;
                else Console.WriteLine("Муха должна быть на полу!");
            }
    }
}
