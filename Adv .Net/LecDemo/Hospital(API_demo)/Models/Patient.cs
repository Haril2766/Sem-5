using System;
using System.Collections.Generic;

namespace Hospital.Models;

public partial class Patient
{
    public int PatientId { get; set; }

    public string PatientName { get; set; } = null!;

    public string? ContactNo { get; set; }

    public int? Age { get; set; }

    public bool EarlierOperation { get; set; }

    public string BloodGroup { get; set; } = null!;
}
