using System.Collections.Generic;

namespace Benefits.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class BenefitPlan
    {
        public BenefitPlan(Core.Client client)
        {
            Client = client;
            AvailableAdjustments = new List<BenefitPlanAdjustmentBase>();
        }

        public BenefitPlan(Core.Client client, List<BenefitPlanAdjustmentBase> availableAdjustments) : this(client)
        {
            AvailableAdjustments = availableAdjustments;
        }

        public int Id { get; set; }
        public double EmployeeYearlyCost { get; set; }
        public double DependencyYearlyCost { get; set; }
        public string Name { get; set; }
        public Core.Client Client { get; internal set; }
        public List<BenefitPlanAdjustmentBase> AvailableAdjustments { get; internal set; }
    }

}