public class Calculator
{
    public double Add(double a, double b) => a + b;
    public double Subtract(double a, double b) => a - b;
    public double Multiply(double a, double b) => a * b;
    public double Divide(double a, double b)
    {
        if (b == 0)
            throw new DivideByZeroException("Cannot divide by zero.");
        return a / b;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Calculator calc = new Calculator();

        Console.WriteLine("Enter first number:");
        double num1 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter an operation (+, -, *, /):");
        string operation = Console.ReadLine()!;

        Console.WriteLine("Enter second number:");
        double num2 = Convert.ToDouble(Console.ReadLine());

        double result = 0;

        try
        {
            switch (operation)
            {
                case "+":
                    result = calc.Add(num1, num2);
                    break;
                case "-":
                    result = calc.Subtract(num1, num2);
                    break;
                case "*":
                    result = calc.Multiply(num1, num2);
                    break;
                case "/":
                    result = calc.Divide(num1, num2);
                    break;
                default:
                    Console.WriteLine("Invalid operation.");
                    return;
            }

            Console.WriteLine($"Result: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}