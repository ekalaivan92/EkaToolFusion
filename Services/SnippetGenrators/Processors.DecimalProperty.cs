using EkaToolFusion.Services.SnippetGenrator.Models;

namespace EkaToolFusion.Services.SnippetGenrators.Processors;

public partial class PredefinedSnippetGenerators
{
    public SnippetInputPayload DecimalProperty
    {
        get
        {
            return new()
            {
                Header = new()
                {
                    Title = "Decimal Property Generator",
                    Author = "Ekalaivan Chidambaram - EkaToolFusion",
                    Description = "This will generates decimal property with auto implemented accessors.",
                    Keywords = ["decimal", "Property"],
                    Shortcut = "mkdecimalprop",
                    SnipperType = SnippetType.Expansion,
                },
                Body = new()
                {
                    Declarations = [
                        new() { ID = "PropName", Default = "MyProp", Editable = true, Tooltip = "Property name to be used for property declaration " }
                    ],
                    CodeBlock = new()
                    {
                        Language = "csharp",
                        Kind = "cData",
                        Delimiter = "$",
                        Code = @"public decimal $PropName$ { get; set; }"
                    }
                }
            };
        }
    }
}