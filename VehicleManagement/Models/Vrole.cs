using System;
using System.Collections.Generic;

namespace VehicleManagement.Models;

public partial class Vrole
{
    public int Vroleid { get; set; }

    public string? Vrolename { get; set; }

    public virtual ICollection<Vuser> Vusers { get; set; } = new List<Vuser>();
}
