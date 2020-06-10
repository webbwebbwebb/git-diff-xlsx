﻿using System;
using System.IO;

namespace git_diff_xlsx
{
    public class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Usage: git-diff-xlsx.exe filename");
                return -1;
            }

            var inputFilePath = args[0];

            try
            {
                using (var input = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    var printer = new ExcelFilePrinter();
                    printer.Print(input, Console.Out);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            
            return 0;
        }
    }
}