using System;
using System.Collections.Generic;

namespace CRUD_IngenieriaWeb.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public bool? isAuthenticated { get; set; }

    public string? Clave { get; set; }
}
