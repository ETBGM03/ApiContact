using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiContact.Models
{

    public class Contact {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required")]
        [StringLength(60, ErrorMessage = "The filed must be no longer than 60 digitis")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Thie Phone field is required")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "The Phone field can only contain digits.")]
        public string Phone { get; set; }

        [StringLength(400, ErrorMessage = "The filed must be no longer than 400 digitis")]
        public string Comments { get; set; }

        [Required(ErrorMessage = "The ContacType field is required")]
        public ContactType Type { get; set; }

    }

    public class ContactType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Type { get; set; }

        public string Birthday { get; set; } // For person
        public string Address { get; set; } // For person
        public string Website { get; set; } // For contact public
        public string OpenHours { get; set; } // For contact public
        public string OrganizationName { get; set; } // For contact private
        public string Industry { get; set; } // For contact private
    }

}

