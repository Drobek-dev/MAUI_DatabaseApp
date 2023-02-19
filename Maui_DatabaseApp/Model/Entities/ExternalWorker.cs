using System.ComponentModel.DataAnnotations.Schema;

namespace Maui_DatabaseApp.Model.Entities;

internal class ExternalWorker
{
    [Column("externalworkerid")]
    public Guid ExternalWorkerID { get; init; }

    [Column("name")]
    public required string Name { get; set; }

    [Column("phonenumber")]
    public required string PhoneNumber { get; set; }

    [Column("function")]
    public required string Function { get; set; }

    [Column("festivalid")]
    [ForeignKey(nameof(Festival))]
    public Guid FestivalID { get; set; }
    public Festival Festival { get; set; }
}
