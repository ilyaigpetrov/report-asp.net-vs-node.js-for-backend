using Owin;

namespace HelloWorld
{
  public class Startup
  {
    // Invoked once at startup to configure your application.
    public void Configuration(IAppBuilder app)
    {
      app.Run(async (context) => {
          await context.Response.WriteAsync("Hello World!");
      });
    }
  }
}