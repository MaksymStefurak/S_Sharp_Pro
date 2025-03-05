Money productPrice = new Money(25, 75, "USD");
Product product = new Product("Laptop", productPrice);

product.Display(); 

Console.WriteLine("Reducing price by 500 cents (5.00 USD)...");
product.ReducePrice(500);
product.Display(); 


public class Money
{
    public int WholePart {  get; set; }
    public int FractionalPart {  get; set; }
    public string Currrency {  get; set; }

    public Money(int wholePart, int fractionalPart, string currrency)
    {
        if (fractionalPart < 0 ||  fractionalPart >= 100)
        {
            throw new ArgumentException("Fractional part must be between 0 and 99");
        }
        WholePart = wholePart;
        FractionalPart = fractionalPart;
        Currrency = currrency;
    }
    public void Display()
    {
        Console.WriteLine($"Amount: {WholePart}.{FractionalPart:D2} {Currrency}");
    }
}

public class Product
{
    public string Name { get; set; }
    public Money Price { get; set; }
    public Product(string name, Money price)
    {
        Name = name;
        Price = price;
    }
    public void ReducePrice(int amount)
    {
        int totalCents = (Price.WholePart * 100 + Price.FractionalPart) - amount;
        if (totalCents < 0) totalCents = 0;

        Price.WholePart = totalCents / 100;
        Price.FractionalPart = totalCents % 100;
    }

    public void Display()
    {
        Console.Write($"Product: {Name}, ");
        Price.Display();
    }
}



//zad2
//MusicInstrument[] instruments =
//{
//    new Violin(),
//    new Trombone(),
//    new Ukulele(),
//    new Cello()
//};

//foreach(var instr in instruments)
//{
//    instr.Show();
//    instr.Desc();
//    instr.History();
//    instr.Sound();
//    Console.WriteLine();
//}


//public abstract class MusicInstrument
//{
//    protected string Name { get; set; }
//    protected string Description { get; set; }
//    protected string HistoryInfo { get; set; }

//    public MusicInstrument(string name, string description, string history)
//    {
//        Name = name;
//        Description = description;
//        HistoryInfo = history;
//    }

//    public void Show()
//    {
//        Console.WriteLine($"Name is: {Name}");
//    }

//    public void Desc()
//    {
//        Console.WriteLine($"Description: {Description}");
//    }

//    public void History()
//    {
//        Console.WriteLine($"History: {HistoryInfo}");
//    }

//    public abstract void Sound();

//}
//public class Violin : MusicInstrument
//{
//    public Violin() : base("Violin", "Stringed bowed instrument", "Appeared in the 16th century in Italy") { }
//    public override void Sound()
//    {
//        Console.WriteLine("The violin produces a melodic and gentle sound");
//    }
//}
//public class Trombone : MusicInstrument
//{
//    public Trombone() : base("Trombone", "Brass wind instrument", "Known since the 15th century") { }
//    public override void Sound()
//    {
//        Console.WriteLine("The trombone produces a powerful and low sound");
//    }
//}
//public class Ukulele : MusicInstrument
//{
//    public Ukulele() : base("Ukulele", "Small guitar with four strings", "Originated from Portugal, became popular in Hawaii") { }
//    public override void Sound()
//    {
//        Console.WriteLine("The ukulele produces a light and cheerful sound");
//    }
//}
//public class Cello : MusicInstrument
//{
//    public Cello() : base("Cello", "Stringed bowed instrument", "Developed in the 16th century") { }

//    public override void Sound()
//    {
//        Console.WriteLine("The cello produces a deep and rich sound");
//    }
//}
