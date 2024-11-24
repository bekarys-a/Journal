namespace Journal;

public class DateFile
{
    public string FilePath;

    public DateFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found - ${filePath}");
        }

        FilePath = filePath;
    }

    public IEnumerable<DateOnly> GetDates()
    {
        return File
            .ReadLines(FilePath)
            .Where(line =>
            {
                try
                {
                    DateTime.Parse(line);
                    return true;
                } catch (FormatException)
                {
                    return false;
                }
            })
            .Select(line => DateOnly.FromDateTime(DateTime.Parse(line)));
    }

    public void Sort(SortDirection sortDirection)
    {
        var dates = GetDates();

        dates = sortDirection switch
        {
            SortDirection.Ascending => dates.OrderBy(d => d),
            SortDirection.Descending => dates.OrderByDescending(d => d),
            _ => throw new NotImplementedException()
        };

        Save(dates);
    }

    public void Add(DateOnly dateOnly)
    {
        var dates = GetDates().Append(dateOnly);
        Save(dates);
    }

    public bool Remove(DateOnly dateOnly)
    {
        var dates = GetDates().ToList();
        var result = dates.Remove(dateOnly);
        Save(dates);
        return result;
    }

    public int RemoveAll(DateOnly dateOnly)
    {
        var dates = GetDates().ToList();
        var newDates = GetDates().Where(i => i != dateOnly).ToList();
        Save(newDates);
        return dates.Count() - newDates.Count();
    }

    private void Save(IEnumerable<DateOnly> dates)
    {
        var monthGrouped = dates
            .OrderBy(d => d)
            .GroupBy(d => d.Month);

        var stringDates = new List<string>();

        foreach (var (group, index) in monthGrouped.Select((g, i) => (g, i)))
        {
            foreach (var date in group)
            {
                stringDates.Add(date.ToString());
            }

            if (monthGrouped.Count() != index + 1)
                stringDates.Add("");
        }

        File.WriteAllLines(FilePath, stringDates);
    }
}


public enum SortDirection
{
    Ascending,
    Descending,
}
