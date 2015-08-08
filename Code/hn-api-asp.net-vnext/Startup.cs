using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using System;

namespace HelloWeb
{
  public class Startup
  {
  
    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
      var logger = loggerFactory.AddConsole().CreateLogger(this.GetType().Name);
      logger.LogInformation("Configuring...");
      
      app.Run(async (context) => {
        var path = context.Request.Path;
        logger.LogInformation("Has request for "+path+"!");
        if (!path.Equals("/"))
          return;

        using (var client = new HttpClient())
        {
          client.BaseAddress = new Uri("https://hacker-news.firebaseio.com");
          client.DefaultRequestHeaders.Accept.Clear();
          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

          HttpResponseMessage response = await client.GetAsync("/v0/topstories.json?print=pretty");
          logger.LogInformation("Got first response.");
          if (response.IsSuccessStatusCode)
          {
            var arr = await response.Content.ReadAsAsync<dynamic>();
            var responseId = (String) arr[0];
            var itemUrl = String.Format("/v0/item/{0}.json?print=pretty", responseId);
            response = await client.GetAsync(itemUrl);
            logger.LogInformation("Got second response.");
            if (response.IsSuccessStatusCode)
            {
              var dict = await response.Content.ReadAsAsync<dynamic>();
              var title = (String) dict["title"];
              logger.LogInformation(title);
              await context.Response.WriteAsync(title);
              return;
            }
          }
          logger.LogInformation("Error:" + await response.Content.ReadAsStringAsync());
          await context.Response.WriteAsync("Service is not available!");
        }      
      });
    }
  }
}