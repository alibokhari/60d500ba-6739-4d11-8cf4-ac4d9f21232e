using PayrollService.Model;
using PayrollService.Model.Entity;
using PayrollService.Model.Exceptions;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PayrollService
{
    class Program
    {
        static void Main(string[] args)
        {
            var readMe = new StringBuilder();
            readMe.AppendLine("**************Welcome to Payroll Service**************");
            readMe.AppendLine("**********Generate employee monthly pay slip**********");
            readMe.AppendLine("Sample Input: GenerateMonthlyPayslip \"Mary Song\" 60000");
            Console.WriteLine(readMe.ToString());

            try
            {
                while (true)
                {
                    try
                    {
                        Console.Write(">");
                        var command = Console.ReadLine();
                        GeneratePaySlip(command);
                    }
                    catch (InvalidInputException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: { ex.Message }");
            }
            Console.Read();

            Console.ReadLine();
        }

        private static void GeneratePaySlip(string input)
        {
            var commandPattern = @"^(GenerateMonthlyPayslip \""[a-zA-Z ]+\"" \d+)$";

            if (!Regex.IsMatch(input, commandPattern))
            {
                throw new InvalidInputException("Invalid input");
            }

            var fullName = Regex.Match(input, "\"(.*?)\"").Value.Replace("\"", string.Empty);
            var annualSalary = Convert.ToDouble(Regex.Match(input, "(\\d+)").Value);

            var paySlip = new EmployeePaySlip(fullName, annualSalary);

            var output = new StringBuilder();
            output.AppendLine($"Monthly Payslip for: {fullName}");
            output.AppendLine($"Gross Monthly Income: ${paySlip.GrossMonthlyIncome()}");
            output.AppendLine($"Monthly Income Tax: ${paySlip.MonthlyIncomeTax()}");
            output.AppendLine($"Net Monthly Income: ${paySlip.NetMonthlyIncomeTax()}");
            Console.WriteLine(output.ToString());
        }
    }
}
