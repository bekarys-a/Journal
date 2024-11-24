namespace Journal;
public class DateService
{
    private DateFile _dateFile;

    public DateService(DateFile dateFile)
    {
        _dateFile = dateFile;
    }

    public Dictionary<DateOnly, int> RepeatDays()
    {
        return _dateFile.GetDates().GroupBy(d => d).ToDictionary(
            d => d.Key,
            d => d.Count()
        );
    }

    public Dictionary<int, int> RepeatMonth(int? year = null)
    {
        return _dateFile.GetDates()
            .Where(d => year.HasValue ? year.Value == d.Year : true)
            .GroupBy(d => d.Month)
            .ToDictionary(
                d => d.Key,
                d => d.Count()
            );
    }

    public Dictionary<int, int> RepeatYears()
    {
        return _dateFile.GetDates().GroupBy(d => d.Year).ToDictionary(
            d => d.Key,
            d => d.Count()
        );
    }

    public IEnumerable<DateOnly> GetDates()
    {
        return _dateFile.GetDates();
    }

    public void Sort(SortDirection sortDirection)
    {
        _dateFile.Sort(sortDirection);
    }

    public void Add(DateOnly dateOnly)
    {
        _dateFile.Add(dateOnly);
    }

    public bool Remove(DateOnly dateOnly)
    {
        return _dateFile.Remove(dateOnly);
    }

    public int RemoveAll(DateOnly dateOnly)
    {
        return _dateFile.RemoveAll(dateOnly);
    }
}
