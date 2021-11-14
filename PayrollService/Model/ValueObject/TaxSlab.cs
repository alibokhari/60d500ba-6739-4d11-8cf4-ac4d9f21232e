namespace PayrollService.Model.ValueObject
{
    public class TaxSlab : ITaxSlab
    {
        bool _maxSlab = false;

        public double TaxableIncomeStartValue { get; private set; }
        public double TaxableIncomeEndValue { get; private set; }
        public int Tax { get; private set; }
        public bool Taxable { get; private set; }

        public TaxSlab(double startValue, double endValue, int tax, bool taxable)
        {
            TaxableIncomeStartValue = startValue;
            TaxableIncomeEndValue = endValue;
            Tax = tax;
            Taxable = taxable;
            _maxSlab = endValue == 0;
        }

        public double TaxOnIncome(double income)
        {
            if (!Taxable)
                return 0;

            return ((_maxSlab || income <= TaxableIncomeEndValue ? income : TaxableIncomeEndValue) - TaxableIncomeStartValue + 1) * Tax / 100;
        }

        public string GetDescription()
        {
            return Taxable ? $"{Tax}c for each $1 over ${TaxableIncomeStartValue - 1:###,###}" : "$0";
        }

        public bool AboveTaxSlabStartValue(double income)
        {
            return income > TaxableIncomeStartValue;
        }
    }
}
