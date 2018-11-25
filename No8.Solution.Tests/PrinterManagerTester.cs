using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using No8.Solution;
using System.IO;

namespace No8.Solution.Tests
{
    [TestFixture]
    class PrinterManagerTester
    {
        private PrinterCreator pc;
        private PrinterManager pm;
        [SetUp]
        public void ManagerAndCreatorInitialization()
        {
            pm = new PrinterManager();
            pc = new PrinterCreator();
        }
        [Test]
        public void PrinterCreate_ValideParameters_NormalPrinter()
        {
            Printer canonThroughCreator = pc.CreatePrinter(Printers.CanonPrinter, "1", "2");
            Printer epsonThroughCreator = pc.CreatePrinter(Printers.EpsonPrinter, "1", "2");
            Assert.True(canonThroughCreator.Equals(new CanonPrinter("1","2")));
            Assert.True(epsonThroughCreator.Equals(new EpsonPrinter("1", "2")));
        }

        [TestCase(Printers.CanonPrinter,null,"1")]
        [TestCase(Printers.EpsonPrinter,"1",null)]
        public void PrinterCreate_InvalideParameters_ThrownException(Printers printerType,string name,string model)
        {
            Assert.Throws<ArgumentNullException>(()=>pc.CreatePrinter(printerType,name,model));
        }

        [TestCase(Printers.CanonPrinter, "1", "1",ExpectedResult =true)]
        [TestCase(Printers.EpsonPrinter, "1", "1",ExpectedResult =true)]
        public bool PrinterAdd_2Printers_2True(Printers printerType, string name, string model)
        {
            return pm.Add(pc.CreatePrinter(printerType, name, model));
        }

        [Test]
        public void PrinterAdd_ReaddPrinter_False()
        {
            pm.Add(pc.CreatePrinter(Printers.EpsonPrinter, "1", "2"));
            Assert.False(pm.Add(pc.CreatePrinter(Printers.EpsonPrinter, "1", "2")));
        }

        [TestCase(Printers.EpsonPrinter,"1","2",ExpectedResult =true)]
        [TestCase(Printers.EpsonPrinter, "2", "2", ExpectedResult = false)]
        public bool PrinterContains(Printers printerType, string name, string model)
        {
            pm.Add(pc.CreatePrinter(Printers.EpsonPrinter, "1", "2"));
            return pm.Contains(pc.CreatePrinter(printerType, name, model));
        }

        [TestCase("1", "2",ExpectedResult =true)]
        [TestCase("2", "2",ExpectedResult =false)]
        public bool PrinterFind(string name, string model)
        {
            Printer addedPrinter = pc.CreatePrinter(Printers.EpsonPrinter, "1", "2");
            pm.Add(addedPrinter);
            Printer printer = pm.Find(name,model);
            return addedPrinter.Equals(printer);
        }

        [Test]
        public void PrinterManagerPrint_ValidPrinting()
        {
            Printer addedPrinter = pc.CreatePrinter(Printers.EpsonPrinter, "1", "2");
            pm.Add(addedPrinter);
            CollectionAssert.AreEqual(addedPrinter.GetTextForPrint(File.OpenRead(@"E:\EPAM\NET.A.2018.Golub\Day 17\M13.Streams.Task\ConsoleClient\bin\Debug\SourceText.txt")), pm.Print(addedPrinter, File.OpenRead(@"E:\EPAM\NET.A.2018.Golub\Day 17\M13.Streams.Task\ConsoleClient\bin\Debug\SourceText.txt")));
        }
        
        [Test]
        public void PrinterManagerPrint_NullParameters_ThrowsArgumentNullException()
        {
            FileStream fs = File.OpenRead(@"E:\EPAM\NET.A.2018.Golub\Day 17\M13.Streams.Task\ConsoleClient\bin\Debug\SourceText.txt");
            Printer printer = new CanonPrinter("1", "2");
            pm.Add(printer);
            Assert.Throws<ArgumentNullException>(()=>pm.Print(null,fs));
            Assert.Throws<ArgumentNullException>(() => pm.Print(printer, null));
        }
        
    }
}
