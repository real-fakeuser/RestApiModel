using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiModel.Helper
{
    public class ErrHandler
    {

        public static void Go(Exception ex, string ErrDescription = null, string location = null)
        {
            try
            {
                Console.WriteLine("###############################################################################");
                Console.WriteLine("An unexpected error occured! ExceptionDescription: " + ex);
                if (ErrDescription != null)
                {
                    Console.WriteLine("ErrorDescription: " + ErrDescription);
                }
                if (ErrDescription != null)
                {
                    Console.WriteLine("Location in code: " + location);
                }
                Console.WriteLine("###############################################################################");
            }
            catch (Exception)
            {
            }
        }

        /*internal static void Go()
        {
            throw new NotImplementedException();
        }*/
    }
}
