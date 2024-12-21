using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagerModel.Entities;

public class Class
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }

    public Class()
    {

    }

    public Class(int year, char schoolClass)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(year, 1);

        schoolClass = schoolClass.ToString().ToUpper()[0];

        if (!Char.IsLetter(schoolClass))
        {
            throw new FormatException("School class must be a letter!");
        }

        Name = $"{year}/{schoolClass}";
    }
}
