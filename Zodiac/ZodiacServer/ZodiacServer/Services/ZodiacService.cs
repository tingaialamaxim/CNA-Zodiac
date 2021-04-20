using ZodiacServer.DataAcces.ZodiacService.DataAccess;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZodiacServer.DataAcces;


namespace ZodiacServer.Services
{
    public class ZodiacService: Horoscope.HoroscopeBase
    {
        /*private readonly ZodiacOperations zodiacOperations = new ZodiacOperations();*/
        public override Task<AddZodiacResponse> AddZodiac(AddZodiacRequest request, ServerCallContext context)
        {
            var zodiac = request.Zodiac;

            if (zodiac.Date.Equals("Invalid Date"))
            {
                Console.WriteLine("\nDate Is Blank!");
                return Task.FromResult(new AddZodiacResponse()
                { Status = AddZodiacResponse.Types.Status.Error, Sign = "Invalid Sign" });
            }

          

           /* var date = zodiac.Date.Split("/");
            var thisYear = int.Parse(date[2]);
            var thisMonth = int.Parse(date[0]);
            var thisDay = int.Parse(date[1]);*/

           /* var dateTime = new DateTime(thisYear, thisMonth, thisDay);*/
            var sign = ZodiacOperations.GetSign(zodiac.Date,@".\Resources\zodiac.txt");
           /* var client = new Horoscope.HoroscopeClient(channel);
            var response = client.AddZodiac(new AddZodiacRequest { Zodiac });
            */
            
        Console.WriteLine($"\nSign: {sign}\n");

            return Task.FromResult(new AddZodiacResponse()
            { Status = AddZodiacResponse.Types.Status.Success, Sign = sign });
        }

        
    }
}
