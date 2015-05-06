using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Benefits.Tests
{
    [TestClass]
    public class BenefitPlanQuoteRequestTests
    {
        private readonly int DEPENDENCY_YEARLY_COST = 500;
        private readonly int EMPLOYEE_YEARLY_COST = 1000;
        private Core.BenefitPlan standardBenefitPlan = null;
        private Core.Employee beneficiary = null;
        private Core.Client client = null;
        private Business.BenefitPlanQuoteRequest request = null;
        private Core.BenefitPlan benefitPlanWithDiscount = null;

        [TestInitialize]
        public void Initialize()
        {
            client = new Core.Client
            {
                Name = "ABC Client",
                PayPeriodsPerYear = 26,
            };

            standardBenefitPlan = new Core.BenefitPlan(client)
            {
                DependencyYearlyCost = DEPENDENCY_YEARLY_COST,
                EmployeeYearlyCost = EMPLOYEE_YEARLY_COST,
                Name = "Standard Benefit Plan"
            };

            benefitPlanWithDiscount = new Core.BenefitPlan(client, new System.Collections.Generic.List<Core.BenefitPlanAdjustmentBase>() { new Core.NameStartsWithAPlanAdjustment() })
            {
                DependencyYearlyCost = DEPENDENCY_YEARLY_COST,
                EmployeeYearlyCost = EMPLOYEE_YEARLY_COST,
                Name = "Standard Plan With Discount"
            };

            beneficiary = new Core.Employee
            {
                Name = "John Doe",
            };
        }

        [TestMethod]
        public void BenefitPlanQuote_One_Employee_No_Discounts()
        {
            request = new Business.BenefitPlanQuoteRequest(standardBenefitPlan, beneficiary);
            Core.BenefitPlanQuote quote = request.GetQuote();

            Assert.AreEqual(quote.StartingCost, quote.FinalCost);
            Assert.AreEqual(quote.PlanParticipants.Count, beneficiary.Dependents.Count + 1);
            Assert.AreEqual(quote.PlanParticipants.Where(x => x.IsDiscounted == true).Count(), 0);
        }

        [TestMethod]
        public void BenefitPlanQuote_One_Employee_One_Dependent_No_Discounts()
        {
            beneficiary.Dependents.Add(new Core.Dependent { Name = "Jane Dependent" });

            request = new Business.BenefitPlanQuoteRequest(standardBenefitPlan, beneficiary);
            Core.BenefitPlanQuote quote = request.GetQuote();

            Assert.AreEqual(quote.StartingCost, quote.FinalCost);
            Assert.AreEqual(quote.PlanParticipants.Count, beneficiary.Dependents.Count + 1);
            Assert.AreEqual(quote.PlanParticipants.Where(x => x.IsDiscounted == true).Count(), 0);
        }

        [TestMethod]
        public void BenefitPlanQuote_One_Employee_Two_Dependents_No_Discounts()
        {
            beneficiary.Dependents.Add(new Core.Dependent { Name = "Jane Dependent"});
            beneficiary.Dependents.Add(new Core.Dependent { Name = "Joey Dependent" });

            request = new Business.BenefitPlanQuoteRequest(standardBenefitPlan, beneficiary);
            Core.BenefitPlanQuote quote = request.GetQuote();

            Assert.AreEqual(quote.StartingCost, quote.FinalCost);
            Assert.AreEqual(quote.PlanParticipants.Count, beneficiary.Dependents.Count + 1);
            Assert.AreEqual(quote.PlanParticipants.Where(x => x.IsDiscounted == true).Count(), 0);
        }

        [TestMethod]
        public void BenefitPlanQuote_One_Employee_With_Discount()
        {
            Core.Employee employeeWithDiscount = new Core.Employee
            {
                Name = "Alfred Discount"
            };

            request = new Business.BenefitPlanQuoteRequest(benefitPlanWithDiscount, employeeWithDiscount);
            Core.BenefitPlanQuote quote = request.GetQuote();

            Assert.IsTrue(quote.FinalCost < quote.StartingCost);
            Assert.AreEqual(quote.PlanParticipants.Count, 1);
            Assert.AreEqual(quote.PlanParticipants.Where(x => x.IsDiscounted == true).Count(), 1);
        }

        [TestMethod]
        public void BenefitPlanQuote_Employee_And_Dependent_With_Discount()
        {
            Core.Employee employeeWithDiscount = new Core.Employee
            {
                Name = "Alfred Discount",
                Dependents = new System.Collections.Generic.List<Core.Dependent>()
                {
                    new Core.Dependent {Name = "Amy Discount"},
                    new Core.Dependent {Name = "Nora Dependent"},
                }                
            };

            request = new Business.BenefitPlanQuoteRequest(benefitPlanWithDiscount, employeeWithDiscount);
            Core.BenefitPlanQuote quote = request.GetQuote();

            Assert.IsTrue(quote.FinalCost < quote.StartingCost);
            Assert.AreEqual(quote.PlanParticipants.Count, employeeWithDiscount.Dependents.Count + 1);
            Assert.AreEqual(quote.PlanParticipants.Where(x => x.IsDiscounted == true && x.CalculatedCost < x.InitialCost).Count(), 2);
        }

    }
}
