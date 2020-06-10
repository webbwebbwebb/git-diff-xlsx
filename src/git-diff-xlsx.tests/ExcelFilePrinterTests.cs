using System;
using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace git_diff_xlsx.tests
{
    [TestFixture]
    public class ExcelFilePrinterTests
    {
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void CanPrintPlainTextCellValues()
        {
            var sut = new ExcelFilePrinter();
            using(var input = new MemoryStream(Resources.SingleSheetWithPlainTextValues))
            using(var output = new StringWriter())
            {
                sut.Print(input, output);
                Approvals.Verify(output.ToString());
            }
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void CanPrintNumericCellValues()
        {
            var sut = new ExcelFilePrinter();
            using(var input = new MemoryStream(Resources.SingleSheetWithNumericValues))
            using(var output = new StringWriter())
            {
                sut.Print(input, output);
                Approvals.Verify(output.ToString());
            }
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void CanPrintCalculatedCellValues()
        {
            var sut = new ExcelFilePrinter();
            using(var input = new MemoryStream(Resources.SingleSheetWithCalculatedValues))
            using(var output = new StringWriter())
            {
                sut.Print(input, output);
                Approvals.Verify(output.ToString());
            }
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void CanPrintCalculatedAndFormattedCellValues()
        {
            var sut = new ExcelFilePrinter();
            using(var input = new MemoryStream(Resources.SingleSheetWithCalculatedAndFormattedValues))
            using(var output = new StringWriter())
            {
                sut.Print(input, output);
                Approvals.Verify(output.ToString());
            }
        }
    }
}
