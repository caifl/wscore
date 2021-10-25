using System;

namespace WSCore.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.GreetingServiceSoapClient client = new ServiceReference1.GreetingServiceSoapClient(ServiceReference1.GreetingServiceSoapClient.EndpointConfiguration.GreetingServiceSoap12);
            var result = client.Say("wscore");

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
