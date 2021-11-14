namespace PayrollService.Model
{
    public interface ITaxSlab
    {
        string GetDescription();
        double TaxOnIncome(double income);
        bool AboveTaxSlabStartValue(double income);
    }
}
