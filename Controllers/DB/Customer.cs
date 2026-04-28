using System;
using System.Collections.Generic;
using Library.DB;

namespace Controllers;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
