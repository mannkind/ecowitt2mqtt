namespace Ecowitt.Models.Shared;

/// <summary>
/// The shared key info => slug mapping across the application
/// </summary>
public record SlugMapping
{
    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    public string MAC { get; init; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    public string Slug { get; init; } = string.Empty;
}
