using System.Text.Json;
using EkaToolFusion.Services.SnippetGenrator.Models;
using EkaToolFusion.Services.Utilities;
using Microsoft.AspNetCore.Components;

namespace EkaToolFusion.Pages;

public partial class SnippetGenrator : ComponentBase
{
    public SnippetInputPayload Payload { get; set; } = new();
    public SnippetReferenceInputPayload ReferencePayload { get; set; } = new();
    public SnippetImportInputPayload ImportPayload { get; set; } = new();
    public SnippetDeclarationInputPayload DeclarationPayload { get; set; } = new();
    public string CodeSnippet { get; set; }

    private void GenerateSnippet()
    {
        CodeSnippet = SnippetUtility.Generate(Payload);
    }

    private void UpdateDeclaration()
    {
        DeclarationPayload.ID = DeclarationPayload.ID.Trim();

        var hasEntry = Payload.Body.Declarations.Any(x => x.ID.Equals(DeclarationPayload.ID, StringComparison.InvariantCultureIgnoreCase));
        if (!hasEntry)
        {
            Payload.Body.Declarations = Payload.Body.Declarations.Append(DeclarationPayload);
        }

        DeclarationPayload = new();
    }

    private void UpdateReference()
    {
        ReferencePayload.Assembly = ReferencePayload.Assembly.Trim();
        
        var hasEntry = Payload.Body.References.Any(x => x.Assembly.Equals(ReferencePayload.Assembly, StringComparison.InvariantCultureIgnoreCase));
        if (!hasEntry)
        {
            Payload.Body.References = Payload.Body.References.Append(ReferencePayload);
        }

        ReferencePayload = new();
    }

    private void UpdateImport()
    {
        ImportPayload.Namespace = ImportPayload.Namespace.Trim();

        var hasEntry = Payload.Body.Imports.Any(x => x.Namespace.Equals(ImportPayload.Namespace, StringComparison.InvariantCultureIgnoreCase));
        if (!hasEntry)
        {
            Payload.Body.Imports = Payload.Body.Imports.Append(ImportPayload);
        }

        ImportPayload = new();
    }
}