using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using ZodiacClient;

namespace ZodiacClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Horoscope.HoroscopeClient(channel);
            /*
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();*/
            string date = "";
            /*if (!ValidationOfDate(date))
            {
                while(!ValidationOfDate(date))
                {
                    Console.Write("Enter the date you want to find the zodiac sign for: ");
                    date = Console.ReadLine();
                }
            }*/
            do
            {
                Console.Write("Enter the date you want to find the zodiac sing for: ");
                date = Console.ReadLine();
            } while (!ValidationOfDate(date));
            /* var givenDate = date.Split("/");
             var dateTime = new DateTime(int.Parse(givenDate[2]), int.Parse(givenDate[1]), int.Parse(givenDate[0]));
             Console.WriteLine(dateTime);*/
            var zodiacToBeAdded = new Zodiac()
            { Date = date != null && date.Trim().Length > 0 ? date : "Invalid Date" };
            var response = await client.AddZodiacAsync(new AddZodiacRequest { Zodiac = zodiacToBeAdded });
             Console.WriteLine($"\nResponse Status: {response.Status}\n\nSign: {response.Sign}\n\n");
                   
            


            }
        public static bool ValidationOfDate(string date)
        {
            try
            {
                var givenDate = date.Split("/");
                var dateTime = new DateTime( int.Parse(givenDate[2]),int.Parse(givenDate[1]),int.Parse(givenDate[0]));
            }
            catch(Exception)
            {
                Console.WriteLine("The format you used is invalid, please use a valid format");
                return false;
            }
           /* catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine("The date you introduced isnt a valid date");
                return false;
            }
            catch(IndexOutOfRangeException)
            {
                Console.WriteLine("The date you introduced is invalid");
                return false;
            }*/
            return true;
        }
    }
    
}
