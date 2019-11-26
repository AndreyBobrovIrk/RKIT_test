using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int n1, n2;
                ReadNumber(out n1, "First");
                ReadNumber(out n2, "Second");

                SwapValues(ref n1, ref n2);

                Console.WriteLine("Swapped Numbers: First - {0} , Second - {1}", n1, n2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to close the application.");
                Console.ReadKey();
            }
        }

        static void SwapValues(ref int n1, ref int n2)
        {
            n1 = n1 + n2;
            n2 = n1 - n2;
            n1 = n1 - n2;
        }

        static void ReadNumber(out int result, string name = "Some")
        {
            Console.Write(String.Format("Enter {0} Number: ", name));
            String s = Console.ReadLine();

            if (!Int32.TryParse(s, out result))
            {
                throw new Exception(String.Format("Wrong Number Format: {0}", s));
            }
        }
    }
}
