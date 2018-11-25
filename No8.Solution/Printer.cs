using System.IO;
using System.Collections.Generic;
using System.Collections;
using System;

namespace No8.Solution
{
    public abstract class Printer:IEquatable<Printer>
    {
        /// <summary>
        /// Printer's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Printer's model
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Printer constructor
        /// </summary>
        /// <param name="name">Printer's name</param>
        /// <param name="model">Printer's model</param>
        public Printer(string name,string model)
        {
            Name = name;
            Model = model;
        }

        /// <summary>
        /// Return lines for output according to printer's type
        /// </summary>
        /// <param name="fs">Stream of file</param>
        /// <returns>Output lines</returns>
        public virtual IEnumerable<string> GetTextForPrint(FileStream fs)
        {
            if (fs==null)
            {
                throw new ArgumentNullException($"{nameof(fs)} can't be null");
            }
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