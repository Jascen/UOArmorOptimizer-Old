namespace ArmorPicker.Models
{
    public class ArmorCandidate
    {
        public Armor Armor { get; set; }
        public Candidate ColdCandidate { get; set; }
        public Candidate EnergyCandidate { get; set; }
        public Candidate FireCandidate { get; set; }
        public Candidate PhysicalCandidate { get; set; }
        public Candidate PoisonCandidate { get; set; }
    }
}