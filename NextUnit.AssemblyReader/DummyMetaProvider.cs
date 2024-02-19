using Microsoft.DiaSymReader;
using System.Reflection;

namespace NextUnit.AssemblyReader
{
    /// <summary>
    /// Dummy metadata provider.
    /// </summary>
    public class DummyMetadataProvider : ISymReaderMetadataProvider
    {
        public unsafe bool TryGetStandaloneSignature(int standaloneSignatureToken, out byte* signature, out int length)
        {
            signature = null;
            length = 0;
            return false;
        }

        public unsafe bool TryGetTypeDefinitionInfo(int typeDefinitionToken, out string namespaceName, out string typeName, out TypeAttributes attributes)
        {
            namespaceName = null;
            typeName = null;
            attributes = TypeAttributes.NotPublic;
            return false;
        }

        public unsafe bool TryGetTypeReferenceInfo(int typeReferenceToken, out string namespaceName, out string typeName)
        {
            namespaceName = null;
            typeName = null;
            return false;
        }
    }
}
