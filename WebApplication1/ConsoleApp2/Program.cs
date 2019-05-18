using Microsoft.Extensions.Options;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class MyOptions
    {
        public string Name { get; set; }
    }
    public class UrlItem
    {
        public string url { get; set; }
        private int _weight;
        public int weight
        {
            get
            {
                if (_weight == 0)
                    return 1;
                else
                    return _weight;
            }
            set
            {
                if (value == 0)
                    _weight = 1;
                else
                    _weight = value;
            }
        }
    }
    public class GetRandUrl
    {
        private static int len = 0;
        private static string[] urls;
        public static string GetUrl(List<UrlItem> urlItems)
        {
            if (urlItems == null || urlItems.Count() == 0)
                return string.Empty;
            if (urlItems.Count() == 1)
                return urlItems[0].url;
            if (len == 0)
            {
                List<string> strList = new List<string>();
                urlItems.ForEach(t =>
                {
                    for(int i = 0; i < t.weight; i++)
                    {
                        strList.Add(t.url);
                    }
                });
                urls = strList.ToArray();
                len = urls.Length;
            }

            var uuid = Guid.NewGuid().ToByteArray();
            int seed = DateTime.Now.Millisecond + uuid.Sum(t => t);
            Random random = new Random(seed);
            int index = random.Next(0, len);
            string url = urls[index];
            Console.WriteLine($"随机地址:{url}");
            return url;
        }
    }
    class Program
    {
        private IOptionsMonitor<MyOptions> _options;
        public Program()
        {
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .Build();
            //var serviceCollection = new ServiceCollection();
            //serviceCollection.Configure<MyOptions>(configuration);

            //var serviceProvider = serviceCollection.BuildServiceProvider();
            //_options = serviceProvider.GetRequiredService<IOptionsMonitor<MyOptions>>();
            List<UrlItem> urlItems = new List<UrlItem>() {
                new UrlItem{ url="参数"},
                new UrlItem{ url="返回",weight=2},
                new UrlItem{ url="boolean"}
            };
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Run(() => {
                    GetRandUrl.GetUrl(urlItems);
                }));
            }
            Task.WaitAll(tasks.ToArray());
            Console.ReadKey();
        }
        
        private string GetUrl(string urls)
        {
            if (string.IsNullOrWhiteSpace(urls))
                return urls;
            var urlArray = urls.Split(",");
            if (urlArray.Length == 1)
                return urlArray[0];
            int len = urlArray.Length;
            var uuid = Guid.NewGuid().ToByteArray();
            int seed = DateTime.Now.Millisecond + uuid.Sum(t => t);
            //int seed = uuid.Sum(t => t);
            Random random = new Random(seed);
            int index = random.Next(0, len);
            string url = urlArray[index];
            Console.WriteLine($"随机地址:{url}");
            return url;
        }
        static void Main(string[] args)
        {
            new Program().Execute(args);
        }
        public void Execute(string[] args)
        {
            Console.WriteLine(_options.CurrentValue.Name);
            _options.OnChange(o => Console.Write(o.Name));
            Console.ReadKey();
        }
    }
}
