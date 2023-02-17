using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui_DatabaseApp.Model.Entities;

internal class Bin
{
    [Key]
    [Column("equipmentid")]
    [ForeignKey(nameof(Equipment))]
    public Guid EquipmentID { get; init; }

    public Equipment Equipment { get; set; }

}
