using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui_DatabaseApp.Model.Entities;

internal class Equipment
{
    [Column("equipmentid")]
    public Guid EquipmentID { get; init; }

    [Column("name")]
    public required string Name { get; set; }

    [Column("quantity")]
    public required int Quantity { get; set; }

    [Column("festival")]
    [ForeignKey(nameof(Festival))]
    public Guid FestivalID { get; set; }

    public Festival Festival { get; set; }
}
