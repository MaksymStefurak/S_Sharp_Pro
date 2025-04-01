//SendMessage sender = message => Console.WriteLine($"Sending message: {message}");
//Message message1 = new Message(TypeOfMessage.Simple, sender);
//message1.Info();
//message1.Send("Hello this is a tesst message");
//public delegate void SendMessage(string message);


//public enum TypeOfMessage
//{
//    Simple,
//    Difficult
//}
//public class Message
//{
//    public TypeOfMessage Type{  get; set; }
//    private SendMessage messageSender;
//    public Message(TypeOfMessage type, SendMessage sender)
//    {
//        Type = type;
//        messageSender = sender;
//    }

//    public void Info()
//    {
//        Console.WriteLine($"Type of message: {Type}");
//    }
//    public void Send(string content)
//    {
//        messageSender?.Invoke(content);
//    }
//}

using System;

Kata kata = new Kata(1705);
kata.СenturyFromYear(1705);
public class Kata
{
    public int Year {  get; set; }
    public Kata (int year)
    {
        Year = year;
    }
    public void СenturyFromYear(int year)
    {
        if (year / 100 == 0)
        {
            Console.WriteLine(year / 100);
        }
        else
        {
            Console.WriteLine(year / 100 + 1);
        }
    }
    
}