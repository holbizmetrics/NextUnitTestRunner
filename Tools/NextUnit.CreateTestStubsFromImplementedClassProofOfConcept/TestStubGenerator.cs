using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;

namespace NextUnit.CreateTestStubsFromImplementedClassProofOfConcept
{
	public static class TestStubGenerator
	{
		public static string GenerateTestStubsFromClipboard()
		{
			// Get the text from the clipboard
			string clipboardText = ClipboardWrapper.GetClipboardText();
			if (string.IsNullOrWhiteSpace(clipboardText))
			{
				throw new InvalidOperationException("Clipboard is empty or does not contain valid text.");
			}

			// Parse the original code
			var tree = CSharpSyntaxTree.ParseText(clipboardText);
			var root = tree.GetRoot() as CompilationUnitSyntax;

			// Remove all comments first
			var rootWithoutComments = RemoveAllComments(root);

			// Modify the class and methods
			var newRoot = GenerateTestStubs(rootWithoutComments);

			// Add Arrange, Act, Assert comments
			newRoot = AddTestComments(newRoot);

			// Format the modified code
			var workspace = new AdhocWorkspace();
			var formattedRoot = Formatter.Format(newRoot, workspace);

			var formattedCode = formattedRoot.ToFullString();
			formattedCode = AddTestCommentsTextual(formattedCode);

			return formattedCode;
		}

		private static string AddTestCommentsTextual(string formattedCode)
		{
			string pattern = @"public void \w+\(\)\s+{}";

			MatchCollection method = Regex.Matches(formattedCode, pattern, RegexOptions.IgnoreCase);
			foreach (Match match in method)
			{

				string methodName = match.Value.Split(' ')[2].Split('(')[0];
				string replacement = $"public void {methodName}()\n" +
					"\t{\n" +
					"\t\t// Arrange\n" +
					"\t\t// Act\n" +
					"\t\t// Assert\n" +
					"\t}";
				formattedCode = formattedCode.Replace(match.Value, replacement);
			}
			return formattedCode;
		}


