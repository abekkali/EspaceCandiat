using System.ComponentModel.DataAnnotations;

namespace EspaceCandiat.Models
{
    public class Candidat
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Nom { get; set; }

        [Required]
        [StringLength(30)]
        public string Prenom { get; set; }

        [Required]
        [StringLength(40)]
        public string Email { get; set; }

        [Required]
        [StringLength(14)]
        public string Telephone { get; set; }

        [Required]
        [StringLength(10)]
        public string NiveauEtude { get; set; }

        public int AnneesExperience { get; set; }

        [StringLength(30)]
        public string DernierEmployeur { get; set; }

        public string Cv { get; set; }
    }
}
