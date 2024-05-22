using System.Text;
using EkaToolFusion.Services.SnippetGenrator.Models;
using EkaToolFusion.Services.SnippetGenrators.Processors;
using EkaToolFusion.Services.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EkaToolFusion.Pages;

public partial class SnippetGenrator : ComponentBase
{

    [Inject]
    public IJSRuntime JS { get; set; }

    public SnippetInputPayload Payload { get; set; } = new();
    public SnippetReferenceInputPayload ReferencePayload { get; set; } = new();
    public SnippetImportInputPayload ImportPayload { get; set; } = new();
    public SnippetDeclarationInputPayload DeclarationPayload { get; set; } = new();
    public string CodeSnippet { get; set; }
    public bool CanDownload { get; set; } = false;

    private void GenerateSnippet()
    {
        CodeSnippet = SnippetUtility.Generate(Payload);
        CanDownload = true;
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

    private void OnPreloadSelect(ChangeEventArgs eventArgs)
    {
        var selectedType = Enum.Parse<PredefinedSnippetType>((string)eventArgs.Value);
        Payload = PredefinedSnippetGenerators.Generators[selectedType];

        GenerateSnippet();
    }

    private async Task DownloadFileFromStream()
    {
        var fileContent = SnippetUtility.Generate(Payload);
        var fileStream = new MemoryStream();
        var writter = new StreamWriter(fileStream, Encoding.UTF8);
        writter.Write(fileContent);
        writter.Flush();
        fileStream.Position = 0;

        var fileName = $"{Payload.Header.Title}.snippet";

        using var streamRef = new DotNetStreamReference(stream: fileStream);
        await JS.InvokeVoidAsync("downloadSnippetFile", fileName, streamRef);
    
        ResetInputs();
    }

    private void ResetInputs()
    {
        Payload = new();
        ReferencePayload = new();
        ImportPayload = new();
        DeclarationPayload = new();
        CodeSnippet = string.Empty;
        CanDownload = false;
    }
}