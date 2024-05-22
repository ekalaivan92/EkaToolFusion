using EkaToolFusion.Services.SnippetGenrator.Models;

namespace EkaToolFusion.Services.SnippetGenrators.Processors;

public partial class PredefinedSnippetGenerators
{
    public SnippetInputPayload BoolProperty
    {
        get
        {
            return new()
            {
                Header = new()
                {
                    Title = "Bool Property Generator",
                    Author = "Ekalaivan Chidambaram - EkaToolFusion",
                    Description = "This will generates bool / boolean property with auto implemented accessors.",
                    Keywords = ["Bool", "Boolean", "Property"],
                    Shortcut = "mkboolprop",
                    SnipperType = SnippetType.Expansion,
                },
                Body = new()
                {
                    DeclarationsRequired = true,
                    Declarations = [
                        new() { ID = "PropName", Default = "MyProp", Editable = true, Tooltip = "Property name to be used for property declaration " }
                    ],
                    CodeBlock = new()
                    {
                        Language = "csharp",
                        Kind = "cData",
                        Delimiter = "$",
                        Code = @"public bool $PropName$ { get; set; }"
                    }
                }
            };
        }
    }
}