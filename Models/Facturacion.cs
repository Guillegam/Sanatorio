using System;
using System.Collections.Generic;

namespace Sanatorio.Models;

public partial class Facturacion
{
    public int IdFacturacion { get; set; }

    public DateOnly Fecha { get; set; }

    public decimal Monto { get; set; }

    public string Tratamiento { get; set; } = null!;

    public int CitasId { get; set; }

    public virtual Cita Citas { get; set; } = null!;
}
