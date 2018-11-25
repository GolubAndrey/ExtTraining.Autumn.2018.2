using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using NLog;
using System.Windows.Forms;
using System.Linq;

namespace No8.Solution
{
    public delegate void PrinterDelegate(string arg);
    

    public class PrinterManager
    {
        private event Action<string> OnPrint;

        Logger logger = LogManager.GetCurrentClassLogger();

        #region Private fields
        private List<Printer> printers;
        private List<string> printerTypeList;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for printer manager
        /// </summary>
        public PrinterManager()
        {
            printers = new List<Printer>();
            printerTypeList = new List<string>();
            OnPrint += Log;
        }
        #endregion

        #region Printer list operations
        /// <summary>
        /// Add printer in printer manager
        /// </summary>
        /// <param name="p1">Printer</param>
        /// <returns>true if successfully added, otherwise false</returns>
        public bool Add(Printer p1)
        {
            if (p1==null)
            {
                throw new ArgumentNullException($"{nameof(p1)} can't be null");
            }
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

        /// <summary>
        /// Take printer in printer manager
        /// </summary>
        /// <param name="name">Printer name</param>
        /// <param name="model">Printer model</param>
        /// <exception cref="ArgumentNullException">Throws when one of the arguments is null</exception>
        /// <returns>Found printer</returns>
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
            return printers.Where(t => t.Name == name && t.Model == model).FirstOrDefault();
        }

        /// <summary>
        /// Check for printer availability
        /// </summary>
        /// <param name="p">Printer</param>
        /// <exception cref="ArgumentNullException">When argument is null</exception>
        /// <returns>true if the printer is in stock, otherwise false</returns>
        public bool Contains(Printer p)
        {
            if (p == null)
            {
                throw new ArgumentNullException($"{nameof(p)} can't be null");
            }
            return printers.Where(t => t.Equals(p)).Any();
        }

        /// <summary>
        /// Check for printer with this name availability
        /// </summary>
        /// <param name="name">Printer's name</param>
        /// <exception cref="ArgumentNullException">Throws when the parameter is null</exception>
        /// <returns>true if printer found, otherwise false</returns>
        public bool PrinterTypeContains(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException($"{nameof(name)} can't be null");
            }
            return printerTypeList.Contains(name);
        }
        #endregion

        #region Get printers and their models
        /// <summary>
        /// Get all printers name
        /// </summary>
        /// <returns>List of all unique by name printers</returns>
        public IEnumerable<string> GetPrinterTypeList()
        {
            return printerTypeList;
        }
        
        /// <summary>
        /// Take printers models with this name
        /// </summary>
        /// <param name="name">Printer name</param>
        /// <returns>List of printers models</returns>
        public IEnumerable<string> TakeModels(string name)
        {
            return printers.Where(t => t.Name == name).Select(t => t.Model);
        }
        #endregion

        /// <summary>
        /// Take lines for output
        /// </summary>
        /// <param name="printer">Printer</param>
        /// <returns>Lines for output</returns>
        public IEnumerable<string> Print(Printer printer,FileStream fileStream)
        {
            return GetTextForPrint(printer, fileStream);
        }

        #region logger
        /// <summary>
        /// Logs records to file
        /// </summary>
        /// <param name="s">Line for logging</param>
        public void Log(string s)
        {
            logger.Info(s);
        }
        #endregion

        #region private methods
        private IEnumerable<string> GetTextForPrint(Printer p, FileStream fs)
        {
            if (p==null)
            {
                throw new ArgumentNullException($"{nameof(p)} can't be null");
            }
            OnPrint($"Printer {p.Name} - {p.Model} began to print");
            if (!Contains(p))
            {
                throw new InvalidOperationException($"In the list of printers no such {nameof(p)}");
            }
            List<string> resultList = p.GetTextForPrint(fs).ToList();
            OnPrint($"Printer {p.Name} - {p.Model} has finished printing");
            return resultList;
        }
        #endregion

        #region For printer creator

        /// <summary>
        /// Check printer in list of all unique printers
        /// </summary>
        /// <param name="value">Printer's name</param>
        /// <returns></returns>
        public bool isEnumValueDefined(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException($"{nameof(value)} can't be null");
            }
            return Enum.IsDefined(typeof(Printers), value);
        }
        #endregion

    }
}