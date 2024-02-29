using System.Reflection.Emit;
using System.Reflection;

namespace NextUnit.Core
{
    public class DelegateTypeFactory
    {
        private readonly ModuleBuilder m_module;

        public DelegateTypeFactory()
        {
#if NET5_0_OR_GREATER
            // Define a dynamic assembly using the current application domain
            var assemblyName = new AssemblyName("DelegateTypeFactory");
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                assemblyName, AssemblyBuilderAccess.RunAndCollect);
            // Define a dynamic module in this assembly
            m_module = assemblyBuilder.DefineDynamicModule("DelegateTypeFactoryModule");
#else
                var assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(
                    new AssemblyName("DelegateTypeFactory"), AssemblyBuilderAccess.RunAndCollect);
                m_module = assembly.DefineDynamicModule("DelegateTypeFactory");
#endif
        }

        public Type CreateDelegateType(MethodInfo method)
        {
            string nameBase = string.Format("{0}{1}", method.DeclaringType.Name, method.Name);
            string name = GetUniqueName(nameBase);

            var typeBuilder = m_module.DefineType(
                name, TypeAttributes.Sealed | TypeAttributes.Public, typeof(MulticastDelegate));

            var constructor = typeBuilder.DefineConstructor(
                MethodAttributes.RTSpecialName | MethodAttributes.HideBySig | MethodAttributes.Public,
                CallingConventions.Standard, new[] { typeof(object), typeof(IntPtr) });
            constructor.SetImplementationFlags(MethodImplAttributes.CodeTypeMask);

            var parameters = method.GetParameters();

            var invokeMethod = typeBuilder.DefineMethod(
                "Invoke", MethodAttributes.HideBySig | MethodAttributes.Virtual | MethodAttributes.Public,
                method.ReturnType, parameters.Select(p => p.ParameterType).ToArray());
            invokeMethod.SetImplementationFlags(MethodImplAttributes.CodeTypeMask);

            for (int i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                invokeMethod.DefineParameter(i + 1, ParameterAttributes.None, parameter.Name);
            }

            return typeBuilder.CreateType();
        }

        private string GetUniqueName(string nameBase)
        {
            int number = 2;
            string name = nameBase;
            while (m_module.GetType(name) != null)
                name = nameBase + number++;
            return name;
        }
    }
}
