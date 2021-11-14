# 60d500ba-6739-4d11-8cf4-ac4d9f21232e
<b>Payroll Service</b>

Generates the employee monthly pay slip

<b>Application Design</b>

PayrollService is the main project to receive the command for the commandline

It validates the input with the correct pattern i.e GenerateMonthlyPayslip "Mary Song" 60000<br/>
It will produce the output i.e<br/>
Monthly Payslip for: "Mary Song"<br/>
Gross Monthly Income: $5000<br/>
Monthly Income Tax: $500<br/>
Net Monthly Income: $4500<br/>

If not valid will throw InvalidInputException which will handle by the app to show the output

There is a Model of EmployeePaySlip that is a main entity of the payroll domain, it is inherated from the Payroll class that is implementing IPayroll interface. By doing so we have decouple on Payslip business logic from Employee class by following (OCP - Open Close Principal) also by implementing interface it will allow to mock the object. This extensibility feature will provide the ability to create different payslip generators of the different periods.

A separate TaxSlab class introduced as a value object which can define the tax slab<br/>
1.TaxableIncomeStartValue ie. $20,000<br/>
2.TaxableIncomeEndValue ie. $40,0000<br/>
3.TaxRate i.e 10%<br/>
4.Taxable i.e true/false<br/>
5.By setting the TaxableIncomeEndValue to zero will mark it as the max slab<br/>
6.It can calculate the TaxOnIncome per the slab<br/>
7.It can detect income is valid for slab<br/>
8.This slab class can be fetched from repository/cache to build the TaxSlabs for the payslip

Payslip class can return<br/>
1.GrossMonthlyIncome
2.MonthlyIncomeTax
3.NetMonthlyIncomeTax
4.LoadTaxSlabs (from any source)

<b>PayrollService.UnitTest</b>

It provide the 100% coverage using the xUnit and Coverlet to gernerate coverage report<br/>
There are two unit tests<br/>
1.TaxSlab to validate (Return_Tax_Description, Return_Tax_On_Income, Return_With_In_TaxSlab_Status)
2.Employee to validate (Return_Gross_Monthly_Income, Return_Monthly_Income_Tax, Return_Net_Monthly_Income_Tax)
