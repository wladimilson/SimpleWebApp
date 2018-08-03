using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using System.Text;

namespace WebApp
{
    public class Program
    {
        public struct Article
        {
            public string Title;
            public string Content;
        }
        public static void Main(string[] args)
        {
            
            WebHost.CreateDefaultBuilder(args)
                .Configure(app => 
                {
                    app.Run(async (context) =>
                    {
                        
                        context.Response.ContentType = "text/html; charset=utf-8";

                        var content = new StringBuilder("<dl>");
                        foreach(var article in Articles())
                            content.Append($"<dt>{article.Title}</dt><dd>{article.Content}</dd>");
                        content.Append("</dt>");

                        await context.Response.WriteAsync(content.ToString());
                    });
                })
                .Build().Run();
        }

        public static List<Article> Articles() {
            var articles = new List<Article>();
            using (var connection = new SqliteConnection(new SqliteConnectionStringBuilder
                        {
                            DataSource = "data.db"
                        }.ToString()))
                {
                
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM Article";
                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine(reader.RecordsAffected);
                        while (reader.Read())
                        {
                            Console.WriteLine(reader.ToString());
                            articles.Add(new Article { Title = reader["title"].ToString(), Content = reader["content"].ToString()});
                        }
                    }
            }

            return articles;
        }
    }
}
