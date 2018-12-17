using System.Collections.Generic;

namespace Lab2.Printer
{
    interface IPrintable
    {
        List<string> GetAllFieldsNames();
        List<int> GetAllFieldsWidth();
        List<string> GetAllFieldsValues();
    }
}
