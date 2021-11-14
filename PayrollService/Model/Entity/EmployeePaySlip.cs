using PayrollService.Model.ValueObject;
using System;

namespace PayrollService.Model.Entity
{
    public class EmployeePaySlip : PaySlip
    {
        public Guid EmployeeID { get; private set; }
        public string FullName { get; private set; }

        public EmployeePaySlip(string fullName, double annualSalary) : base(annualSalary)
        {
            EmployeeID = new Guid();
            FullName = fullName;
        }

    }
}
