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

        public virtual ICollection<ContactType> ContactTypes { get; set; }
    }

    public class ContactType
    {
        public int Id { get; set; }
        public string type { get; set; }
        public string FieldOne { get; set; }
        public string FieldTwo { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        
    }
}

