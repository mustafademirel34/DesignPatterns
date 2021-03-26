using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    // Amaç; değişimi kontrol altında tutmak
    class Program
    {
        static void Main(string[] args)
        {
            // DP'nin olayı; kodlama fabrikaları oluşturmak (çok açıklayıcı oldu bence)
            // Örneğin loglama yapılan bu örnekte metotlara bağımlı kalınmıyor.
            // Karmaşık değil aslında öyle sanıyordum.

            CustomerManager customerManager = new CustomerManager(new LoggerFactory2());

            // loggerFac verilince çağrılan Save(); üzerinde
            // LoggerFactory2() new'lenir ve 
            // fab'dan return edilen logger hangisi ise olayını çalıştırır.
            customerManager.Save();

            Console.ReadLine();
        }
    }

    public class LoggerFactory : ILoggerFactory // CreateLogger()
    {
        public ILogger CreateLogger()
        {
            //Business to decide factory

            // alınan mantıklı kararlara göre; örneğin,
            // burada olay kontrol edilerek ihtiyaca yönelik hangi logger
            // ile çalışılması gerekiyor ise return onu döndürecektir.

            return new EdLogger();

            // çeşitli operasyonlara göre birden fazla factory ile de çalışılabilir
            // 
        }
    }

    public class LoggerFactory2 : ILoggerFactory  // birden fazla factory ile çalışılabilir
    {
        public ILogger CreateLogger()
        {
            //Business to decide factory
            return new Log4NetLogger();
        }
    }

    public interface ILoggerFactory
    {
        ILogger CreateLogger();
    }

    public interface ILogger // Logger'ların kalıt alacağı temel imza
    {
        void Log();
    }

    public class EdLogger : ILogger
    {
        // Sadece bir loglama metotu. Bir iş.
        public void Log()
        {
            Console.WriteLine("Logged with EdLogger");
        }
    }

    public class Log4NetLogger : ILogger // birden fazla logger
    {
        public void Log()
        {
            Console.WriteLine("Logged with Log4NetLogger");
        }
    }

    // asıl olay burda
    public class CustomerManager
    {
        // bir loggerfab isteniyor ki bu logger metotlarını yönetir
        // buna göre farklı farklı fabrikalar oluşturulup customermanager'a
        // parametre ile gönderilerek üzerinden çalışılabilir.

        private ILoggerFactory _loggerFactory;

        public CustomerManager(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public void Save()
        {
            Console.WriteLine("Saved!");
            // Verilen factory'den dönen logger new'lenir burada
            ILogger logger = _loggerFactory.CreateLogger();
            // ve içeriğindeki Log() metotu çağrılır
            logger.Log();
        }
    }
}
