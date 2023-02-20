using System;
using System.Collections.Generic;
using System.Text;

public class MainClass
{
    public static void Main()
    {
        List<Department> company = new List<Department>(4);
        int strategyNumber = int.Parse(Console.ReadLine());

        for (int i = 0; i < 4; i++)
            company.Add(ReadInput(Console.ReadLine()));

        List<Department> companyReorginized = new List<Department>();
        foreach (var department in company)
            companyReorginized.Add(department.ReturnReorginizedDepartment(strategyNumber));

        var builder = new CompanyReportBuilder(companyReorginized);
        var director = new CompanyReportDirector(builder);
        director.Build();
        var report = builder.GetReport();
        Console.WriteLine(report);
    }
    public static Department ReadInput(string input)
    {
        var outputDepartment = new Department();
        string[] departmentAndEmployee = input.Split(":"); // input string divided in two by :
        string departmentName = departmentAndEmployee[0]; // first part goes to name of department
        outputDepartment.Name = departmentName.Split(" ")[1];
        string[] workersAndDirector = departmentAndEmployee[1].Split("+"); // second part includes workers and director divided by +
        string[] workers = workersAndDirector[0].Trim().Split(","); // first part before + is workers divided in groups by ,
        foreach (string worker in workers)
        {
            string[] employees = worker.Split("*");
            for (int i = 0; i < int.Parse(employees[0]); i++)
                outputDepartment.Employees.Add(new Employee(employees[1][..^1], int.Parse("" + employees[1][^1]), false));
        }
        string[] director = workersAndDirector[1].Trim().Split(" ");
        outputDepartment.Employees.Add(new Employee(director[2][..^1], int.Parse("" + director[2][^1]), true));                     // is Director
        return outputDepartment;
    }

    public class Department
    {
        public List<Employee> Employees { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = char.ToUpper(value[0]) + value[1..];
            }
        }
        public Department()
        {
            Employees = new List<Employee>();
        }
        public int GetNumberOfEmployees()
        {
            return Employees.Count;
        }

        public static int GetNumberOfEmployees(List<Department> company)
        {
            int companyStaffQuantity = 0;
            foreach (var department in company)
                companyStaffQuantity += department.GetNumberOfEmployees();
            return companyStaffQuantity;
        }

        public int GetTotalExpenses()
        {
            int totalExpenses = 0;
            foreach (var employee in Employees)
                totalExpenses += employee.Salary;
            return totalExpenses;
        }
        public static int GetTotalExpenses(List<Department> company)
        {
            int totalExpenses = 0;
            foreach (var department in company)
                totalExpenses += department.GetTotalExpenses();
            return totalExpenses;
        }

        public int GetTotalCoffeeExpenses()
        {
            int totalCoffeeExpenses = 0;
            foreach (var employee in Employees)
                totalCoffeeExpenses += employee.CoffeeConsumption;
            return totalCoffeeExpenses;
        }
        public static int GetTotalCoffeeExpenses(List<Department> company)
        {
            int totalCoffeeExpenses = 0;
            foreach (var department in company)
                totalCoffeeExpenses += department.GetTotalCoffeeExpenses();
            return totalCoffeeExpenses;
        }
        public int GetTotalPagesOutcome()
        {
            int totalOutcome = 0;
            foreach (var employee in Employees)
                totalOutcome += employee.ReportPages;
            return totalOutcome;
        }

        public static int GetTotalPagesOutcome(List<Department> company)
        {
            int totalOutcome = 0;
            foreach (var department in company)
                totalOutcome += department.GetTotalPagesOutcome();
            return totalOutcome;
        }

        public double GetEffectiveness()
        {
            return Math.Round(GetTotalExpenses() / (double)GetTotalPagesOutcome(), 2, MidpointRounding.AwayFromZero);
        }

        public static double GetEffectiveness(List<Department> company)
        {
            return Math.Round(GetTotalExpenses(company) / (double)GetTotalPagesOutcome(company), 2, MidpointRounding.AwayFromZero);
        }

        public Department ReturnReorginizedDepartment(int strategy)
        {
            switch (strategy)
            {
                case 1  : return FireEngineersInDepartment();
                case 2  : return PromoteAnalystInDepartment();       
                case 3  : return PromoteManagersInDepartment();
                default : return this;
            }
        }

