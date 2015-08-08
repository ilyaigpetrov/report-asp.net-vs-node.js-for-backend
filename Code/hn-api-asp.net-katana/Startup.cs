using Owin;
using Microsoft.Owin.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using System;

namespace HelloWorld
{
    public class Startup
    {
        private ILogger logger;

        // Invoked once at startup to configure your application.
        public void Configuration(IAppBuilder app)
        {
            this.logger = app.CreateLogger(this.GetType().Name);
            app.Run(async (context) => {
                logger.WriteInformation("Configuring...");

                var path = context.Request.Path;
                logger.WriteInformation("Has request for " + path + "!");
                if (!path.Value.Equals("/"))
                    return;
                logger.WriteInformation("Ok, processing request...");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://hacker-news.firebaseio.com");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("/v0/topstories.json?print=pretty");
                    logger.WriteInformation("Got first response.");
                    if (response.IsSuccessStatusCode)
                    {
                        var arr = await response.Content.ReadAsAsync<dynamic>();
                        var responseId = (String)arr[0];
                        var itemUrl = String.Format("/v0/item/{0}.json?print=pretty", responseId);
                        response = await client.GetAsync(itemUrl);
                        logger.WriteInformation("Got second response.");
                        if (response.IsSuccessStatusCode)
                        {
                            var dict = await response.Content.ReadAsAsync<dynamic>();
                            var title = (String)dict["title"];
                            logger.WriteInformation(title);
                            await context.Response.WriteAsync(title);
                            return;
                        }
                    }
                    logger.WriteInformation("Error:" + await response.Content.ReadAsStringAsync());
                    await context.Response.WriteAsync("Service is not available!");
                }
            });
        }
    }
}