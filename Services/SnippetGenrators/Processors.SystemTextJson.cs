using EkaToolFusion.Services.SnippetGenrator.Models;

namespace EkaToolFusion.Services.SnippetGenrators.Processors;

public partial class PredefinedSnippetGenerators
{
    public SnippetInputPayload SystemTextJsonProperty
    {
        get
        {
            return new()
            {
                Header = new()
                {
                    Title = "System Text Json Property Generator",
                    Author = "Ekalaivan Chidambaram - EkaToolFusion",
                    Description = "This will generates enum property using System.Text.Json string enum converter annotation with auto implemented accessors.",
                    Keywords = ["System Text Json", "Json", "Property"],
                    Shortcut = "mksysjsonprop",
                    SnipperType = SnippetType.Expansion,
                },
                Body = new()
                {
                    ReferencesRequired = true,
                    References = [
                         new() {  Assembly = "System.Text.Json.dll" }
                    ],
                    ImportsRequired = true,
                    Imports = [
                        new() { Namespace = "System.Text.Json.Serialization" }
                    ],
                    DeclarationsRequired = true,
                    Declarations = [
                        new() { ID = "EnumType", Default = "MyEnum", Editable = true, Tooltip = "Enum type to be used for the property type " },
                        new() { ID = "PropName", Default = "MyProp", Editable = true, Tooltip = "Property name to be used for property declaration " }
                    ],
                    CodeBlock = new()
                    {
                        Language = "csharp",
                        Kind = "cData",
                        Delimiter = "$",
                        Code = "[JsonConverter(typeof(JsonStringEnumConverter))]" + Environment.NewLine +
                        "public $EnumType$ $PropName$ { get; set; }"
                    }
                }
            };
        }
    }
}