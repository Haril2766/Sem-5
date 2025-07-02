using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LecDemo.Models;

public partial class Student
{
    [Key]
    public int StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public int Age { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }
}
