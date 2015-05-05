using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benefits.Tests
{
    [TestClass]
    public class BenefitPlanQuoteRequestTests
    {
        [TestMethod]
        public void BenefitPlanQuote_One_Employee_No_Discount()
        {
            Core.BenefitPlan standardBenefitPlan = new Core.BenefitPlan
            {
                DependencyYearlyCost = 500,
                EmployeeYearlyCost = 1000,
                Name = "Standard Benefit Plan"
            };

            Core.Client client = new Core.Client
            {
                Name = "ABC Client",
                PayPeriodsPerYear = 26,
            };

            Core.Employee beneficiary = new Core.Employee
            {
                Name = "John Doe",                
            };

            Business.BenefitPlanQuoteRequest request = new Business.BenefitPlanQuoteRequest(standardBenefitPlan, client, beneficiary);
            Core.BenefitPlanQuote quote = request.GetQuote();

            Assert.AreEqual(quote.StartingCost, standardBenefitPlan.EmployeeYearlyCost);
            Assert.AreEqual(quote.FinalCost, standardBenefitPlan.EmployeeYearlyCost);
        }
    }
}
