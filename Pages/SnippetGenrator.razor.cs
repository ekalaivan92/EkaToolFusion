using System.Text.Json;
using EkaToolFusion.Services.SnippetGenrator.Models;
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
        CodeSnippet = JsonSerializer.Serialize(Payload, new JsonSerializerOptions { WriteIndented = true });
        CodeSnippet += JsonSerializer.Serialize(ReferencePayload, new JsonSerializerOptions { WriteIndented = true });
        CodeSnippet += JsonSerializer.Serialize(ImportPayload, new JsonSerializerOptions { WriteIndented = true });
        CodeSnippet += JsonSerializer.Serialize(DeclarationPayload, new JsonSerializerOptions { WriteIndented = true });
    }
}