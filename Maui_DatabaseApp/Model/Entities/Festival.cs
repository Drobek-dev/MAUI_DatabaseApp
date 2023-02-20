using System.ComponentModel.DataAnnotations.Schema;

namespace Maui_DatabaseApp.Model.Entities;

public class Festival
{
    [Column("festivalid")]
    public Guid FestivalID { get; init; }
    [Column("name")]
    public required string Name { get; set; }
    [Column("location")]
    public required string Location { get; set; }
    [Column("startdate")]
    public required DateOnly StartDate { get; set; }
    [Column("enddate")]
    public required DateOnly EndDate { get; set; }

}
