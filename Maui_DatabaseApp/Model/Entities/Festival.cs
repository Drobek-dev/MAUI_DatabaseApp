using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui_DatabaseApp.Model.Entities;

internal class Festival
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
    public required DateOnly SndDate { get; set; }

}
