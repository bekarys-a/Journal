namespace Journal.Extensions;
public static class DateOnlyExtension
{
    public static DateTime ToDateTime(this DateOnly date)
    {
        return date.ToDateTime(new TimeOnly());
    }
}
