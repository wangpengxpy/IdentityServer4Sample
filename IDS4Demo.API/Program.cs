Copyright (c) 2019 https://jeffcky.ke.qq.com/
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace IDS4Demo.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
