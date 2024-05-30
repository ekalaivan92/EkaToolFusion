using EkaToolFusion.Services.SnippetGenrator.Models;

namespace EkaToolFusion.Services.SnippetGenrators.Processors;

public partial class PredefinedSnippetGenerators
{
    public static PredefinedSnippetGenerators Generators = new();

    public Dictionary<PredefinedSnippetType, SnippetInputPayload> Snippets
        => new(){
            { PredefinedSnippetType.NewtonSoftJson,  NewtonSoftJsonProperty },
            { PredefinedSnippetType.Int,  IntProperty },
            { PredefinedSnippetType.Long,  LongProperty },
            { PredefinedSnippetType.Bool,  BoolProperty },
            { PredefinedSnippetType.Byte,  ByteProperty },
            { PredefinedSnippetType.ByteArray,  ByteArrayProperty },
            { PredefinedSnippetType.SByte,  SByteProperty },
            { PredefinedSnippetType.Char,  CharProperty },
            { PredefinedSnippetType.Decimal,  DecimalProperty },
            { PredefinedSnippetType.Double,  DoubleProperty },
            { PredefinedSnippetType.Float,  FloatProperty },
            { PredefinedSnippetType.UInt,  UIntProperty },
            { PredefinedSnippetType.NInt,  NIntProperty },
            { PredefinedSnippetType.NUint,  NUIntProperty },
            { PredefinedSnippetType.ULong,  ULongProperty },
            { PredefinedSnippetType.Short,  ShortProperty },
        };

    public SnippetInputPayload this[PredefinedSnippetType type]
        => Snippets[type];
}