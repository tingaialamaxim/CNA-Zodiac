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
            string date = "";
            do
            {
                Console.Write("Enter the date you want to find the zodiac sing for: ");
                date = Console.ReadLine();
            } while (!ValidationOfDate(date));
            var zodiacAdded = new Zodiac()
            { Date = date != null && date.Trim().Length > 0 ? date : "Invalid Date" };
            var response = await client.AddZodiacAsync(new AddZodiacRequest { Zodiac = zodiacAdded });
             Console.WriteLine($"\nResponse Status: {response.Status}\nSign: {response.Sign}\n");
            }
        public static bool ValidationOfDate(string date)
        {
            try
            {
                var givenDate = date.Split("/");
                var dateTime = new DateTime( int.Parse(givenDate[2]),int.Parse(givenDate[0]),int.Parse(givenDate[1]));
            }
            catch(Exception)
            {
                Console.WriteLine("The format you used is invalid, please use a valid format");
                return false;
            }
            return true;
        }
    }
    
}
