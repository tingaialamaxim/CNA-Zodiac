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
        public override Task<AddZodiacResponse> AddZodiac(AddZodiacRequest request, ServerCallContext context)
        {
            var zodiac = request.Zodiac;
            if (zodiac.Date.Equals("Invalid Date"))
            {
                Console.WriteLine("\nThe date is Blank!");
                return Task.FromResult(new AddZodiacResponse()
                { Status = AddZodiacResponse.Types.Status.Error, Sign = "Invalid sign" });
            }
            var zodiacSign = ZodiacOperations.GetSign(zodiac.Date,@".\Resources\zodiac.txt"); 
            Console.WriteLine($"\nSign: {zodiacSign}\n");
            return Task.FromResult(new AddZodiacResponse()
               { Status = AddZodiacResponse.Types.Status.Success, Sign = zodiacSign });
        }
    }
}
