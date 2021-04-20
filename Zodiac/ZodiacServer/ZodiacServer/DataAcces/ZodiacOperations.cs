using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ZodiacServer.DataAcces
{
    namespace ZodiacService.DataAccess
    {
        public class ZodiacOperations
        {
           
            public static List<Tuple<Zodiac, Zodiac, string>> GetAllZodiacs(string filePath)
            {
                var zodiacs = new List<Tuple<Zodiac, Zodiac, string>>();

                try
                {
                    var sr = new StreamReader(filePath);
                    var line = sr.ReadLine()?.Split(" ");
                    while (line != null)
                    {
                        zodiacs.Add(new Tuple<Zodiac, Zodiac, string>(
                            new Zodiac { Date = line[0] },
                            new Zodiac { Date = line[1] },
                            line[2]));

                        line = sr.ReadLine()?.Split(" ");
                    }
                    sr.Close();
                }
                catch (Exception)
                {
                    Console.WriteLine("File Not Readable!");
                }

                return zodiacs;
            }

            public static string GetSign(string zodiacDate, string filePath)
            {
                var zodiacs = GetAllZodiacs(filePath);

                return (from variable in zodiacs

                        let startMonth = int.Parse(variable.Item1.Date.Substring(0, 2))
                        let startDay = int.Parse(variable.Item1.Date.Substring(3, 2))
                        let endMonth = int.Parse(variable.Item2.Date.Substring(0, 2))
                        let endDay = int.Parse(variable.Item2.Date.Substring(3, 2))

                        let date = zodiacDate.Split("/")
                        let thisMonth = int.Parse(date[0])
                        let thisDay = int.Parse(date[1])

                        where thisMonth == startMonth && thisDay >= startDay || thisMonth == endMonth && thisDay <= endDay
                        select variable.Item3).FirstOrDefault();
            }
        }
    }
}
