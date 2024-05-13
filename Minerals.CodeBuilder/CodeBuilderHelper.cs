namespace Minerals
{
    public static class CodeBuilderHelper
    {
        public static string GetIdentifierNameOf(SyntaxNode baseTypeNode)
        {
            return ((BaseTypeDeclarationSyntax)baseTypeNode).Identifier.ValueText;
        }

        public static string? GetNamespaceOf(SyntaxNode syntaxNode)
        {
            var nameSyntax = syntaxNode.FirstAncestorOrSelf<NamespaceDeclarationSyntax>()?.Name;
            nameSyntax ??= syntaxNode.FirstAncestorOrSelf<FileScopedNamespaceDeclarationSyntax>()?.Name;
            return nameSyntax?.ToString();
        }

        public static IEnumerable<string> GetModifiersOf(SyntaxNode memberNode)
        {
            return ((MemberDeclarationSyntax)memberNode).Modifiers.Select(x => x.ValueText);
        }

        public static IEnumerable<string>? GetAccessModifiersOf(SyntaxNode memberNode)
        {
            return ((MemberDeclarationSyntax)memberNode).Modifiers.Where(x => x.IsKind(SyntaxKind.PrivateKeyword)
                    || x.IsKind(SyntaxKind.ProtectedKeyword)
                    || x.IsKind(SyntaxKind.InternalKeyword)
                    || x.IsKind(SyntaxKind.PublicKeyword))
                    .Select(x => x.ValueText);
        }

        public static IEnumerable<string>? GetBasesOf(SyntaxNode baseTypeNode)
        {
            return ((BaseTypeDeclarationSyntax)baseTypeNode).BaseList?.Types.Select(x => x.Type.ToString());
        }

        public static IEnumerable<string>? GetUsingsOf(SyntaxNode syntaxNode)
        {
            var fileUsings = syntaxNode.FirstAncestorOrSelf<CompilationUnitSyntax>()?.Usings
                .Where(x => x.GlobalKeyword.IsKind(SyntaxKind.None))
                .Select(x => x.Name!.ToString());

            var namespaceUsings = syntaxNode.FirstAncestorOrSelf<NamespaceDeclarationSyntax>()?.Usings
                .Where(x => x.GlobalKeyword.IsKind(SyntaxKind.None))
                .Select(x => x.Name!.ToString());

            if (fileUsings is not null && namespaceUsings is not null)
            {
                return fileUsings.Concat(namespaceUsings);
            }
            else if (fileUsings is not null)
            {
                return fileUsings;
            }
            else if (namespaceUsings is not null)
            {
                return namespaceUsings;
            }
            return null;
        }
    }
}