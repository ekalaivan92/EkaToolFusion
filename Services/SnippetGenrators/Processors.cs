using EkaToolFusion.Services.SnippetGenrator.Models;

namespace EkaToolFusion.Services.SnippetGenrators.Processors;

public partial class PredefinedSnippetGenerators
{
    public static PredefinedSnippetGenerators Generators = new(); 

    public Dictionary<PredefinedSnippetType, SnippetInputPayload> Snippets
        => new(){ 
            { PredefinedSnippetType.NewtonSoftJson,  NewtonSoftJsonProperty },
            { PredefinedSnippetType.Int,  IntProperty },
        };

    public SnippetInputPayload this[PredefinedSnippetType type] 
        => Snippets[type];
}