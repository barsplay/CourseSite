using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site.Models
{using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Site.Models
{
        public class GeneralForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Ваш текст")]
        [Column(TypeName = "TEXT")]
        public string? SearchText { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Паттерн")]
        [Column(TypeName = "TEXT")]
        public string? Pattern { get; set; }

        [Column(TypeName = "INTEGER")]
        public int Counts { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public List<List<int>>? AlgOut { get; set; }
    }
}

    public class GeneralForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Ваш текст")]
        [Column(TypeName = "TEXT")]
        public string? SearchText { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Паттерн")]
        [Column(TypeName = "TEXT")]
        public string? Pattern { get; set; }

        [Column(TypeName = "INTEGER")]
        public int Counts { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public List<List<int>>? AlgOut { get; set; }
    }
}
