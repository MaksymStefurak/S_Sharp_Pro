interface IOutput
{
    void Show();
    void Show(string info);
}
interface IMath
{
    int Max();
    int Min();
    float Avg();
    bool Search(int valueToSearch);
}
interface ISort
{
    void SortAsc();
    void SortDesc();
    void SortByParam(bool isAsc);
}
class MyArray : IOutput, IMath, ISort
{
    private int[] array;

    public MyArray(int[] arr)
    {
        array = arr;
    }
    public void Show()
    {
        Console.WriteLine(string.Join(" ", array));
    }

    public void Show(string info)
    {
        Console.WriteLine(info);
        Show();
    }
    public int Max()
    {
        return array.Max();
    }

    public int Min()
    {
        return array.Min();
    }

    public float Avg()
    {
        return (float)array.Average();
    }

    public bool Search(int valueToSearch)
    {
        return array.Contains(valueToSearch);
    }
    public void SortAsc()
    {
        Array.Sort(array);
    }

    public void SortDesc()
    {
        Array.Sort(array);
        Array.Reverse(array);
    }

    public void SortByParam(bool isAsc)
    {
        if (isAsc)
            SortAsc();
        else
            SortDesc();
    }
}
class Program
{
    static void Main()
    {
        int[] testArray = { 5, 2, 9, 1, 5, 6 };
        MyArray myArray = new MyArray(testArray);

        Console.WriteLine("Initial array:");
        myArray.Show();

        Console.WriteLine("Maximum: " + myArray.Max());
        Console.WriteLine("Minimum: " + myArray.Min());
        Console.WriteLine("Average: " + myArray.Avg());
        Console.WriteLine("Search 5: " + myArray.Search(5));
        Console.WriteLine("Search 10: " + myArray.Search(10));

        Console.WriteLine("Sorting in ascending order:");
        myArray.SortAsc();
        myArray.Show();

        Console.WriteLine("Sorting in descending order:");
        myArray.SortDesc();
        myArray.Show();

        Console.WriteLine("Sorting by parameter:");
        myArray.SortByParam(true);
        myArray.Show();
    }
}
