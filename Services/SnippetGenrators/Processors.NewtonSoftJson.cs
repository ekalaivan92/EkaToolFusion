using EkaToolFusion.Services.SnippetGenrator.Models;

namespace EkaToolFusion.Services.SnippetGenrators.Processors;

public partial class PredefinedSnippetGenerators
{
    public SnippetInputPayload NewtonSoftJsonProperty
    {
        get
        {
            return new()
            {
                Header = new()
                {
                    Title = "Newton Json Property Generator",
                    Author = "Ekalaivan Chidambaram - EkaToolFusion",
                    Description = "This will generates enum property using NewtonSoft Json string enum converter annotation with auto implemented accessors.",
                    Keywords = ["NewtonSoftJson", "Json", "Property"],
                    Shortcut = "mkjsonprop",
                    SnipperType = SnippetType.Expansion,
                },
                Body = new()
                {
                    ReferencesRequired = true,
                    References = [
                         new() {  Assembly = "Newtonsoft.Json.dll" }
                    ],
                    ImportsRequired = true,
                    Imports = [
                        new() {  Namespace = "Newtonsoft.Json" },
                        new() { Namespace = "Newtonsoft.Json.Converters" }
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
                        Code = "[JsonConverter(typeof(StringEnumConverter))]" + Environment.NewLine +
                        "public $EnumType$ $PropName$ { get; set; }"
                    }
                }
            };
        }
    }
}