using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicc.Model
{
    public class InputValidator
    {

        

        static public bool InputNotEmpty(string input)
        {
            if (input == null)
            {
                return false;
            }
            return true;
        }
        static public bool InputOnlyNumbers(string input)
        {
            foreach (char c in input)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
        }
        static public bool InputOnlyLetters(string input) //polish letters also allowed
        {            
            foreach (char c in input)
            {
                char character = Char.ToLower(c);
                if (!((character >= 'a' && character <= 'z') || 
                    IsPolishLetter(character)))
                {
                    return false;
                }                
            }
            return true;
        }
        static private bool IsPolishLetter(char c)
        {            
            return (c == 'ą' || c == 'ę' || c == 'ż' || c == 'ź' || c == 'ń' || c == 'ł' || c == 'ó'|| c == 'ś');
        }
    }
}
