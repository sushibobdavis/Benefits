using System.Linq;
using System.Collections.Generic;

namespace Benefits.Core
{
    public class BenefitPlanQuote
    {
        private Core.Client _client = null;

        public BenefitPlanQuote(Core.Client client, List<BenefitPlanAdjustmentBase> benefitPlanAdjustments)
        {
            _client = client;
            PlanParticipants = new List<PlanParticipant>();
            BenefitPlanAdjustments = benefitPlanAdjustments;
        }

        public double StartingCost { 
            get 
            { 
               return PlanParticipants.Select(x=>x.InitialCost).Sum();
            } 
        }
        
        public double FinalCost { 
            get 
            {
                return PlanParticipants.Select(x => x.CalculatedCost).Sum();
            } 
        }

        public double AmountPerPayPeriod
        {
            get
            {
                return FinalCost / _client.PayPeriodsPerYear;
            }
        }

        public List<BenefitPlanAdjustmentBase> BenefitPlanAdjustments { get; set; }
        public List<PlanParticipant> PlanParticipants { get; set; }
    }
}