using EkaToolFusion.Services.SnippetGenrator.Models;

namespace EkaToolFusion.Services.SnippetGenrators.Processors;

public partial class PredefinedSnippetGenerators
{
    public SnippetInputPayload CharProperty
    {
        get
        {
            return new()
            {
                Header = new()
                {
                    Title = "Char Property Generator",
                    Author = "Ekalaivan Chidambaram - EkaToolFusion",
                    Description = "This will generates char property with auto implemented accessors.",
                    Keywords = ["Char", "Property"],
                    Shortcut = "mkcharprop",
                    SnipperType = SnippetType.Expansion,
                },
                Body = new()
                {
                    Declarations = [
                        new() { ID = "PropName", Default = "MyProp", Editable = true, Tooltip = "Property name to be used for property declaration " }
                    ],
                    CodeBlock = new()
                    {
                        Language = "C#",
                        Kind = "cData",
                        Delimiter = "$",
                        Code = @"public char $PropName$ { get; set; }"
                    }
                }
            };
        }
    }
}