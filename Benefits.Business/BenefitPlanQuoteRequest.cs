
namespace Benefits.Business
{
    public class BenefitPlanQuoteRequest
    {
        private Core.BenefitPlan _benefitPlan = null;
        private Core.Employee _employee = null;

        public BenefitPlanQuoteRequest(Core.BenefitPlan benefitPlan, Core.Employee employee)
        {
            _benefitPlan = benefitPlan;
            _employee = employee;
        }

        public Core.BenefitPlanQuote GetQuote()
        {
            Core.BenefitPlanQuote quote = new Core.BenefitPlanQuote(_benefitPlan.Client, _benefitPlan.AvailableAdjustments);
            
            Core.PlanParticipant employeeParticipant = new Core.PlanParticipant(_employee) { 
                InitialCost = _benefitPlan.EmployeeYearlyCost,
                CalculatedCost = _benefitPlan.EmployeeYearlyCost,
                IsDiscounted = false,
            };

            quote.PlanParticipants.Add(employeeParticipant);

            _employee.Dependents.ForEach(x => quote.PlanParticipants.Add(new Core.PlanParticipant(x) { 
                InitialCost = _benefitPlan.DependencyYearlyCost,
                CalculatedCost = _benefitPlan.DependencyYearlyCost,
                IsDiscounted = false,
            }));

            _benefitPlan.AvailableAdjustments.ForEach(x => x.MakeAdjustments(quote));

            return quote;
        }
    }

    
}