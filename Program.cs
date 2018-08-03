using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            WebHost.CreateDefaultBuilder(args)
                .Configure(app => 
                {
                    app.Run(async (context) =>
                    {
                        await context.Response.WriteAsync("Bem-vindo ao site!");
                    });
                })
                .Build().Run();
        }
    }
}
