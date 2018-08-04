using System;

namespace HomiesAPI.DataAccess
{
    public static class SeedDefaults
    {
        public const int NUM_DEFAULTS = 8;

        private static Random _randomizer = new Random();

        public static string[] FirstNames = new string[] 
        { 
            "Josh", 
            "Logan", 
            "Hayden", 
            "Jack", 
            "Alex", 
            "Kurt", 
            "Austin", 
            "Cal" 
        };

        public static string[] LastNames { get; set; } = new string[] 
        {
            "Fry",
            "Rodman",
            "Atwell",
            "Kinney",
            "Smith",
            "Moreau",
            "Lee",
            "Michaels"
        };

        public static string[] Emails { get; set; } = new string[]
        {
            "mruw.ratnaa@nhifswkaidn4hr0dwf4.ga",
            "clucia@keykeykelynss.tk",
            "vx.yazanstoneg@tgxvhp5fp9.tk",
            "3immranb@raiasu.ml",
            "2saadmed.elkhatie@toy68n55b5o8neze.cf",
            "7crist@vs3ir4zvtgm.ga",
            "harsh@tq84vt9teyh.cf",
            "falim@vmail.tech"
        };

        public static string[] States { get; set; } = new string[]
        {
            "Ohio",
            "California",
            "Michigan",
            "Florida",
            "Texas",
            "Washington",
            "Hawaii",
            "New York"
        };

        public static string[] Cities { get; set; } = new string[]
        {
            "Columbus",
            "Los Angeles",
            "Detroit",
            "Miami",
            "San Antonio",
            "Seattle",
            "Honolulu",
            "Manhattan"
        };

        public static string[] Streets { get; set; } = new string[] 
        {
            "Oakland Avenue",
            "Main Street",
            "5th Avenue",
            "Park Place Avenue",
            "Lane Avenue",
            "High Street",
            "College Road",
            "Morgan Run"
        };

        public static int GetRandomInt(int min = 0, int max = NUM_DEFAULTS)
        {
            return _randomizer.Next(min, max);
        }

        public static bool GetRandomBool()
        {
            return _randomizer.Next(100) > 50;
        }

        public static string GetRandomDigitString(int numDigits = 5)
        {
            var min = (int)Math.Pow(10, numDigits - 1);
            var max = (int)Math.Pow(10, numDigits);
            var num = GetRandomInt(min, max);
            var converted = num.ToString();
            return converted;
        }

        public static DateTime GetRandomDateTime()
        {
            var monthsOffset = SeedDefaults.GetRandomInt(-12, 1);
            var daysOffset = SeedDefaults.GetRandomInt(-30, 1);
            var hoursOffset = SeedDefaults.GetRandomInt(-60, 1);
            var minutesOffset = SeedDefaults.GetRandomInt(-60, 1);
            var secondsOffset = SeedDefaults.GetRandomInt(-60, 1);

            var time = DateTime.Now
                .AddMonths(monthsOffset)
                .AddDays(daysOffset)
                .AddHours(hoursOffset)
                .AddMinutes(minutesOffset)
                .AddSeconds(secondsOffset);

            return time;
        }
    }
}