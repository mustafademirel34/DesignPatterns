using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    // Dikkat edilmesi gereken husus; herkes tarafından kullanılacak mı
    // 

    class Program
    {
        static void Main(string[] args)
        {
            // CustomerManager new'lenerek üretilemiyor.

            var customerManager = CustomerManager.CreateAsSingleton();
            customerManager.Save();

            // ninject gibi eklentilerle business katmanı için singleton olaylar birlikte gelir.
            // <>.InSingleton(); gibi
        }
    }

    // design pattern olarak geçiyor ancak 
    class CustomerManager // bence her dp için class'ı özelleştiriyoruz diyebilirim kısaca
    {
        // static olacak, 
        private static CustomerManager _customerManager;

        // defensive programming kurallarına göre nadiren oluşabilecek birden fazla örnek
        // oluşturulmaması için olay kilitleme kullanılır. 
        static object _lockObject =new object();

        

        private CustomerManager() // private constructor // dış erişimi engelleme
        {
         
        }
        // Daha önce oluşturulmuş nesne varsa onu, yoksa oluşturup döndürür
        public static CustomerManager CreateAsSingleton() // singleton kendisini döndüren metot
        {
            // bir olay işlenirken ikinci bağlantı bekletilir
            lock (_lockObject) // scope safe çalışır // blok
            {
                // _customerManager oluşturulmamışsa oluşturulur
                if (_customerManager==null)
                {
                    _customerManager=new CustomerManager();
                }
            }
            return _customerManager;

            // alternatif, döndür _customerManager, eğer null ise oluştur
            // return _customerManager ?? (_customerManager = new CustomerManager());
        }

        public void Save() // her zamanki gibi işlem metotları kaydedilebilir 
        {
            Console.WriteLine("Saved!!");
        }

    }
}
