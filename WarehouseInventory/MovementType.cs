using System;
using System.Collections.Generic;

namespace WarehouseInventory;

public partial class MovementType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
}
