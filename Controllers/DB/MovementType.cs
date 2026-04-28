using System;
using System.Collections.Generic;
using Library.DB;

namespace Controllers;

public partial class MovementType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
}
