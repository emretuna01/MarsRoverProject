using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MarsRover.Services
{
    public class RoverControlServices
    {
        /*
       public bool RoverCommandStrigChecker(string input)
       {

           bool result = true;
           string distinctInput = new String(input.Distinct().ToArray());

           if (distinctInput.Length > 2)
           {
               result = false;
               return result;
           }

           foreach (char key in distinctInput)
           {
               if (key != 'L' && key != 'M' && key != 'R')
               {
                   result = false;
                   break;
               }
           }

           return result;


           string pattern = @"[LRM]";

           foreach (Match m in Regex.Matches(input, pattern))
           {
               if (m.Success == false)
               {
                   return false;
               }
           }
           return true;
          

    }
         */
    }
}
