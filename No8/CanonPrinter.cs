using System;
using System.IO;

namespace No8
{
    internal class CanonPrinter
    {
        public CanonPrinter(string name,string model)
        {
            Name = "Canon";
            Model = "123x";
        }

        public void Print(FileStream fs)
        {
            for (int i = 0; i < fs.Length; i++)
            {
                // simulate printing
                Console.WriteLine(fs.ReadByte());
            }
        }

        public string Name { get; set; }

        public string Model { get; set; }
    }
}