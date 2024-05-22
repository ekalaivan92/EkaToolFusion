# EkaToolFusion
My customized tools for development designed with Blazor WebAssembly and .NET 8 to provide an intuitive interface. It is planed to have lot of tools, available tools are

- Advanced Visual Studio Snippet Generator

## Advanced Visual Studio Snippet Generator

This is designed to generate Visual Studio code snippets. Whether you're a seasoned developer or just getting started, this tool will streamline the process of creating reusable code snippets for your projects.

### Features

- **Preloaded Snippets**: Select from a variety of preloaded snippets from the dropdown menu. These predefined templates will auto-populate the input fields, allowing for quick and easy customization.
- **Custom Snippet Creation**: Enter your own details to create a completely custom code snippet. This includes fields for the snippet's metadata and code content.
- **Direct Download**: Once your snippet is ready, download the XML file directly with a single click.
- **User-Friendly Interface**: Built with Blazor WebAssembly, the application provides a responsive and interactive user experience.

### Input Fields

The snippet generator provides the following fields for input:

1. **Name**: The name of the snippet.
2. **Shortcut**: A shortcut key to insert the snippet quickly.
3. **Language**: The programming language of the snippet (e.g., C#, HTML, JavaScript).
4. **Type**: The type of snippet (e.g., expansion, surround, etc.).
5. **Author**: Specifies the name of the snippet author. The Code Snippets Manager displays the name stored in the Author element of the code snippet.
6. **Documentation URL**: A URL pointing to relevant documentation.
7. **Description**: A brief description of what the snippet does.
8. **Keywords**: Keywords associated with the snippet for easy searching.
9. **DLL References**: Any required DLL references.
   1.  **DLL Fullname** : Specifies the name of the assembly referenced by the code snippet. The text value of the Assembly element is either the friendly text name of the assembly, such as System.dll, or its strong name, such as System,Version=1.0.0.1,Culture=neutral,PublicKeyToken=9b35aa323c18d4fb1
   2.  **DLL documentation URL**: The actual documentation url for the reference
10. **Namespace Imports**: Namespaces that should be imported.
    1.  **Name**: Full qualified name of the namespace need to be inserter while generating the code
11. **Placeholders / Declarations**: Placeholder text and declarations to be used inside code block so that can get the input from the user while generating
    1.  **Name**: name of the placeholder
    2.  **Default Name**: What would be the default name you wish to have
    3.  **Tooltip**: Information or tip to show to the user when generating
    4.  **FunctionName**: Specifies a function to execute when the literal or object receives focus in Visual Studio.
12. **Code**: Provides a container for short code blocks.

## How to Use

1. **Select a Preloaded Snippet**: From the dropdown menu, choose a preloaded snippet to see how the fields are populated. You can modify these values to suit your needs.
2. **Enter Custom Snippet Details**: If you prefer to create a snippet from scratch, fill in all the required fields with your information.
3. **Generate the Snippet**: Click on the "Generate Snippet" button. This will create the snippet in XML format based on the provided inputs.
4. **Download the Snippet**: Once generated, click on the "Download Snippet" button to save the XML file to your local machine.

## Example

Here's a quick example to illustrate how you might fill out the fields:

- **Name**: "For Loop"
- **Shortcut**: "forloop"
- **Language**: "C#"
- **Type**: "Expansion"
- **Author**: "John Doe"
- **Documentation URL**: "https://example.com/for-loop-docs"
- **Description**: "Creates a basic for loop structure."
- **Keywords**: "loop, for, iteration"
- **DLL References**: ""
- **Namespace Imports**: "System"
- **Placeholders / Declarations**:
  ```xml
  <Literal>
    <ID>iterator</ID>
    <ToolTip>Iterator variable</ToolTip>
    <Default>i</Default>
  </Literal>
  <Literal>
    <ID>limit</ID>
    <ToolTip>Loop limit</ToolTip>
    <Default>10</Default>
  </Literal>
  ```
- **Code**:
  ```csharp
  for (int $iterator$ = 0; $iterator$ < $limit$; $iterator$++)
  {
      // TODO: Add your code here
  }
  ```

## Installation and Setup

To run this project locally, ensure you have the following prerequisites:

- .NET 8 SDK
- Visual Studio 2022 or later

Clone the repository and navigate to the project directory:

```sh
git clone https://github.com/yourusername/advanced-vs-snippet-generator.git
cd advanced-vs-snippet-generator
```

Run the project:

```sh
dotnet run
```

Open your browser and navigate to `https://localhost:5001` to start using the snippet generator.

## Contributions

Contributions are welcome! Feel free to submit a pull request or open an issue if you encounter any bugs or have suggestions for improvements.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

---

Thank you for using the Advanced Visual Studio Snippet Generator! If you have any questions or need further assistance, please don't hesitate to reach out.

Happy coding! 🚀
