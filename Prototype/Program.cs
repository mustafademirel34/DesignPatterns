using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    class Program // prototip tasarım deseni
    {
        static void Main(string[] args)
        {
            Customer customer1 = new Customer {FirstName = "Engin", LastName = "Demiroğ", City = "Ankara", Id = 1};
            
            // kopyasını alma
            Customer customer2 = (Customer)customer1.Clone();
            customer2.FirstName = "Salih";

            Console.WriteLine(customer1.FirstName);
            Console.WriteLine(customer2.LastName);

            Console.ReadLine();

        }
    }

    public abstract class Person // Person'dan kalıtım alanlar özelliklerini implement etmeden de kullanabilir
    {
        // Klonlanması için soyut bir tanım
        public abstract Person Clone(); // abs yapabilmek için class'ın da abs olması gerek

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }

    public class Customer : Person // müşteri bir kişidir buna göre 
    {
        public string City { get; set; }

        // Klonlama yapılabilinecektir
        public override Person Clone()
        {
            return (Person) MemberwiseClone(); // bir kopyasını almamızı sağlar
        }
    }

    public class Employee : Person
    {
        public decimal Salary { get; set; }


        public override Person Clone()
        {
            return (Person)MemberwiseClone();
        }
    }
}
