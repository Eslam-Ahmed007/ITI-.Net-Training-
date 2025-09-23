using System;
using System.Linq;

namespace Day7_Task
{
    #region Task1
    class Product
    {
        public string name { get; set; }
        public double price { get; set; }
    }
    #endregion

    #region Task2,3,4,5
    static class ExtensionMethods
    {
        public static int CountWords(this string str)
        {
            var words = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Count();
        }

        public static bool IsEven(this int num)
        {
            return num % 2 == 0;
        }

        public static int Age(this DateTime date)
        {
            var now = DateTime.Now;
            int age = now.Year - date.Year;
            if (now < date.AddYears(age)) age--;
            return (age >= 0) ? age : -1;
        }

        public static string ReverseString(this string str)
        {
            var charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
    #endregion

    internal class Program
    {
        public static object CreateProduct()
        {
            var product = new Product { name = "Sample Product", price = 9.99 };
            return new { product.name, product.price }; // anonymous object
        }

        static void Main(string[] args)
        {
            // Task 1
            var product = CreateProduct();
            Console.WriteLine($"Product: {product}");

            // Task 2
            string text = "Hello world from C#";
            Console.WriteLine($"Word Count: {text.CountWords()}");

            // Task 3
            int number = 42;
            Console.WriteLine($"{number} is even? {number.IsEven()}");

            // Task 4
            DateTime birthDate = new DateTime(2000, 5, 15);
            Console.WriteLine($"Age: {birthDate.Age()}");

            // Task 5
            string hello = "hello";
            Console.WriteLine($"Reversed: {hello.ReverseString()}");
        }
    }
}
