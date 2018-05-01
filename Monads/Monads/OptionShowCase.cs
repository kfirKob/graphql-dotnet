using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads
{
    
    class MonadBasics
    {
        static void Main(string[] args)
        {
           
            int val = 3;

            //lift to container  => "Real world to abstraction"
            Option<int> intOption = Option<int>.Some(val);
            Option<int> intOptionNONE = Option<int>.None;

            /*Map 
             * public Option<B> Map<B>(Func<A, B> f);
             * "Abstraction to abstraction"
             * 1. extract "real value" 
             * 2. Aplly function from "real value" to different "real value"
             * 3. Lift again to abstraction.
             */
            Option<string> stringOption = intOption.Map<string>(value => value.ToString());
            Option<string> stringOptionNONE = intOptionNONE.Map<string>(value => value.ToString());

            /*Bind
             * public Option<B> Bind<B>(Func<A, Option<B>> f);
             * "Abstraction to abstraction"
             * 1. Extract "real value"
             * 2. Apply function from "real world" to abstraction"
             * 
             * If we were using MAP we would end up with nested containers.. not so cool.
             */
            Option<string> stringOption2 = stringOption.Bind(s => Option<string>.Some(s + "2"));
            Option<Option<string>> OptionstringOption2 = stringOption.Map(s => Option<string>.Some(s + "2"));

            //Back from abstraction to "real world"
            var res = stringOption2.
                Some(s => s).
                None(string.Empty);



            //chinning example
            string resChain = Option<int>.Some(val).
                Map<string>(value => value.ToString()).
                Bind(s => Option<string>.Some(s + "2")).
                Some(s => s).
                None(string.Empty);


            /*
             * Good practice will try and stay as much as possible in abstraction level.
             * Can convert between different kind of monads along the way.. not just inner values. (chinning not affected) 
             * *Not easy as it sounds
             * 
             */


        }


    }
}
