
namespace Benefits.Core
{
    public class NameStartsWithAPlanAdjustment : BenefitPlanAdjustmentBase
    {
        public override void MakeAdjustments(BenefitPlanQuote benefitPlanQuote)
        {
            foreach (PlanParticipant participant in benefitPlanQuote.PlanParticipants)
            {
                if (participant.ParticipantInfo.Name.StartsWith("A"))
                {
                    participant.IsDiscounted = true;
                    participant.CalculatedCost *= .9;
                }
            }
        }
    }
}