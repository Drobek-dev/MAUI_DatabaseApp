using System.ComponentModel.DataAnnotations.Schema;

namespace Maui_DatabaseApp.Model.Entities;

public class Festival : ObservableObject
{
    [Column("festivalid")]
    public Guid FestivalID { get; init; }
    [Column("name")]
    public required string Name { get; set; }
    [Column("location")]
    public required string Location { get; set; }


    DateOnly startDate;
    [Column("startdate")]
    public required DateOnly StartDate
    {
        get => startDate;
        set
        {
            if (value == startDate)
                return;
            startDate = value;
            OnPropertyChanged(nameof(StartDate));
        }
    }

    DateOnly endDate;
    [Column("enddate")]
    public required DateOnly EndDate
    {
        get => endDate;
        set
        {
            if (value == endDate)
                return;
            endDate = value;
            OnPropertyChanged(nameof(EndDate));
        }
    }

}