		private static CompilationUnitSyntax GenerateTestStubs(CompilationUnitSyntax root)
		{
			// Find the class declaration
			var classDeclaration = root.DescendantNodes().OfType<ClassDeclarationSyntax>().FirstOrDefault();
			if (classDeclaration == null)
			{
				throw new InvalidOperationException("No public class found in the provided text.");
			}

			// Add "Tests" suffix to the class name and ensure the class is public
			var newClassDeclaration = classDeclaration
				.WithIdentifier(SyntaxFactory.Identifier(classDeclaration.Identifier.Text + "Tests"))
				.WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
				.WithBaseList(null) // Remove inheritance and interfaces
				.WithAttributeLists(new SyntaxList<AttributeListSyntax>()) // Remove all attribute usages
				.WithMembers(SyntaxFactory.List<MemberDeclarationSyntax>());

			// Remove constructors and include only public methods and properties
			var membersToAdd = classDeclaration.Members
				.Where(m => !(m is ConstructorDeclarationSyntax))
				.Where(m => m.Modifiers.Any(SyntaxKind.PublicKeyword))
				.Select<MemberDeclarationSyntax, MemberDeclarationSyntax>(m =>
				{
					if (m is MethodDeclarationSyntax method)
					{
						var newMethod = SyntaxFactory.MethodDeclaration(
								attributeLists: new SyntaxList<AttributeListSyntax>(),
								modifiers: SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)),
								returnType: SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
								explicitInterfaceSpecifier: method.ExplicitInterfaceSpecifier,
								identifier: SyntaxFactory.Identifier(method.Identifier.Text + "Test"),
								typeParameterList: method.TypeParameterList,
								parameterList: SyntaxFactory.ParameterList(),
								constraintClauses: method.ConstraintClauses,
								body: SyntaxFactory.Block(),
								expressionBody: null)
							.AddAttributeLists(SyntaxFactory.AttributeList(SyntaxFactory.SingletonSeparatedList(SyntaxFactory.Attribute(SyntaxFactory.ParseName("Test")))));
						return newMethod;
					}
					else if (m is PropertyDeclarationSyntax property)
					{
						var newMethod = SyntaxFactory.MethodDeclaration(
							attributeLists: new SyntaxList<AttributeListSyntax>(),
							modifiers: SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)),
							returnType: SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
							explicitInterfaceSpecifier: null,
							identifier: SyntaxFactory.Identifier(property.Identifier.Text + "Test"),
							typeParameterList: null,
							parameterList: SyntaxFactory.ParameterList(),
							constraintClauses: new SyntaxList<TypeParameterConstraintClauseSyntax>(),
							body: SyntaxFactory.Block(),
							expressionBody: null)
						.AddAttributeLists(SyntaxFactory.AttributeList(SyntaxFactory.SingletonSeparatedList(SyntaxFactory.Attribute(SyntaxFactory.ParseName("Test")))));
						return newMethod;
					}
					else if (m is ClassDeclarationSyntax nestedClass)
					{
						var newNestedClass = nestedClass
							.WithIdentifier(SyntaxFactory.Identifier(nestedClass.Identifier.Text + "Tests"))
							.WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
							.WithBaseList(null)
							.WithAttributeLists(new SyntaxList<AttributeListSyntax>())
							.WithMembers(SyntaxFactory.List<MemberDeclarationSyntax>());

						var nestedMembersToAdd = nestedClass.Members
							.Where(nm => !(nm is ConstructorDeclarationSyntax))
							.Where(nm => nm.Modifiers.Any(SyntaxKind.PublicKeyword))
							.Select<MemberDeclarationSyntax, MemberDeclarationSyntax>(nm =>
							{
								if (nm is MethodDeclarationSyntax nestedMethod)
								{
									var newNestedMethod = SyntaxFactory.MethodDeclaration(
											attributeLists: new SyntaxList<AttributeListSyntax>(),
											modifiers: SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)),
											returnType: SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
											explicitInterfaceSpecifier: nestedMethod.ExplicitInterfaceSpecifier,
											identifier: SyntaxFactory.Identifier(nestedMethod.Identifier.Text + "Test"),
											typeParameterList: nestedMethod.TypeParameterList,
											parameterList: SyntaxFactory.ParameterList(),
											constraintClauses: nestedMethod.ConstraintClauses,
											body: SyntaxFactory.Block(),
											expressionBody: null)
										.AddAttributeLists(SyntaxFactory.AttributeList(SyntaxFactory.SingletonSeparatedList(SyntaxFactory.Attribute(SyntaxFactory.ParseName("Test")))));
									return newNestedMethod;
								}
								else if (nm is PropertyDeclarationSyntax nestedProperty)
								{
									var newNestedMethod = SyntaxFactory.MethodDeclaration(
										attributeLists: new SyntaxList<AttributeListSyntax>(),
										modifiers: SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)),
										returnType: SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword)),
										explicitInterfaceSpecifier: null,
										identifier: SyntaxFactory.Identifier(nestedProperty.Identifier.Text + "Test"),
										typeParameterList: null,
										parameterList: SyntaxFactory.ParameterList(),
										constraintClauses: new SyntaxList<TypeParameterConstraintClauseSyntax>(),
										body: SyntaxFactory.Block(),
										expressionBody: null)
									.AddAttributeLists(SyntaxFactory.AttributeList(SyntaxFactory.SingletonSeparatedList(SyntaxFactory.Attribute(SyntaxFactory.ParseName("Test")))));
									return newNestedMethod;
								}
								else
								{
									return null; // Return null for non-public fields
								}
							})
							.Where(nm => nm != null);

						newNestedClass = newNestedClass.AddMembers(nestedMembersToAdd.ToArray());
						return newNestedClass;
					}
					else
					{
						return null; // Return null for non-public fields
					}
				})
				.Where(m => m != null);

			newClassDeclaration = newClassDeclaration.AddMembers(membersToAdd.ToArray());

			var newRoot = root.ReplaceNode(classDeclaration, newClassDeclaration);

			return newRoot;
		}

		private static CompilationUnitSyntax AddTestComments(CompilationUnitSyntax root)
		{
			var rewriter = new TestCommentAdder();
			return (CompilationUnitSyntax)rewriter.Visit(root);
		}

		private class TestCommentAdder : CSharpSyntaxRewriter
		{
			public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
			{
				if (node.Body != null)
				{
					var newBody = SyntaxFactory.Block(
						SyntaxFactory.ParseStatement("// Arrange\n"),
						SyntaxFactory.ParseStatement("// Act\n"),
						SyntaxFactory.ParseStatement("// Assert\n")
					);
					return node.WithBody(newBody);
				}
				return node;
			}
		}

		private static CompilationUnitSyntax RemoveAllComments(CompilationUnitSyntax root)
		{
			var rewriter = new CommentRemover();
			return (CompilationUnitSyntax)rewriter.Visit(root);
		}

		private class CommentRemover : CSharpSyntaxRewriter
		{
			public override SyntaxTrivia VisitTrivia(SyntaxTrivia trivia)
			{
				if (trivia.IsKind(SyntaxKind.SingleLineCommentTrivia) ||
					trivia.IsKind(SyntaxKind.MultiLineCommentTrivia) ||
					trivia.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) ||
					trivia.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia))
				{
					return default;
				}
				return base.VisitTrivia(trivia);
			}
		}
	}
}
