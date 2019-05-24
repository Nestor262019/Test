using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestNestorCanales;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            const string message = "Inicio de sesión con éxito";
            const bool message2 = true;
            const bool warning = true;
            const bool error = true;

            JobLogger.LogMessage(message, message2, warning, error);              

        }
    }
}
