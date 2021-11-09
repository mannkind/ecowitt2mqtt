using System.Collections.Generic;
using Ecowitt.Models.Shared;
using TwoMQTT.Interfaces;

namespace Ecowitt.Models.Options;

/// <summary>
/// The shared options across the application
/// </summary>
public record SharedOpts : ISharedOpts<SlugMapping>
{
    public const string Section = "Ecowitt";

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public UnitSystem UnitSystem { get; init; } = UnitSystem.Metric;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="SlugMapping"></typeparam>
    /// <returns></returns>
    public List<SlugMapping> Resources { get; init; } = new();
}
