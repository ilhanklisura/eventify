namespace Eventify.Backend.Models.Response.Common;

/// <summary>Bazna lista modela (Items).</summary>
public class ListModel<T>
{
    public ListModel() => Items = new List<T>();

    public ListModel(IEnumerable<T> items) => Items = items?.ToList() ?? new List<T>();

    public IEnumerable<T> Items { get; set; }
}
