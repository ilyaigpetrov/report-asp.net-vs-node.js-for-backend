using System.Web.Http;

namespace HelloWorldWebApi2.Controllers
{
    public class HelloWorldController : ApiController
    {
        public string Get()
        {
            return "Hello World";
        }
    }
}
