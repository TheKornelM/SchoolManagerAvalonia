using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagerModel.Entities.UserModel;

public class Student
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required User User { get; set; }

    public required Class Class { get; set; }

    public List<AssignedSubject>? Subjects { get; set; }

}
