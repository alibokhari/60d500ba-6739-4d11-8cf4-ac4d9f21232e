using PayrollService.Model;
using PayrollService.Model.ValueObject;
using Xunit;

namespace PayrollService.UnitTest
{
    public class TaxSlabUnitTestShould
    {
        [Theory]
        [InlineData(0, 20000, 0, false, "$0")]
        [InlineData(20001, 40000, 10, true, "10c for each $1 over $20,000")]
        [InlineData(40001, 80000, 20, true, "20c for each $1 over $40,000")]
        [InlineData(80001, 180000, 30, true, "30c for each $1 over $80,000")]
        [InlineData(180001, 0, 40, true, "40c for each $1 over $180,000")]
        public void Return_Tax_Description(double startValue, double endValue, int tax, bool taxable, string expectedTaxDescription)
        {
            ITaxSlab taxSlab = new TaxSlab(startValue, endValue, tax, taxable);

            var taxDescription = taxSlab.GetDescription();

            Assert.Equal(expectedTaxDescription, taxDescription);
        }

        [Theory]
        [InlineData(0, 20000, 0, false, 15000, 0)]
        [InlineData(20001, 40000, 10, true, 65000, 2000)]
        [InlineData(40001, 80000, 20, true, 60000, 4000)]
        [InlineData(80001, 180000, 30, true, 130000, 15000)]
        [InlineData(180001, 0, 40, true, 210000, 12000)]
        public void Return_Tax_On_Income(double startValue, double endValue, int tax, bool taxable, double income, double expectedTax)
        {
            ITaxSlab taxSlab = new TaxSlab(startValue, endValue, tax, taxable);

            var taxOnIncome = taxSlab.TaxOnIncome(income);

            Assert.Equal(expectedTax, taxOnIncome);
        }

        [Theory]
        [InlineData(0, 20000, 0, false, 15000, true)]
        [InlineData(20001, 40000, 10, true, 35000, true)]
        [InlineData(40001, 80000, 20, true, 75000, true)]
        [InlineData(80001, 180000, 30, true, 70000, false)]
        [InlineData(180001, 0, 40, true, 210000, true)]
        public void Return_With_In_TaxSlab_Status(double startValue, double endValue, int tax, bool taxable, double income, bool expectedWithInTaxSlabStatus)
        {
            ITaxSlab taxSlab = new TaxSlab(startValue, endValue, tax, taxable);

            var withInTaxSlabStatus = taxSlab.WithinTaxSlab(income);

            Assert.Equal(expectedWithInTaxSlabStatus, withInTaxSlabStatus);
        }
    }
}
