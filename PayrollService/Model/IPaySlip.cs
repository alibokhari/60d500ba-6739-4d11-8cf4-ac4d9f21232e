namespace PayrollService.Model
{
    public interface IPaySlip
    {
        double GrossMonthlyIncome();
        double MonthlyIncomeTax();
        double NetMonthlyIncomeTax();
    }
}
