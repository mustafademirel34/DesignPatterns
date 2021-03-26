using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    class Program
    {
        // Amaç nesnelere kolaylık sağlamak için, factory'e benzer
        // şartlara göre, factory gibi ancak belirli şartlara tabii
        static void Main(string[] args)
        {
            
            ProductManager productManager = new ProductManager(new Factory2()); // çalışacağımız fabrika stili
            productManager.GetAll();
            Console.ReadLine();
        }
    }

    public abstract class Logging // Loglama temeli
    {
        public abstract void Log(string message); // soyut log metotu
    }

    public class Log4NetLogger : Logging
    {
        // kalıtım alınarak log içeriği dolduruluyor
        public override void Log(string message)
        {
            Console.WriteLine("Logged with log4net");
        }
    }

    public class NLogger : Logging
    {
        public override void Log(string message)
        {
            Console.WriteLine("Logged with nLogger");
        }
    }

    public abstract class Caching // aynı şekilde farklı bir şey, belleğe alma
    {
        public abstract void Cache(string data);
    }

    public class MemCache : Caching
    {
        // tanımlanıyor işte
        public override void Cache(string data)
        {
            Console.WriteLine("Cached with MemCache");
        }
    }

    public class RedisCache : Caching
    {
        public override void Cache(string data)
        {
            Console.WriteLine("Cached with Redis");
        }
    }


    public abstract class CrossCuttingConcernsFactory
    {
        // Burası şöyle ki normalde olmayan soyut bir tanımlama yapıyoruz.
        // Oluşturulmamış ancak oluşturulmak üzere tanınlanmış bir fabrika diyebiliriz.
        public abstract Logging CreateLogger();
        public abstract Caching CreateCaching();
    }

    public class Factory1 : CrossCuttingConcernsFactory
    {
        // alınan kalıtıma bağlı olarak loglama ve belleğe alma sistemi için birer yöntem veriyoruz
        // oluşturulan doğrultuda bu özellikler bir bütünü andırıyor
        // Bu yöntem isteğe ve amaca göre değişiklik gösterebilir, bunu ise değiştirebilir, yeni fab ekleyebiliriz.

        public override Logging CreateLogger() // CreateLogger metotu override, kalıtım alınan fabrikadan kırılır. Bir yöntem istemektedir.
        {
            return new Log4NetLogger(); // Override edilen abstract nesne oluşturularak dönülür. ** Bu farklı işler için de kullanılabilir.
            // Yani Factory1'in CreateLogger metotu çalıştırıldığında dönüş verilen nesne oluşturulur. Bu olay CreateLogger ismini yansıtmaktadır.
            // Oluşturulan nesne manager tarafından kullanılıyor.
        }

        public override Caching CreateCaching()
        {
            return new RedisCache();
        }
    }

    public class Factory2 : CrossCuttingConcernsFactory
    {
        // yukarıdaki gibi farklı bir fab
        public override Logging CreateLogger()
        {
            return new NLogger();
        }

        public override Caching CreateCaching()
        {
            return new RedisCache();
        }
    }

    public class ProductManager // Olayı yönettiğimiz kısım burası
    {
       
        private CrossCuttingConcernsFactory _crossCuttingConcernsFactory;
        // Bir CCC belirliyoruz ki onun amacı sadece fabrikaları tanımlamak,
        // fabrikaların yaptığı iş ise yöntemleri bir araya almak (fb1git) 

        // Yöntemler farklılıklar gösterebiliriz.
        // Görünüşe göre buradaki odağımız logging ve caching yöntemleri
        private Logging _logging;
        private Caching _caching;

        public ProductManager(CrossCuttingConcernsFactory crossCuttingConcernsFactory)
        {
            // Bir fabrika tanımı istiyoruz ardından fabrikaya ait yöntemler alınacaktır.
            _crossCuttingConcernsFactory = crossCuttingConcernsFactory;

            // Alınan fabrikanın metotları çalıştırılırken yöntemler, metotlar yardımıyla oluşturularak atanır.
            _logging = _crossCuttingConcernsFactory.CreateLogger(); // Her Create bir nesne oluşumudur.
            _caching = _crossCuttingConcernsFactory.CreateCaching();// Yöntemler _logging ve _caching kaydedilir.
        }

        public void GetAll()
        {
            // Constructor'da hazır hale getirdiğimiz sistemleri buradan kullanabiliriz.
            // Verilen verileri işlemelerini sağlayabiliriz.
            _logging.Log("Logged");
            _caching.Cache("Data");
            Console.WriteLine("Products listed!");
        }
    }

}
