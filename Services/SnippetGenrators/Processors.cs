using EkaToolFusion.Services.SnippetGenrator.Models;

namespace EkaToolFusion.Services.SnippetGenrators.Processors;

public partial class PredefinedSnippetGenerators
{
    public static PredefinedSnippetGenerators Generators = new();

    public Dictionary<PredefinedSnippetType, SnippetInputPayload> Snippets
        => new(){
            { PredefinedSnippetType.NewtonSoftJson,  NewtonSoftJsonProperty },
            { PredefinedSnippetType.Int,  IntProperty },
            { PredefinedSnippetType.UInt,  UIntProperty },
            { PredefinedSnippetType.NInt,  NIntProperty },
            { PredefinedSnippetType.NUint,  NUIntProperty },
            { PredefinedSnippetType.Long,  LongProperty },
            { PredefinedSnippetType.ULong,  ULongProperty },
            { PredefinedSnippetType.Bool,  BoolProperty },
            { PredefinedSnippetType.Byte,  ByteProperty },
            { PredefinedSnippetType.ByteArray,  ByteArrayProperty },
            { PredefinedSnippetType.SByte,  SByteProperty },
            { PredefinedSnippetType.Char,  CharProperty },
            { PredefinedSnippetType.Decimal,  DecimalProperty },
            { PredefinedSnippetType.Double,  DoubleProperty },
            { PredefinedSnippetType.Float,  FloatProperty },
            { PredefinedSnippetType.Short,  ShortProperty },
            { PredefinedSnippetType.UShort,  UShortProperty },
        };

    public SnippetInputPayload this[PredefinedSnippetType type]
        => Snippets[type];
}