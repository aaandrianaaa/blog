using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Helper
{
    class RandomPassword
    {
        public static string Generate()
        {

            Random random = new Random();
            int num_letters = random.Next(5, 15);

            Random rand = new Random();


            char[] letters = "ABCDEFGHIJKLMNOPQghfj1257bjkRSTUVWXYZ".ToCharArray();
            string password = "";

            for (int i = 1; i <= num_letters; i++)
            {
             
                int letter_num = rand.Next(0, letters.Length - 1);
                password += letters[letter_num];
            }

            return password;
        }
    }
}
