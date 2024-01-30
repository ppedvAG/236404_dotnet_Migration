using System;
using System.Threading.Tasks;

namespace HalloParallel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hallo");

            Parallel.For(0, 100000, i => Console.WriteLine(i));

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
