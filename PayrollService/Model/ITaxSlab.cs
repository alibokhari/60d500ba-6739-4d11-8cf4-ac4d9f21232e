namespace PayrollService.Model
{
    public interface ITaxSlab
    {
        string GetDescription();
        double TaxOnIncome(double income);
        bool WithinTaxSlab(double income);
    }
}
