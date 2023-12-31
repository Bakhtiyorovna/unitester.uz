﻿using Unitester_Domain.Enums;

namespace Unitester_Domain.Entities.Tests;

public class Test : Auditable
{
    public string test { get; set; } = String.Empty;
    public string VariantA { get; set; } = String.Empty;
    public string VariantB { get; set; } = String.Empty;
    public string VariantC { get; set; } = String.Empty;
    public string VariantD { get; set; } = String.Empty;
    public long DirectionId { get; set; }
    public RightVariant RightVariant { get; set; }
    public TestType Type { get; set; }
}
