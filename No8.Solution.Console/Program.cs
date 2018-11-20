using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using No8.Solution;

namespace No8.Solution.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            PrinterManager pm = new PrinterManager();
            PrinterCreator pc = new PrinterCreator();

            string act = null;
            while(act!="4")
            {
                System.Console.WriteLine("Select your choice:");
                System.Console.WriteLine("1:Add new printer");
                DrawPrintersList(2,pm.GetPrinterTypeList());
                
                act = System.Console.ReadLine();
                switch (act)
                {
                    case "1":
                        {
                            string printerName = System.Console.ReadLine();
                            string printerModel = System.Console.ReadLine();
                            string printer = $"{printerName}Printer";
                            if (!pm.isEnumValueDefined(printer))
                            {
                                System.Console.WriteLine("Сannot add this type of printer");
                                break;
                            }
                            if (!pm.Add(pc.CreatePrinter((Printers)Enum.Parse(typeof(Printers),printer), printerName, printerModel)))
                            {
                                System.Console.WriteLine("Impossible to add printer with this name and model");
                            }
                            break;
                        }
                    default:
                        {
                            List<string> list = pm.GetPrinterTypeList().ToList();
                            int intAct = Convert.ToInt32(act);
                            if (intAct >1 && intAct<list.Count+2)
                            {
                                DrawPrintersList(1,pm.TakeModels(list[intAct - 2]));
                                string model = System.Console.ReadLine();

                            }
                            break;
                        }
                }
            }
        }

        public static void DrawPrintersList(int counter,IEnumerable<string> list)
        {
            foreach(string element in list)
            {
                System.Console.WriteLine($"{counter}. Print on {element}");
                counter++;
            }
        }
    }
}
