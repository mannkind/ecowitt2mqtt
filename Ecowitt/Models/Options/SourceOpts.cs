using System;
using System.ComponentModel.DataAnnotations;

namespace Ecowitt.Models.Options;

/// <summary>
/// The source options
/// </summary>
public record SourceOpts
{
    public const string Section = "Ecowitt";

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = Section + ":" + nameof(Port) + " is missing")]
    public UInt16 Port { get; init; } = 10000;
}
