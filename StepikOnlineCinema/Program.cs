using System;
public class MainClass
{
    public static void Main()
    {

        var customer = readInput(out double cost);
        var ticketOffice = new TicketOffice();

        Console.WriteLine(Math.Round(ticketOffice.SumToPay(customer, cost), 2));

    }

    public static Customer readInput(out double cost)
    {
        string[] input = Console.ReadLine().Split(" ");
        Customer customer;
        int numberOfVisits = int.Parse(input[1]); //number of visits
        cost = double.Parse(input[2]); //cost of ticket
        int numberOfTiketsToBuy = int.Parse(input[3]); //number of tickets to buy

        switch (input[0]) // type of customer
        {
            case "viewer": customer = new Viewer(numberOfVisits, numberOfTiketsToBuy); break;
            case "student": customer = new Student(numberOfVisits, numberOfTiketsToBuy); break;
            case "regular": customer = new Regular(numberOfVisits, numberOfTiketsToBuy); break;
            case "pensioner": customer = new Pensioneer(numberOfVisits, numberOfTiketsToBuy); break;
            default: throw new Exception();
        }

        return customer;
    }
    public class TicketOffice
    {
        private double _sum = 0;

        public double SumToPay(Customer customer, double cost)
        {
            _calculateTotalSum(customer, cost);
            return Math.Round(_sum, 2);
        }

        private double _buyTicket(Customer customer, double cost)
        {
            customer.NumberOfVisits++;
            return Math.Round(customer.Discount() * cost, 2);
        }

        private void _calculateTotalSum(Customer customer, double cost)
        {
            int maxNumberOfTickets = customer.NumberOfTicketsToBuy + customer.NumberOfVisits;
            for (int i = customer.NumberOfVisits; i < maxNumberOfTickets; i++)
                _sum += _buyTicket(customer, cost);
        }
    }
    
    public class Customer
    {
        private int _numberOfVisits;
        private int _numberOfTicketsToBuy;

        public Customer(int numberOfVisits, int numberOfTicketsToBuy)
        {
            NumberOfTicketsToBuy = numberOfTicketsToBuy;
            NumberOfVisits = numberOfVisits;
        }


        public virtual double Discount() => Math.Round(1.0);

        public int NumberOfTicketsToBuy
        {
            get { return _numberOfTicketsToBuy; }
            set { _numberOfTicketsToBuy = value; }
        }

        public int NumberOfVisits
        {
            get { return _numberOfVisits; }
            set { _numberOfVisits = value; }
        }
    }
    public class Viewer: Customer
    {
        public Viewer(int numberOfVisits, int numberOfTicketsToBuy) : base(numberOfVisits, numberOfTicketsToBuy) 
        {
            NumberOfTicketsToBuy = numberOfTicketsToBuy;
            NumberOfVisits = numberOfVisits;
        }
                
        public override double Discount() => Math.Round(1.0);
    }

    public class Regular : Customer
    {
        public Regular(int numberOfVisits, int numberOfTicketsToBuy) : base(numberOfVisits, numberOfTicketsToBuy)
        {
            NumberOfTicketsToBuy = numberOfTicketsToBuy;
            NumberOfVisits = numberOfVisits;
        }
        public override double Discount()
        {
            int discount = (this.NumberOfVisits / 10) > 20 ? 20 : this.NumberOfVisits / 10;

            return Math.Round(1.0 - (double)discount / 100, 2); 
        }
    }
    public class Student : Customer
    {
        public Student(int numberOfVisits, int numberOfTicketsToBuy) : base(numberOfVisits, numberOfTicketsToBuy)
        {
            NumberOfTicketsToBuy = numberOfTicketsToBuy;
            NumberOfVisits = numberOfVisits;
        }
        public override double Discount()
        {
            int discount = (this.NumberOfVisits % 3) == 0 ? 50 : 0;

            return Math.Round(1.0 - (double)discount / 100, 2);
        }
    }
    public class Pensioneer : Customer
    {
        public Pensioneer(int numberOfVisits, int numberOfTicketsToBuy) : base(numberOfVisits, numberOfTicketsToBuy) 
        { 
            NumberOfTicketsToBuy = numberOfTicketsToBuy; 
            NumberOfVisits = numberOfVisits; 
        }
        public override double Discount()
        {
            int discount = (this.NumberOfVisits % 5) == 0 ? 50 : 0;

            return Math.Round(0.5 - (double)discount / 100, 2);
        }
    }
}