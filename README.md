# Minerals.CodeBuilder

![GitHub License](https://img.shields.io/github/license/SzymonHalucha/Minerals.CodeBuilder?style=for-the-badge)
![NuGet Version](https://img.shields.io/nuget/v/Minerals.CodeBuilder?style=for-the-badge)
![NuGet Downloads](https://img.shields.io/nuget/dt/Minerals.CodeBuilder?style=for-the-badge)

[Package on nuget.org](https://www.nuget.org/packages/Minerals.CodeBuilder/)

This small package provides functionalities for source code generation with management of code indentation.

## Features

- Writes lines and code blocks with automatic indentation management.
- Iterates over sequences and write code within each iteration.
- Creates custom logic for iteration elements.
- Implements conditional code generation based on boolean values.
- Adds standard auto-generated headers and attributes.

## Installation

Add the Minerals.CodeBuilder nuget package to your C# project using the following methods:

### 1. Project file definition

```xml
<PackageReference Include="Minerals.CodeBuilder" Version="0.2.1" />
```

### 2. dotnet command

```bat
dotnet add package Minerals.CodeBuilder
```

## Usage

### Write, WriteLine, OpenBlock, CloseBlock

```csharp
var builder = new Minerals.CodeBuilder(); // Default Parameters: (int indentationSize = 4, int indentationLevel = 0)
builder.Write("namespace ExampleNamespace").OpenBlock();
builder.WriteLine("public class ExampleClass").OpenBlock();
builder.WriteLine("public int[] ExampleArray = new int[] { 1, 2, 3, 4, 5 };");
builder.CloseBlock().CloseBlock();
// or
builder.CloseAllBlocks();
```

### Iteration

```csharp
var builder = new CodeBuilder();
IEnumerable<string> enumerable = ["int a = 0;", "int b = 1;", "int c = 2;", "int d = 3;", "int e = 4;"];
builder.Write("public class ExampleClass").OpenBlock();
builder.WriteIteration(enumerable);
builder.CloseBlock();
```

### Custom Iteration

```csharp
var builder = new CodeBuilder();
IEnumerable<string> enumerable = ["a = 0;", "b = 1;", "c = 2;", "d = 3;", "e = 4;"];
builder.Write("public class ExampleClass").OpenBlock();
builder.WriteIteration(enumerable, (builder1, item, next) =>
{
    builder1.WriteLine("int ").Write(item);
});
builder.CloseBlock();
```

### Inline Conditions

```csharp
var builder = new CodeBuilder();
var success = true;
builder.If(success)?.WriteLine("private bool _success = true;");
```

### CodeBuilderExtensions class

```csharp
var builder = new CodeBuilder();
builder.AddAutoGeneratedHeader(Assembly.GetExecutingAssembly());

// Output:

// <auto-generated>
// This code was generated by a tool.
// Name: {{AssemblyTitle}}
// Version: {{AssemblyInformationalVersion}}
// </auto-generated>
```

```csharp
var builder = new CodeBuilder();
builder.AddAutoGeneratedAttributes(typeof(ClassDeclarationSyntax));
builder.Write("public class ExampleClass { }");

// Output:

[global::System.Diagnostics.DebuggerNonUserCode]
[global::System.Runtime.CompilerServices.CompilerGenerated]
[global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class ExampleClass { }
```

### CodeBuilderHelper class

```csharp
string className = CodeBuilderHelper.GetIdentifierNameOf(baseTypeNode);
string? namespaceName = CodeBuilderHelper.GetNamespaceOf(syntaxNode); // Block Namespace or File Scoped Namespace
IEnumerable<string> modifiers = CodeBuilderHelper.GetModifiersOf(memberNode); // All modifiers
IEnumerable<string>? accessModifiers = CodeBuilderHelper.GetAccessModifiersOf(memberNode); // Only private, protected, internal, public
IEnumerable<string>? bases = CodeBuilderHelper.GetBasesOf(baseTypeNode); // Base class and interfaces
IEnumerable<string>? usings = CodeBuilderHelper.GetUsingsOf(syntaxNode); // File usings and namespace usings
```

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [branches on this repository](https://github.com/SzymonHalucha/Minerals.CodeBuilder/branches).

## Authors

- **Szymon Hałucha** - Maintainer

See also the list of [contributors](https://github.com/SzymonHalucha/Minerals.CodeBuilder/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE](./LICENSE) file for details.
