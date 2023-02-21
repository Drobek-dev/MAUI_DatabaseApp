using System.ComponentModel.DataAnnotations.Schema;

namespace Maui_DatabaseApp.Model.Entities;

public class Equipment
{
    [Column("equipmentid")]
    public Guid EquipmentID { get; init; }

    [Column("name")]
    public required string Name { get; set; }

    [Column("quantity")]
    public required int Quantity { get; set; }
    [Column("isinbin")]
    public required bool IsInBin { get; set; }

    [Column("festivalid")]
    [ForeignKey(nameof(Festival))]
    public Guid FestivalID { get; set; }

    public Festival Festival { get; set; }
}
