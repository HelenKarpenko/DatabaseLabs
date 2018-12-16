using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Printer
{
    interface IPrintable
    {
        List<string> GetAllFieldsNames();
        List<int> GetAllFieldsWidth();
        List<string> GetAllFieldsValues();
    }
}
