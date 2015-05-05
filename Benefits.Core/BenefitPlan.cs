
namespace Benefits.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class BenefitPlan
    {
        public int Id { get; set; }
        public double EmployeeYearlyCost { get; set; }
        public double DependencyYearlyCost { get; set; }
        public string Name { get; set; }
    }
}