using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketServer
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                Server.Start(12);
            }
        }
    }
}
