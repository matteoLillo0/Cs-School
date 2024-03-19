using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int N;



            using (StreamReader sr = new StreamReader("C:\\Users\\matteo.angiolillo\\Desktop\\grafo-100.txt"))
            {
                string size = sr.ReadLine();

                int.TryParse(size, out N);

                sr.Close();
            }

        }
    }
}
