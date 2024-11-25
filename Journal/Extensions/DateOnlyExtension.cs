namespace Journal.Extensions;


internal static class DateOnlyExtension
{
    internal static DateTime ToDateTime(this DateOnly date)
    {
        return date.ToDateTime(new TimeOnly());
    }
}
