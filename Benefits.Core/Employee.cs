using System.Collections.Generic;

namespace Benefits.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Employee : Person
    {
        public Employee()
        {
            this.Dependents = new List<Dependent>();
        }

        public List<Dependent> Dependents { get; set; }
    }
}