using LanguageExt;
using LanguageExt.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads
{


    public static class StringToInt
    {

        public static int ToInt(string s)
        {
            if (int.TryParse(s, out int res))
            {
                return res;
            }
            else
            {
                //return -1;
                throw new InvalidCastException();
            }
        }

        public static int? ToInt2(string s)
        {
            if (int.TryParse(s, out int res))
            {
                return res;
            }
            else
            {
                return null;
            }
        }

        public static Option<int> ToInt3(string s)
        {
            return int.TryParse(s, out int res)
                ? Option<int>.Some(res)
                : Option<int>.None;

        }


        //Use Unit instead of void.. (Out of scope)
        public static void LogMultiply(string numAsString, string numAsString2)
        {
            try
            {
                var x = ToInt(numAsString);
                System.Console.WriteLine(x * 2);
            }
            catch
            {
                //whatever
            }
                
        }

        public static void LogMultiply2(string numAsString)
        {
            var x = ToInt2(numAsString);
            if (x.HasValue)
            {
                System.Console.WriteLine(x * 2);
            }
        }


        
        public static void LogMultiply3(string numAsString)
        {
            ToInt3(numAsString)
                .Map(x=> x++)
                .Some(x => System.Console.WriteLine(x * 2));
        }



    }


}
