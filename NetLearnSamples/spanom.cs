using System;
using System.Collections.Generic;
using System.Text;

namespace NetLearnSamples
{
    public record Person(int Age, string Name);

    internal class spanom
    {
        public delegate void PrintHandler(string message);

        public event PrintHandler OnPrint;

        public void Run()
        {
            ReadOnlySpan<char> name = "Charles, Katie, Johnny".AsSpan();

            var names = name.Split(',');

            var p1 = new Person(30, "Charles");

            var p2 = p1 with { Name = "Katie" };
            var p3 = p2 with { Name = "Johnny" };

            Console.WriteLine(p1 == p2);

            var (age, nam) = p1;

            var printer = (string message) => OnPrint?.Invoke(message);

            
        }

        public async Task<int> ProcessData()
        {
            await Task.Delay(100);
            return 42;
        }

        public void Print(string? message)
        {
            ArgumentNullException.ThrowIfNull(message, nameof(message));
            Console.WriteLine(message.Length);

            var result = message switch
            {
                "Hello" => "Hi there!",
                "Goodbye" => "See you later!",
                _ => "Unknown message"
            };
        }

        public string Describe(object obj)
        {
            return obj switch
            {
                int i when i > 0 => $"Positive integer: {i}",
                int i when i < 0 => $"Negative integer: {i}",
                string s => $"String of length {s.Length}",
                Person p => $"Person named {p.Name} aged {p.Age}",
                _ => "Unknown type"
            };
        }

        public string Classify(Person p) => p switch
        {
            { Name: "Charles" } => "Special Charles",
            { Age: < 18 } => "Child",
            { Age: >= 18 and < 65 } => "Adult",
            { Age: >= 65 } => "Senior",
            _ => "Unknown"
        };
    }


}
