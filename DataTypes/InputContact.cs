using ApiContact.Models;

namespace ApiContact.DataTypes;

public class InputContact
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Comments { get; set; }
    public string ContactType { get; set; }
    public ICollection<InputExtraFields> ExtraFields { get; set; }
        
}

public class InputPatchContact
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Comments { get; set; }
}

public class InputExtraFields
{
    public string Field { get; set; }
    public string Value { get; set; }
}

