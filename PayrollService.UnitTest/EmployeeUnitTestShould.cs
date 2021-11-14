using PayrollService.Model;
using PayrollService.Model.Entity;
using System;
using Xunit;

namespace PayrollService.UnitTest
{
    public class EmployeeUnitTestShould
    {
        [Fact]
        public void Return_Gross_Monthly_Income()
        {
            IPaySlip employeePaySlip = new EmployeePaySlip("Ali", 60000);
            var expectedGrossMonthlyIncome = 5000;

            var grossMonthlyIncome = employeePaySlip.GrossMonthlyIncome();

            Assert.Equal(expectedGrossMonthlyIncome, grossMonthlyIncome);
        }

        [Theory]
        [InlineData(18000, 0)]
        [InlineData(35000, 125)]
        [InlineData(60000, 500)]
        [InlineData(95000, 1208.33)]
        [InlineData(135000, 2208.33)]
        public void Return_Monthly_Income_Tax(double income, double expectedMonthlyIncomeTax)
        {
            IPaySlip employeePaySlip = new EmployeePaySlip("Ali", income);

            var monthlyIncomeTax = Math.Round(employeePaySlip.MonthlyIncomeTax(), 2);

            Assert.Equal(expectedMonthlyIncomeTax, monthlyIncomeTax);
        }

        [Theory]
        [InlineData(18000, 1500)]
        [InlineData(35000, 2791.67)]
        [InlineData(60000, 4500)]
        [InlineData(95000, 6708.33)]
        [InlineData(135000, 9041.67)]
        public void Return_Net_Monthly_Income_Tax(double income, double expectedNetMonthlyIncomeTax)
        {
            IPaySlip employeePaySlip = new EmployeePaySlip("Ali", income);

            var netMonthlyIncomeTax = Math.Round(employeePaySlip.NetMonthlyIncomeTax(), 2);

            Assert.Equal(expectedNetMonthlyIncomeTax, netMonthlyIncomeTax);
        }
    }
}
