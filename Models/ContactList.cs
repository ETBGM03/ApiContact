using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiContact.Models
{
    public class ContactList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "This fields is required")]
        [StringLength(60, ErrorMessage = "The filed must be no longer than 60 digitis")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This fields is required")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "The Phone field can only contain digits.")]
        public string Phone { get; set; }

        [StringLength(400, ErrorMessage = "The filed must be no longer than 400 digitis")]
        public string Comments { get; set; }

        public string ContactType { get; set; }
    }
}
