// Copyright (c) Jeffcky <see cref="https://jeffcky.ke.qq.com/"/> All rights reserved.
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace IDS4Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
