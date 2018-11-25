using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No8.Solution
{
    public class PrinterCreator
    {
        /// <summary>
        /// Create printer according his class name
        /// </summary>
        /// <param name="printerClass">Printer from list of all unique printers</param>
        /// <param name="name">Printer's name</param>
        /// <param name="model">Printer's model</param>
        /// <returns></returns>
        public Printer CreatePrinter(Printers printerClass,string name,string model)
        {
            if (name==null)
            {
                throw new ArgumentNullException($"{name} can't be null");
            }
            if (model == null)
            {
                throw new ArgumentNullException($"{model} can't be null");
            }
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
