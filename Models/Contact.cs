using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiContact.Models
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required")]
        [StringLength(60, ErrorMessage = "The field must be no longer than 60 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Phone field is required")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "The Phone field can only contain digits.")]
        public string Phone { get; set; }

        [StringLength(400, ErrorMessage = "The field must be no longer than 400 characters")]
        public string Comments { get; set; }

        public ICollection<ExtraFields> ExtraFields { get; set; }

        // Clave foránea
        public int ContactTypeId { get; set; }
        [ForeignKey("ContactTypeId")]
        
        //Propiedad de navegación
        public ContactType ContactType { get; set; }
    }

    public class ContactType
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }

    public class ExtraFields
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }

        // Clave foránea
        public int ContactId { get; set; }
        [ForeignKey("ContactId")]
        // Propiedad de navegación
        public Contact Contact { get; set; }
    }
}