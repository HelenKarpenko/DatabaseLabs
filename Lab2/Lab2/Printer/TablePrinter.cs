using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2.Printer
{
    class TablePrinter<T> where T : IPrintable
    {
        public static void Print(List<T> records)
        {
            if (records == null) throw new ArgumentNullException("Records is null.");
            if (records.Count == 0) return;

            List<string> colNames = records.FirstOrDefault().GetAllFieldsNames();
            List<int> widths = records.FirstOrDefault().GetAllFieldsWidth();
            int tableWidth = 0;
            foreach (var width in widths)
            {
                tableWidth += width + 1;
            }

            if (colNames.Count <= 0) return;

            PrintLine(tableWidth);
            PrintRow(colNames, widths);
            PrintLine(tableWidth);

            foreach (T rec in records)
            {
                PrintRow(rec.GetAllFieldsValues(), widths);
                PrintLine(tableWidth);
            }
        }

        public static void Print(T record)
        {
            List<string> colNames = record.GetAllFieldsNames();
            List<int> widths = record.GetAllFieldsWidth();

            if (colNames.Count <= 0) return;

            int tableWidth = 0;
            foreach (var width in widths)
            {
                tableWidth += width + 1;
            }

            PrintLine(tableWidth);
            PrintRow(colNames, widths);
            PrintLine(tableWidth);
            PrintRow(record.GetAllFieldsValues(), widths);
            PrintLine(tableWidth);
        }

        private static void PrintLine(int tableWidth)
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        private static void PrintRow(List<string> columns, List<int> widths)
        {
            string row = "|";

            for (int i = 0; i < columns.Count; i++)
            {
                row += AlignCentre(columns[i], widths[i]) + "|";
            }

            Console.WriteLine(row);
        }

        private static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}
