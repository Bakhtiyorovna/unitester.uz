using Unitester_Domain.Enums;

namespace Unitester_Domain.Entities.Tests;

public class Test : Auditable
{
    public string Question { get; set; } = String.Empty;
    public string VariantA { get; set; } = String.Empty;
    public string VariantB { get; set; } = String.Empty;
    public string VariantC { get; set; } = String.Empty;
    public string VariantD { get; set; } = String.Empty;
    public string RightVariant { get; set; } = String.Empty;
    public TestType Type { get; set; }
    public string Description { get; set; } = String.Empty;
}
