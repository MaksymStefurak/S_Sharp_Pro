public class Worker
{
    public string Name { get; set; }
    private decimal Salary { get; set; }
    public Worker(string name, decimal salary)
    {
        Name = name;
        Salary = salary;
    }
    public void Print()
    {
        Console.WriteLine($"{Name},{Salary}");
    }

    public static Worker operator +(Worker worker, decimal moneyForOperation)
    {
        if (moneyForOperation <= 0)
        {
            throw new ArgumentException("Сума підвищення зарплати має бути меньше нуля");
        }
        return new Worker(worker.Name, worker.Salary + moneyForOperation);
    }
    public static Worker operator -(Worker worker, decimal moneyForOperation)
    {
        if (moneyForOperation <= 0)
        {
            throw new ArgumentException("Сума зняття зарплати має бути менше нуля.");
        }

        return new Worker(worker.Name, worker.Salary - moneyForOperation);
    }
    public static bool operator ==(Worker workerSalary1, Worker workerSalary2)
    {
        if (workerSalary1 is null || workerSalary2 is null)
            return false;

        return workerSalary1.Salary == workerSalary2.Salary;
    }

    public static bool operator !=(Worker workerSalary1, Worker workerSalary2)
    {
        return !(workerSalary1 == workerSalary2);
    }

    public static bool operator >(Worker workerSalary1, Worker workerSalary2)
    {
        return workerSalary1.Salary > workerSalary2.Salary;
    }
    public static bool operator <(Worker workerSalary1, Worker workerSalary2)
    {
        return workerSalary1.Salary < workerSalary2.Salary;
    }
}

public class Town
{
    public int CountOfPeople;

    public Town(int countOfPeople)
    {
        CountOfPeople = countOfPeople;
    }
    public void Print()
    {
        Console.WriteLine($"Населення міста: {CountOfPeople}");
    }

    public static Town operator +(Town town, int newCountOfPeople)
    {
        return new Town(town.CountOfPeople + newCountOfPeople);
    }
    public static Town operator -(Town town, int newCountOfPeople)
    {
        return new Town(town.CountOfPeople - newCountOfPeople);
    }
    public static bool operator ==(Town town1, Town town2)
    {
        if (town1 is null || town2 is null)
            return false;

        return town1.CountOfPeople == town2.CountOfPeople;
    }

    public static bool operator !=(Town town1, Town town2)
    {
        return !(town1 == town2);
    }

    public static bool operator >(Town town1, Town town2)
    {
        return town1.CountOfPeople > town2.CountOfPeople;
    }
    public static bool operator <(Town town1, Town town2)
    {
        return town1.CountOfPeople < town2.CountOfPeople;
    }

}

public class CreditCard 
{
    public decimal Balance {  get; private set; }

    public CreditCard(decimal balance)
    {
        if (balance < 0)
        {
            throw new ArgumentException("Баланс не може бути від’ємним.");
        }
        Balance = balance; 
    }
    public void Print()
    {
        Console.WriteLine($"Баланс картки: {Balance} грн");
    }
    public static CreditCard operator +(CreditCard card, decimal moneyForPlus)
    {
        if (moneyForPlus <= 0)
        {
            throw new ArgumentException("Have to be more than 0");
        }
        return new CreditCard(card.Balance + moneyForPlus);
    }
    public static CreditCard operator -(CreditCard card, decimal moneyForMinus)
    {
        if (moneyForMinus <= 0)
        {
            throw new ArgumentException("Have to be more than 0");
        }
        if (card.Balance < moneyForMinus)
        {
            throw new InvalidOperationException("Недостатньо коштів на картці.");
        }
        return new CreditCard(card.Balance - moneyForMinus);
    }
    public static bool operator ==(CreditCard card1, CreditCard card2)
    {
        if (card1 is null || card2 is null)
            return false;

        return card1.Balance == card2.Balance;
    }

    public static bool operator !=(CreditCard card1, CreditCard card2)
    {
        return !(card1 == card2);
    }

    public static bool operator >(CreditCard card1, CreditCard card2)
    {
        return card1.Balance > card2.Balance;
    }
    public static bool operator <(CreditCard card1, CreditCard card2)
    {
        return card1.Balance < card2.Balance;
    }

}

class Program
{
    static void Main()
    {
        //Zad1
        //Worker worker1 = new Worker("Olya", 255m);
        //Worker worker2 = new Worker("Olya", 25m);
        //Worker worker3 = new Worker("Olya", 55m);
        //worker1 = worker1 + 50m; 
        //worker1.Print();
        //Console.WriteLine(worker3 != worker1);
        //Console.WriteLine(worker2 > worker1);

        //Zad2
        Town town = new Town(2500);
        Town town1 = new Town(3000);
        Town town2 = new Town(3000);
        town = town + 500;
        town.Print();
        town = town - 500;
        town.Print();
        Console.WriteLine(town == town1);
        Console.WriteLine(town1 > town2);
        Console.WriteLine(town1 > town);

        //zad3
        //CreditCard myCard = new CreditCard(500m);
        //CreditCard myCard2 = new CreditCard(1000m);
        //CreditCard myCard3 = new CreditCard(1500m);

        //myCard = myCard + 200m;
        //myCard.Print(); 

        //myCard = myCard - 300m; 
        //myCard.Print();
        //Console.WriteLine(myCard>myCard2);
        //Console.WriteLine(myCard2 == myCard);

    }
}
