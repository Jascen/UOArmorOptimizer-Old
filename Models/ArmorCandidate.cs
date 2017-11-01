namespace ArmorOptimizer.Models
{
    public class ArmorCandidate
    {
        public ArmorViewModel ArmorViewModel { get; set; }
        public Candidate ColdCandidate { get; set; }
        public Candidate EnergyCandidate { get; set; }
        public Candidate FireCandidate { get; set; }
        public Candidate PhysicalCandidate { get; set; }
        public Candidate PoisonCandidate { get; set; }
    }
}