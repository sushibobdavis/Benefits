
namespace Benefits.Business
{
    public class BenefitPlanQuoteRequest
    {
        private Core.BenefitPlan _benefitPlan = null;
        private Core.Client _client = null;
        private Core.Employee _employee = null;

        public BenefitPlanQuoteRequest(Core.BenefitPlan benefitPlan, Core.Client client, Core.Employee employee)
        {
            _benefitPlan = benefitPlan;
            _client = client;
            _employee = employee;
        }

        public Core.BenefitPlanQuote GetQuote()
        {
            return new Core.BenefitPlanQuote() { 
                FinalCost = 1000,
                StartingCost = 1000,
            };
        }
    }
}