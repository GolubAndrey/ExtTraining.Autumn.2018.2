using System.IO;
using System.Collections.Generic;
using System.Collections;
using System;

namespace No8.Solution
{
    public abstract class Printer:IEquatable<Printer>
    {
        public string Name { get; set; }

        public string Model { get; set; }

        public Printer(string name,string model)
        {
            Name = name;
            Model = model;
        }

        public virtual IEnumerable<string> GetTextForPrint(FileStream fs)
        {
            List<string> text = new List<string>();
            for (int i = 0; i < fs.Length; i++)
            {
                text.Add(fs.ReadByte().ToString());
            }
            return text;
        }
        
        public bool Equals(Printer otherPrinter)
        {
            if (otherPrinter==null)
            {
                return false;
            }
            return Name == otherPrinter.Name && Model == otherPrinter.Model;
        }

        public override bool Equals(object obj)
        {
            if (obj==null)
            {
                return false;
            }
            Printer printer = obj as Printer;

            return Equals(printer);
        }
    }
}