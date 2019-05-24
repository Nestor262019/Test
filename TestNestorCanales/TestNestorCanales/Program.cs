using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNestorCanales
{
   public class Program
    {
        static void Main(string[] args)
        {
            JobLogger test = new JobLogger(true,false,true,true,true,true);
            JobLogger.LogMessage("Hi Friends",false,false,true);
        }     

    }
}
