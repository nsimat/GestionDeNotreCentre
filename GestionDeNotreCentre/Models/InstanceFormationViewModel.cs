using GestionDeCentreDAL.Models;

namespace GestionDeNotreCentre.Models
{
    public class InstanceFormationViewModel
    {
        public Personne Personne { get; set; }
        public Formation Formation { get; set; }
        public InstanceFormation InstanceFormation { get; set; }
    }
}