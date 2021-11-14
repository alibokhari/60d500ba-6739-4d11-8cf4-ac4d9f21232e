using System.Collections.Generic;

namespace PayrollService.Model.ValueObject
{
    public abstract class PaySlip : IPaySlip
    {
        private IList<ITaxSlab> taxSlabs;
        
        public double AnnualSalary { get; private set; }

        protected PaySlip(double annualSalary) 
        { 
            AnnualSalary = annualSalary;

            LoadTaxSlabs();
        }

        public double GrossMonthlyIncome()
        {
            return AnnualSalary / 12;
        }

        public double MonthlyIncomeTax()
        {
            double monthlyIncomeTax = 0;
            
            foreach (var taxSlab in taxSlabs)
            {
                if (!taxSlab.AboveTaxSlabStartValue(AnnualSalary))
                    break;
                monthlyIncomeTax += taxSlab.TaxOnIncome(AnnualSalary);
            }

            return monthlyIncomeTax > 0 ? monthlyIncomeTax /12 : 0;
        }

        private void LoadTaxSlabs()
        {
            //Configurable, can be fetched from repository/cache

            taxSlabs = new List<ITaxSlab>();
            taxSlabs.Add(new TaxSlab(0, 20000, 0, false));
            taxSlabs.Add(new TaxSlab(20001, 40000, 10, true));
            taxSlabs.Add(new TaxSlab(40001, 80000, 20, true));
            taxSlabs.Add(new TaxSlab(80001, 180000, 30, true));
            taxSlabs.Add(new TaxSlab(180000, 0, 40, true));
        }

        public double NetMonthlyIncomeTax()
        {
            return GrossMonthlyIncome() - MonthlyIncomeTax();
        }
    }
}
