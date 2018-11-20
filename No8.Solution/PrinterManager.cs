using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using NLog;
using System.Windows.Forms;

namespace No8.Solution
{
    public delegate void PrinterDelegate(string arg);
    

    public class PrinterManager
    {
        public event Action<string> OnPrint;

        Logger logger = LogManager.GetCurrentClassLogger();

        #region Private fields
        private List<Printer> printers;
        private List<string> printerTypeList;
        #endregion

        #region Constructors
        public PrinterManager()
        {
            printers = new List<Printer>();
            printerTypeList = new List<string>();
            OnPrint += Log;
        }
        #endregion

        #region Common operations in manager
        public bool Add(Printer p1)
        {
            if (!Contains(p1))
            {
                if (!printerTypeList.Contains(p1.Name))
                {
                    printerTypeList.Add(p1.Name);
                }
                printers.Add(p1);
                return true;
            }
            return false;
        }

        public Printer Find(string name,string model)
        {
            if (name == null)
            {
                throw new ArgumentNullException($"{nameof(name)} can't be null");
            }
            if (model == null)
            {
                throw new ArgumentNullException($"{nameof(model)} can't be null");
            }
            foreach (Printer printer in printers)
            {
                if (printer.Name==name && printer.Model==model)
                {
                    return printer;
                }
            }
            return null;
        }

        public bool Contains(Printer p)
        {
            if (p == null)
            {
                throw new ArgumentNullException($"{nameof(p)} can't be null");
            }
            foreach (Printer printer in printers)
            {
                if (p.Equals(printer))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        public IEnumerable<string> TakeModels(string name)
        {
            List<string> result = new List<string>();
            foreach(Printer printer in printers)
            {
                if (name==printer.Name)
                {
                    result.Add(printer.Model);
                }
            }
            return result;
        }
        

        public IEnumerable<string> Print(Printer printer,string fileName)
        {
            var o = new OpenFileDialog();
            o.ShowDialog();
            var f = File.OpenRead(o.FileName);

            return GetTextForPrint(printer, f);
        }

        public void Log(string s)
        {
            logger.Info(s);
        }

        private IEnumerable<string> GetTextForPrint(Printer p, FileStream fs)
        {
            OnPrint($"Printer {p.Name} - {p.Model} began to print");
            if (!Contains(p))
            {
                throw new InvalidOperationException($"In the list of printers no such {nameof(p)}");
            }
            List<string> resultList = (List<string>)p.GetTextForPrint(fs);
            OnPrint($"Printer {p.Name} - {p.Model} has finished printing");
            return resultList;
        }

        public bool PrinterTypeContains(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException($"{nameof(name)} can't be null");
            }
            return printerTypeList.Contains(name);
        }

        public IEnumerable<string> GetPrinterTypeList()
        {
            return printerTypeList;
        }

        public bool isEnumValueDefined(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException($"{nameof(value)} can't be null");
            }
            return Enum.IsDefined(typeof(Printers), value);
        }
        
    }
}