using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Net;

namespace Pretty.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {

            return WebHost.CreateDefaultBuilder(args)
            .UseUrls(args.Length > 0 ? args[0] : "https://localhost:44351")
            .UseKestrel(options =>
            {
                options.Limits.MaxRequestBufferSize = 302768;
                options.Limits.MaxRequestLineSize = 302768;
            }).UseStartup<Startup>();
        }
    }
}
