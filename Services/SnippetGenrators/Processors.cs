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
        };

    public SnippetInputPayload this[PredefinedSnippetType type] 
        => Snippets[type];
}