//Напишите здесь необходимый класс
using System.Data;
using System.Drawing;
public class MainClass
{
    public static void Main()
    {

        var A = new Money("1", "р.", "00", "коп.");
        A.Print();
        var B = new Money("00", "р.", "90", "коп.");
        Money.Difference(A, B).Print();
    }
    //Напишите здесь необходимый класс

    public class Money
    {
        int Rubles;
        int Coins;

        public Money(string moneyQuantity, string moneyType)
        {
            try
            {
                if (int.Parse(moneyQuantity) < 0) throw new Exception("Не может быть отрицательным!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            switch (moneyType)
            {
                case "р": Rubles = int.Parse(moneyQuantity); break;
                case "коп.": Coins = int.Parse(moneyQuantity); this.CheckCoinsOverflow(); break;
                default: Console.WriteLine("тип денег не распознан"); break;
            }
        }

        public Money(string moneyQuantityRubles, string moneyTypeRubles, string moneyQuantityCoins, string moneyTypeCoins)
        {
            try
            {
                if (int.Parse(moneyQuantityRubles) < 0 || int.Parse(moneyQuantityCoins) < 0) throw new Exception("Не может быть отрицательным!");
                if (moneyTypeRubles == "коп.") throw new Exception("Рубли и копейки перепутаны местами!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Rubles = int.Parse(moneyQuantityRubles);
            Coins = int.Parse(moneyQuantityCoins);
            this.CheckCoinsOverflow();
        }

        void CheckCoinsOverflow()
        {
            if (this.Coins > 99)
            {
                this.Rubles += Coins / 100;
                this.Coins %= 100;
            }
            if (this.Coins < 0)
            {
                this.Rubles -= 1;
                this.Coins = 100 + this.Coins;
            }
            if (this.Rubles < 0)
            {
                this.Rubles = 0;
                this.Coins = 0;
            }
        }

        public static Money Sum(Money A, Money B)
        {
            Money result = new Money(Convert.ToString(A.Rubles + B.Rubles), "р.", Convert.ToString(A.Coins + B.Coins), "коп.");

            return result;
        }

        public static Money Difference(Money A, Money B)
        {
            Money result = new Money(Convert.ToString(A.Rubles - B.Rubles), "р.", Convert.ToString(A.Coins - B.Coins), "коп.");
            return result;
        }

        public void Print()
        {
            if (this.Rubles != 0) Console.Write($"{this.Rubles} р. ");
            Console.Write($"{this.Coins} коп.");
        }

        public void PrintTransferCost(double tax)
        {
            double amount = this.Rubles * 100 + this.Coins;
            amount *= (1 + tax);
            int amountInt = (int)Math.Round(amount, 0);

            Money result = new Money(Convert.ToString(amountInt / 100), "р.", Convert.ToString(amountInt % 100), "коп.");
            result.Print();
        }
    }
}