        private Department PromoteManagersInDepartment()
        {
            Department reorginizedDepartment = new();
            List<Employee> managersToPromote = ReturnListOfSpecialists(EmployeeType.Manager);
            if (managersToPromote.Count == 0) return this;
            
            
            return reorginizedDepartment;
        }

        private Department PromoteAnalystInDepartment()
        {
            Department reorginizedDepartment = new();
            List<Employee> analystToPromoteRank1 = ReturnListOfSpecialistsByRank(EmployeeType.Analyst, 1);
            List<Employee> analystToPromoteRank2 = ReturnListOfSpecialistsByRank(EmployeeType.Analyst, 1);
            
            if (analystToPromoteRank1.Count == 0 && analystToPromoteRank2.Count == 0) return this;
            
            foreach (var person in Employees)
            {
                ;
            }
            
            return reorginizedDepartment;
        }
        private List<Employee> ReturnListOfSpecialists(EmployeeType employeeType)
        {
            List<Employee> listOfSpecialists = new();
            foreach (var person in Employees)
                if (person.EmployeeType == employeeType) listOfSpecialists.Add(person);
            return listOfSpecialists;
        }

        private List<Employee> ReturnListOfSpecialistsByRank(EmployeeType employeeType, int employeeRank)
        {
            List<Employee> listOfSpecialists = new();
            foreach (var person in Employees)
                if (person.EmployeeType == employeeType && person.EmployeeRank == employeeRank) listOfSpecialists.Add(person);
            return listOfSpecialists;
        }
        private Department FireEngineersInDepartment()
        {
            Department reorginizedDepartment = new();
            List<Employee> engineersInDepartment = ReturnListOfSpecialists(EmployeeType.Engineer);

            if (engineersInDepartment.Count == 0) return this;

            if (engineersInDepartment.Count > 1) engineersInDepartment.Sort(new Comparison<Employee>((x, y) => x.EmployeeRank.CompareTo(y.EmployeeRank)));
            int employeeNumberToFire = (int)Math.Round(Employees.Count * 0.4, 0, MidpointRounding.AwayFromZero);
            int count = 0;
            for (int i = 0; i < engineersInDepartment.Count; i++)
            {
                if (engineersInDepartment[i].IsDirector)
                {
                    reorginizedDepartment.Employees.Add(engineersInDepartment[i]);
                    continue;
                }
                if (count < employeeNumberToFire)
                {
                    count++;
                    continue;
                }
                reorginizedDepartment.Employees.Add(engineersInDepartment[i]);
            }
            return reorginizedDepartment;
        }
    public enum TypesOfReport
    {
        CommonReport,
        SchemaReport,
        StrategicInvestigationReport
    }
    public enum EmployeeType
    {
        Manager,
        Marketer,
        Engineer,
        Analyst,
    }
    public struct Employee
    {
        public int BaseSalary { get; set; }
        public int Salary 
        { 
            get
                {
                return getSalary(this);
                }
        }
        public int CoffeeConsumption { get; private set; }
        public int ReportPages { get; private set; }
        public TypesOfReport TypeOfReport { get; private set; }
        public int EmployeeRank { get; private set; }
        public EmployeeType EmployeeType { get; private set; }
        public bool IsDirector { get; private set; }

