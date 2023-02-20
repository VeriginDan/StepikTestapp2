using System;
using System.Collections.Generic;
using System.Linq;
//Напишите программу целиком.
//Не забудте подключить необходимые
//пространства имён.
public class MainClass
{
    public static void Main()
    {
        List<Panda> pandaAll = ReadInput();
        Console.WriteLine($"Total pandas count: {pandaAll.Count}");
        List<Panda> pandaMale = selectPandas(pandaAll, "male");
        List<Panda> pandaFemale = selectPandas(pandaAll, "female");

        List<pandasPair> pandasPair = new List<pandasPair>();

        int minMale, minFemale;
        double minimum;

        while (pandaMale.Count != 0 && pandaFemale.Count != 0)
        {
            double[,] distancesOfPairs = new double[pandaMale.Count, pandaFemale.Count];
            minimum = double.MaxValue;
            minMale = 0;
            minFemale = 0;
            for (int i = 0; i < pandaMale.Count; i++)
                for (int j = 0; j < pandaFemale.Count; j++)
                {
                    distancesOfPairs[i, j] = Panda.Distance(pandaMale[i], pandaFemale[j]);
                    if (minimum > distancesOfPairs[i, j])
                    {
                        minimum = distancesOfPairs[i, j];
                        minMale = i;
                        minFemale = j;
                    }
                }
            pandasPair.Add(new pandasPair(pandaMale[minMale], pandaFemale[minFemale], distancesOfPairs[minMale, minFemale]));

            pandaMale.RemoveAt(minMale);
            pandaFemale.RemoveAt(minFemale);
        }

        if (pandaMale.Count != 0)
            foreach (var panda in pandaMale) panda.Print();
        if (pandaFemale.Count != 0)
            foreach (var panda in pandaFemale) panda.Print();

        foreach (var pair in pandasPair) pair.Print();
    }

    struct pandasPair
    {
        public Panda PandaMale;
        public Panda PandaFemale;
        public double Distance;

        public pandasPair(Panda pandaMale, Panda pandaFemale, double distance)
        {
            PandaMale = new Panda(pandaMale.Position.X, pandaMale.Position.Y, pandaMale.Sex);
            PandaFemale = new Panda(pandaFemale.Position.X, pandaFemale.Position.Y, pandaFemale.Sex);
            Distance = distance;
        }

        public void Print() =>
            Console.WriteLine($"Pandas pair at distance {Distance}, male panda at X: {PandaMale.Position.X}, Y: {PandaMale.Position.Y}, female panda at X: {PandaFemale.Position.X}, Y: {PandaFemale.Position.Y}");
    }

    public static List<Panda> ReadInput()
    {
        string input;
        List<Panda> listReturn = new List<Panda>();

        while ((input = Console.ReadLine()) != "end") // cycle until input string is not end
        {
            Panda tempPanda = new Panda(input.Split(" "));
            if (IsPandaExists(listReturn, tempPanda)) continue;
            else listReturn.Add(tempPanda);
        }
        return listReturn;
    }
    public static bool IsPandaExists(List<Panda> pandaList, Panda pandaNew)
    {
        if (pandaList.Contains(pandaNew)) return true; // check about same panda
        if (pandaList.Exists(x => x == pandaNew)) return true; // check about two pandas in one position


        return false;
    }
   
    public static List<Panda> selectPandas(List<Panda> pandas, string sex)
    {
        List<Panda> arrayReturn = new List<Panda>();
        foreach (var panda in pandas)
            if (panda.Sex == sex) arrayReturn.Add(panda);
        return arrayReturn;
    }

    public class Point
    {
        private int x, y;

        public int X
        {
            get => x;
            set => x = value;
        }
        public int Y
        {
            get => y;
            set => y = value;
        }
    }

    public class Panda
    {
        private string sex;
        private Point pandasPosition;

        public Point Position
        {
            get {return pandasPosition; }
            set {pandasPosition = value; }
        }

        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        public Panda(string[] input)
        {
            Point inputPoint= new Point() {X = int.Parse(input[0]), Y = int.Parse(input[1]) };
            this.Position = inputPoint;
           //this.Position.Y = int.Parse(input[1]);
            this.Sex = input[2];
        }
        public Panda(int x, int y, string sex)
        {
            Point inputPoint = new Point() { X = x, Y = y };
            this.Position = inputPoint;
            this.Sex = sex;
        }

        public static double Distance(Panda male, Panda female)
        {
            return Math.Round(Math.Sqrt((male.Position.X - female.Position.X) * (male.Position.X - female.Position.X) +
                             (male.Position.Y - female.Position.Y) * (male.Position.Y - female.Position.Y)), 2);
        }
        public void Print() => Console.WriteLine($"Lonely {this.Sex} panda at X: {this.Position.X}, Y: {this.Position.Y}");
    }
}