using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No8.Solution
{
    public class PrinterCreator
    {
        public Printer CreatePrinter(Printers printerClass,string name,string model)
        {
            string fullName = $"{GetType().Namespace}.{printerClass.ToString()}";
            Type t;
            try
            {
                t = Type.GetType(fullName,true,false);
            }
            catch(TypeLoadException ex)
            {
                throw new InvalidOperationException($"There is no such printer class {nameof(printerClass)}");
            }
            Printer printer = (Printer)Activator.CreateInstance(t, name, model);
            return printer;
        }
    }
}
