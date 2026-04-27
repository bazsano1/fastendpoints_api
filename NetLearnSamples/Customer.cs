namespace NetLearnSamples
{
    public class Customer
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public Customer(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    public static class UpdateCustomer
    {
        extension(Customer cr)
        {
            public void UpdateAge(int newAge)
            {
                if (newAge < 0)
                {
                    throw new ArgumentException("Age cannot be negative.", nameof(newAge));
                }
                cr.Age = newAge;
            }
        }

        extension(List<Customer?> customers)
        {
            public void UpdateAges(int newAge)
            {
                if (newAge < 0)
                {
                    throw new ArgumentException("Age cannot be negative.", nameof(newAge));
                }
                foreach (var customer in customers)
                {
                    customer?.UpdateAge(newAge);
                }
            }
        }
    }


    static class Program
    {
        static void Main(string[] args)
        {
            var customers = new List<Customer?>();

            Customer? customer = null;
            customer?.UpdateAge(35);
            Console.WriteLine($"After update: {customer?.Name}, {customer?.Age}");

            customers.Add(customer);
            customers.Add(new Customer("Alice", 30));
            customers.Add(new Customer("Bob", 25));
            customers.UpdateAges(40);
        }
    }
}
