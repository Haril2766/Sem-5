using System;
using System.Collections.Generic;

namespace Hospital.Models;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string DoctorName { get; set; } = null!;

    public string Degree { get; set; } = null!;

    public string? Expirience { get; set; }

    public string Specialization { get; set; } = null!;

    public int? Age { get; set; }

    public DateOnly? DateOfBirth { get; set; }
}
