using SchoolManagerModel.Entities.UserModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagerModel.Entities;

public class AssignedSubject
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required Subject Subject { get; set; }
    public required Student Student { get; set; }
    public bool GotGrade { get; set; }
    public int? Mark { get; set; }
}