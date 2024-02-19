using Microsoft.DiaSymReader;
using System.Reflection;

namespace NextUnit.AssemblyReader
{
    /// <summary>
    /// Dummy implementation which is doing nothing.
    /// At the moment we just need it to pass *any* implementation of <see cref="ISymReaderMetadataProvider"/>
    /// to <see cref="SymUnmanagedReaderFactory.CreateReader{T}"/>.
    /// </summary>
    public class SymReaderMetadataProvider : ISymReaderMetadataProvider
    {
        public unsafe bool TryGetStandaloneSignature(int standaloneSignatureToken, out byte* signature, out int length)
        {
            signature = null;
            length = 0;
            return false;
        }

        public bool TryGetTypeDefinitionInfo(int typeDefinitionToken, out string namespaceName, out string typeName, out TypeAttributes attributes)
        {
            namespaceName = null;
            typeName = null;
            attributes = TypeAttributes.NotPublic;
            return false;

        }

        public bool TryGetTypeReferenceInfo(int typeReferenceToken, out string namespaceName, out string typeName)
        {
            namespaceName = null;
            typeName = null;
            return false;
        }
    }
}