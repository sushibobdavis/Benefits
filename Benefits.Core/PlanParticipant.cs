
namespace Benefits.Core
{
    public class PlanParticipant
    {
        public PlanParticipant(Person participantInfo)
        {
            ParticipantInfo = participantInfo;
        }

        public Person ParticipantInfo { get; internal set; }
        public double InitialCost { get; set; }
        public double CalculatedCost { get; set; }
        public bool IsDiscounted { get; set; }
    }
}