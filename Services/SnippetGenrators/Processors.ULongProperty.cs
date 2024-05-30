using EkaToolFusion.Services.SnippetGenrator.Models;

namespace EkaToolFusion.Services.SnippetGenrators.Processors;

public partial class PredefinedSnippetGenerators
{
    public SnippetInputPayload ULongProperty
    {
        get
        {
            return new()
            {
                Header = new()
                {
                    Title = "ULong Property Generator",
                    Author = "Ekalaivan Chidambaram - EkaToolFusion",
                    Description = "This will generates ulong property with auto implemented accessors.",
                    Keywords = ["ULong", "Property"],
                    Shortcut = "mkulongprop",
                    SnipperType = SnippetType.Expansion,
                },
                Body = new()
                {
                    DeclarationsRequired = true,
                    Declarations = [
                        new() { ID = "PropName", Default = "MyProp", Editable = true, Tooltip = "Property name to be used for property declaration" }
                    ],
                    CodeBlock = new()
                    {
                        Language = "csharp",
                        Kind = "cData",
                        Delimiter = "$",
                        Code = @"public ulong $PropName$ { get; set; }"
                    }
                }
            };
        }
    }
}