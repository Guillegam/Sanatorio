using System;
using System.Collections.Generic;

namespace Sanatorio.Models;

public partial class Paciente
{
    public int IdPaciente { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int Dni { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
}
