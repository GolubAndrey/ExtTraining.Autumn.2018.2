using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using No8.Solution;
using System.IO;
using System.Windows.Forms;

namespace No8.Solution.Console
{
    class Program
    {
        private static PrinterManager pm;
        private static PrinterCreator pc;

        [STAThread]
        static void Main(string[] args)
        {
            pm = new PrinterManager();
            pc = new PrinterCreator();

            string act = null;
            while(act!="0")
            {
                System.Console.WriteLine("Select your choice:");
                System.Console.WriteLine("0:Exit");
                System.Console.WriteLine("1:Add new printer");
                DrawPrintersList(2,pm.GetPrinterTypeList());
                System.Console.WriteLine();
                
                act = System.Console.ReadLine();
                switch (act)
                {
                    case "0":
                        {
                            break;
                        }
                    case "1":
                        {
                            PrinterAdd();
                            break;
                        }
                    default:
                        {
                            Print(act);
                            break;
                        }
                }
            }
        }

        private static void DrawPrintersList(int counter,IEnumerable<string> list)
        {
            foreach(string element in list)
            {
                System.Console.WriteLine($"{counter}. Print on {element}");
                counter++;
            }
        }

        private static void PrinterAdd()
        {
            string printerName = System.Console.ReadLine();
            string printerModel = System.Console.ReadLine();
            string printer = $"{printerName}Printer";
            if (!pm.isEnumValueDefined(printer))
            {
                System.Console.WriteLine("Сannot add this type of printer");
                return;
            }
            try
            {
                if (!pm.Add(pc.CreatePrinter((Printers)Enum.Parse(typeof(Printers), printer), printerName, printerModel)))
                {
                    System.Console.WriteLine("Impossible to add printer with this name and model");
                }
            }
            catch(Exception ex)
            {
                pm.Log(ex.Message);
                System.Console.WriteLine(ex.Message);
            }
        }

        private static bool PrinterTypeValidation(int intAct, List<string> list)
        {
            if (intAct > 1 && intAct < list.Count + 2)
            {
                return true;
            }
            return false;
        }

        private static void Print(string act)
        {
            List<string> list = pm.GetPrinterTypeList().ToList();
            int intAct = Convert.ToInt32(act);
            if (PrinterTypeValidation(intAct,list))
            {
                List<string> models = pm.TakeModels(list[intAct - 2]).ToList();
                DrawPrintersList(1, models);
                System.Console.WriteLine("Enter the model of printer");
                string model = System.Console.ReadLine();
                if (models.Contains(model))
                {
                    var o = new OpenFileDialog();
                    o.ShowDialog();
                    var f = File.OpenRead(o.FileName);
                    foreach (string str in pm.Print(pm.Find(list[intAct - 2], model), f))
                    {
                        System.Console.WriteLine(str);
                    }
                }
                else
                {
                    pm.Log($"No {list[intAct - 2]} printer of this model");
                    System.Console.WriteLine("Invalide operation");
                }
            }
            else
            {
                pm.Log($"No this printer");
                System.Console.WriteLine("Invalide operation");
            }
        }
    }
}
