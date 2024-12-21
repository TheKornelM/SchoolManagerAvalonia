using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolManagerModel.Entities.UserModel;

namespace SchoolManagerModel.Entities;

public class Subject
{
    // Required for Entity Framework
    public Subject()
    {
    }

    public Subject(string name, Class @class, Teacher teacher)
    {
        Name = name;
        Class = @class;
        Teacher = teacher;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required string Name { get; set; }
    public required Class Class { get; set; }
    public required Teacher Teacher { get; set; }
}