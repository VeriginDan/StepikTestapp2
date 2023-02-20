//Напишите программу целиком

public class MainClass
{
    public static void Main()
    {
        string[] input = Console.ReadLine().Split(" ");
        var A = new Vasya(input[0], int.Parse(input[1]));
        A.Print();

        Surprise.RunMe();
    }
}
public class Vasya
{
    private string _name;
    private int _age;
    private string _ageString;

    public string Name
    {
        get { return _name; }
        set
        {
            if (value == "Василий") _name = value;
            else _name = String.Concat("Я не ", value, ", а Василий!");
        }
    }

    public int Age
    {
        get { return _age; }
        set
        {
            if (value < 0) _age = 0;
            else if (value > 122) _age = 122;
            else _age = value;
            this._ageString = AgeString(_age);
        }
    }

    protected string AgeString(int Age)
    {
        string tempString = Age.ToString();

        int caseChar = tempString[tempString.Length - 1] - '0';
        switch (caseChar)
        {
            case 0: tempString = "лет"; break;
            case 1: tempString = "год"; break;
            case 2: tempString = "года"; break;
            case 3: tempString = "года"; break;
            case 4: tempString = "года"; break;
            case 5: tempString = "лет"; break;
            case 6: tempString = "лет"; break;
            case 7: tempString = "лет"; break;
            case 8: tempString = "лет"; break;
            case 9: tempString = "лет"; break;
            default: tempString = "год"; break;
        }

        if ((this.Age > 10 && this.Age < 15) || (this.Age > 110 && this.Age < 115))
            tempString = "лет";
        return tempString;
    }

    public Vasya(string name, int age)
    {
        this.Name = name;
        this.Age = age;
    }

    public void Print()
    {
        Console.WriteLine(this.Name);
        Console.WriteLine($"Мне {this.Age} {_ageString}");
    }
}