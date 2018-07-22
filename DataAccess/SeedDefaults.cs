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

        public static string[] Emails { get;set; } = new string[]
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

        public static int GetRandomInt(int max = NUM_DEFAULTS)
        {
            return _randomizer.Next(max);
        }

        public static bool GetRandomBool()
        {
            return _randomizer.Next(100) > 50;
        }
    }
}