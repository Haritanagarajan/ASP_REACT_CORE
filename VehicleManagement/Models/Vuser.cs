using System;
using System.Collections.Generic;

namespace VehicleManagement.Models;

public partial class Vuser
{
    public int Vuserid { get; set; }

    public string? Vusername { get; set; }

    public string? Vemail { get; set; }

    public string? Vpassword { get; set; }

    public string? VconfirmPassword { get; set; }

    public long? Vmobile { get; set; }

    public DateTime? Vcreated { get; set; }

    public DateTime? VlastLoginDate { get; set; }

    public int? Vroleid { get; set; }

    public virtual ICollection<CarDetail> CarDetails { get; set; } = new List<CarDetail>();

    public virtual Vrole? Vrole { get; set; }
}
