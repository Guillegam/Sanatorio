using System;
using System.Collections.Generic;

namespace Sanatorio.Models;

public partial class Cita
{
    public int IdCita { get; set; }

    public DateTime Fecha { get; set; }

    public TimeSpan Hora { get; set; }

    public string Tratamiento { get; set; } = null!;

    public int PacientesId { get; set; }

    public int MedicosId { get; set; }
    public virtual Medico Medicos { get; set; } = null!;

    public virtual Paciente Pacientes { get; set; } = null!;
public virtual ICollection<Facturacion> Facturacions { get; set; } = new List<Facturacion>();

}
    