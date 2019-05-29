using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveNpyQuestion
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("YYNB!");
            Console.WriteLine("Test numbers:");
            int T = int.Parse(Console.ReadLine());
            List<int> testresults = new List<int>(T);
            Parallel.For(0, T, i => {
                int result = Test();
                Console.WriteLine(result);
                testresults.Add(result);
            });
            Console.WriteLine($"\n\nAverage = {testresults.Average()}");
            Console.ReadKey();
        }

        static int Test()
        {
            int count = 0;
            HashSet<Constellation> constellations = new HashSet<Constellation>(12);
            HashSet<int> lunisolar = new HashSet<int>(12);
            Random random = new Random();
            while (constellations.Count < 12 || lunisolar.Count < 12)
            {
                DateTime date = new DateTime(1990,1,1);
                date = date.AddDays(random.Next(0, 13148));
                constellations.Add(GetConstellation(date.Month, date.Day));
                ChineseLunisolarCalendar lc = new ChineseLunisolarCalendar();
                lunisolar.Add(lc.GetSexagenaryYear(date)%12);
                count++;
            }

            return count;
        }

        enum Constellation
        {
            Aquarius = 1,       // 水瓶座 1.20 - 2.18
            Pisces = 2,         // 双鱼座 2.19 - 3.20
            Aries = 3,          // 白羊座 3.21 - 4.19
            Taurus = 4,         // 金牛座 4.20 - 5.20
            Gemini = 5,         // 双子座 5.21 - 6.21
            Cancer = 6,         // 巨蟹座 6.22 - 7.22
            Leo = 7,            // 狮子座 7.23 - 8.22
            Virgo = 8,          // 处女座 8.23 - 9.22
            Libra = 9,          // 天秤座 9.23 - 10.23
            Acrab = 10,         // 天蝎座 10.24 - 11.22
            Sagittarius = 11,   // 射手座 11.23 - 12.21
            Capricornus = 12,   // 摩羯座 12.22 - 1.19
        }

        // 根据出生日期获得星座信息
        static Constellation GetConstellation(int birthMonth, int birthDate)
        {
            float birthdayF = birthMonth == 1 && birthDate < 20 ?
                13 + birthDate / 100f :
                birthMonth + birthDate / 100f;

            float[] bound = { 1.20F, 2.20F, 3.21F, 4.21F, 5.21F, 6.22F, 7.23F, 8.23F, 9.23F, 10.23F, 11.21F, 12.22F, 13.20F };

            Constellation[] constellations = new Constellation[12];
            for (int i = 0; i < constellations.Length; i++)
                constellations[i] = (Constellation)(i + 1);

            for (int i = 0; i < bound.Length - 1; i++)
            {
                float b = bound[i];
                float nextB = bound[i + 1];
                if (birthdayF >= b && birthdayF < nextB)
                    return constellations[i];
            }

            return Constellation.Acrab;
        }
    }
}