        public Employee(string employeeType, int employeerank, bool isDirector)
        {
            IsDirector = isDirector;
            EmployeeRank = employeerank;

            switch (employeeType)
            {
                case "manager":
                    CoffeeConsumption = 20;
                    BaseSalary = 50_000;
                    ReportPages = 200;
                    TypeOfReport = TypesOfReport.CommonReport;
                    EmployeeType = EmployeeType.Manager;
                    break;
                case "marketer":
                    CoffeeConsumption = 15;
                    BaseSalary = 40_000;
                    ReportPages = 150;
                    TypeOfReport = TypesOfReport.CommonReport;
                    EmployeeType = EmployeeType.Marketer;
                    break;
                case "engineer":
                    CoffeeConsumption = 5;
                    BaseSalary = 20_000;
                    ReportPages = 50;
                    TypeOfReport = TypesOfReport.SchemaReport;
                    EmployeeType = EmployeeType.Engineer;
                    break;
                case "analyst":
                    CoffeeConsumption = 50;
                    BaseSalary = 80_000;
                    ReportPages = 5;
                    TypeOfReport = TypesOfReport.StrategicInvestigationReport;
                    EmployeeType = EmployeeType.Analyst;
                    break;
                default:
                    BaseSalary = 0;
                    CoffeeConsumption = 0;
                    ReportPages = 0;
                    TypeOfReport = TypesOfReport.CommonReport;
                    EmployeeType = EmployeeType.Manager;
                    break;
            }
            
            CoffeeConsumption *= IsDirector ? 2 : 1; ;
            ReportPages *= IsDirector ? 0 : 1;
        }
        static int getSalary(Employee employee)
        {
            double salary;
            salary = employee.BaseSalary * getEmployeeRankCoefficient(employee.EmployeeRank, employee.IsDirector);
            return (int)Math.Round(salary, 0, MidpointRounding.AwayFromZero);
        }

        static double getEmployeeRankCoefficient(int employeeRank, bool isDirector)
        {
            double coefficient = 1;
            switch (isDirector)
            {
                case true: coefficient *= 1.5; break;
                case false: coefficient *= 1; break;
            }
            switch (employeeRank)
            {
                case 1: coefficient *= 1; break;
                case 2: coefficient *= 1.25; break;
                case 3: coefficient *= 1.5; break;
            }
            return (double)coefficient;
        }
    } // great thanks for report builder to Codaza https://www.youtube.com/watch?v=2ReKJaM2glI
    public class CompanyReport
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public string Footer { get; set; }
        public override string ToString()
        {
            return new StringBuilder()
                .Append(Header)
                .Append(Body)
                .Append(Footer)
                .ToString();
        }
    }
    public class CompanyReportBuilder
    {
        private CompanyReport _companyReport;
        private readonly List<Department> _company;
        public CompanyReportBuilder(List<Department> company)
        {
            _company = company;
            _companyReport = new CompanyReport();
        }
        public void BuildHeader()
        {
            _companyReport.Header =
                "Департамент     Сотрудников     Тугрики     Кофе     Страницы     Тугр./стр.\n";
            _companyReport.Header +=
                "----------------------------------------------------------------------------\n";
        }
        public void BuildBody()
        {
            _companyReport.Body = "";
            foreach (var department in _company)
            {
                _companyReport.Body +=
                    department.Name.ToString().PadRight(16);
                _companyReport.Body +=
                    department.GetNumberOfEmployees().ToString().PadRight(16);
                _companyReport.Body +=
                    department.GetTotalExpenses().ToString().PadRight(12);
                _companyReport.Body +=
                    department.GetTotalCoffeeExpenses().ToString().PadRight(9);
                _companyReport.Body +=
                    department.GetTotalPagesOutcome().ToString().PadRight(13);
                _companyReport.Body +=
                    department.GetEffectiveness().ToString();
                _companyReport.Body += "\n";
            }
        }
        public void BuildFooter()
        {
            _companyReport.Footer =
                "----------------------------------------------------------------------------\n"; ;
            _companyReport.Footer +=
                "Всего".ToString().PadRight(16);
            _companyReport.Footer +=
                Department.GetNumberOfEmployees(_company).ToString().PadRight(16);
            _companyReport.Footer +=
                Department.GetTotalExpenses(_company).ToString().PadRight(12);
            _companyReport.Footer +=
                Department.GetTotalCoffeeExpenses(_company).ToString().PadRight(9);
            _companyReport.Footer +=
                Department.GetTotalPagesOutcome(_company).ToString().PadRight(13);
            _companyReport.Footer +=
                Department.GetEffectiveness(_company).ToString();
        }
        public CompanyReport GetReport()
        {
            CompanyReport companyReport = _companyReport;
            _companyReport = new();
            return companyReport;
        }
    }
    public class CompanyReportDirector
    {
        private readonly CompanyReportBuilder _builder;
        public CompanyReportDirector(CompanyReportBuilder builder)
        {
            _builder = builder;
        }
        public void Build()
        {
            _builder.BuildHeader();
            _builder.BuildBody();
            _builder.BuildFooter();
        }
    }
}