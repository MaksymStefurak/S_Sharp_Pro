using System.Text;
//Zad1

Test();
////GC.Collect();
//Console.Read();
void Test()
{
Console.OutputEncoding = Encoding.UTF8;
//    ThePlay play1 = new ThePlay("Гамлет", "Вільям Шекспір", "Трагедія", 1601);
//    play1.Info();

//    ThePlay play2 = new ThePlay("Ревізор", "Микола Гоголь", "Комедія", 1836);
//    play2.Info();
ThePlay? play1 = null;
try
{
play1 = new ThePlay("Гамлет", "Вільям Шекспір", "Трагедія", 1601);
}
finally
{
play1?.Dispose();
}
}

public class ThePlay : IDisposable
{
    public string NameOfPlay { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public int Year { get; set; }

    public ThePlay(string nameOfPlay, string autor, string genre, int year)
    {
        NameOfPlay = nameOfPlay;
        Author = autor;
        Genre = genre;
        Year = year;
    }
    public void Info()
    {
        Console.WriteLine($"Назва: {NameOfPlay}, Автор: {Author}, Жанр: {Genre}, Рік: {Year}");
    }
    public void Dispose()
    {
        Console.WriteLine($"Year is deaeted: {Year}");
    }

    //    ~ThePlay()
    //    {
    //        Console.WriteLine($"The play: {NameOfPlay} is deleted");
    //    }
}

//Zad2

Test();
GC.Collect();
Console.Read();

static void Test()
{
    Console.OutputEncoding = Encoding.UTF8;
    using (Shop shop1 = new Shop("АТБ", "вул. Шевченка, 10", TypeOfShop.Food))
    {
        shop1.Info();
    }

    
    Shop shop2 = new Shop("Епіцентр", "пр. Незалежності, 25", TypeOfShop.Household);
    shop2.Info();
    shop2.Dispose(); 
}
public enum TypeOfShop
{
    Food,
    Household,
    Clothing,
    Footwear
}
public class Shop : IDisposable
{
    public string NameOfShop { get; set; }
    public string AddressOfShop { get; set; }
    public TypeOfShop TypeOfShop { get; set; }

    public Shop(string nameOfShop, string addressOfShop, TypeOfShop typeOfShop)
    {
        NameOfShop = nameOfShop;
        AddressOfShop = addressOfShop;
        TypeOfShop = typeOfShop;
    }
    public void Info()
    {
        Console.WriteLine($"Магазин: {NameOfShop}, Адреса: {AddressOfShop}, Тип: {TypeOfShop}");
    }

    public void Dispose()
    {
        Console.WriteLine($"Магазин \"{NameOfShop}\" закрито.");
        //GC.SuppressFinalize(this); 
    }
    ~Shop()
    {
        Console.WriteLine($"Об'єкт магазину \"{NameOfShop}\" знищено.");
    }
}