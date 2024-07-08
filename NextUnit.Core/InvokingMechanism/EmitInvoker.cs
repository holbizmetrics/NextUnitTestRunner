using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NextUnit.Core.InvokingMechanism
{
	public class EmitInvoker : Invoker, IInvoker
	{
		private Delegate _dynamicMethodDelegate;
		private MethodInfo _targetMethod;

		public EmitInvoker()
		{

		}

		public EmitInvoker(MethodInfo methodInfo)
		{
			Initialize(methodInfo);
		}

		private void Initialize(MethodInfo methodInfo)
		{
			_targetMethod = methodInfo;
			_dynamicMethodDelegate = CreateDynamicMethodDelegate(methodInfo);
		}

		public void Set(MethodInfo methodInfo)
		{
			Initialize(methodInfo);
		}

		private Delegate CreateDynamicMethodDelegate(MethodInfo methodInfo)
		{
			Type[] paramTypes = methodInfo.GetParameters().Select(p => p.ParameterType).ToArray();
			Type returnType = methodInfo.ReturnType;

			// Create a dynamic method that matches the signature of the original method
			var dynamicMethod = new DynamicMethod(
		"Dynamic" + methodInfo.Name,
				returnType,
				paramTypes,
				typeof(EmitInvoker).Module,
				skipVisibility: true);  // Allow accessing private/internal methods

			ILGenerator il = dynamicMethod.GetILGenerator();

			// If the method is an instance method, load the instance before arguments
			if (!methodInfo.IsStatic)
			{
				il.Emit(OpCodes.Ldarg_0); // Load the instance (this)
				for (int i = 0; i < paramTypes.Length; i++)
				{
					il.Emit(OpCodes.Ldarg, i + 1); // Load each argument
				}
			}
			else
			{
				for (int i = 0; i < paramTypes.Length; i++)
				{
					il.Emit(OpCodes.Ldarg, i); // Load each argument for static methods
				}
			}

			// Use Call or Callvirt based on whether the method is virtual or not
			if (methodInfo.IsVirtual)
			{
				il.EmitCall(OpCodes.Callvirt, methodInfo, null);
			}
			else
			{
				il.EmitCall(OpCodes.Call, methodInfo, null);
			}

			il.Emit(OpCodes.Ret);

			// Create a delegate from the dynamic method
			return dynamicMethod.CreateDelegate(
				Expression.GetDelegateType(paramTypes.Concat(new Type[] { returnType }).ToArray())
			);
		}

		//private Delegate CreateDynamicMethodDelegate(MethodInfo methodInfo)
		//{
		//	var dynamicMethod = new DynamicMethod(
		//		"Dynamic" + methodInfo.Name,
		//		methodInfo.ReturnType,
		//		methodInfo.GetParameters().Select(p => p.ParameterType).ToArray(),
		//		typeof(EmitInvoker).Module);

		//	ILGenerator il = dynamicMethod.GetILGenerator();
		//	for (int i = 0; i < methodInfo.GetParameters().Length; i++)
		//	{
		//		il.Emit(OpCodes.Ldarg, i);
		//	}

		//	il.EmitCall(OpCodes.Call, methodInfo, null);
		//	il.Emit(OpCodes.Ret);

		//	return dynamicMethod.CreateDelegate(
		//		Expression.GetDelegateType(
		//			methodInfo.GetParameters().Select(p => p.ParameterType).Append(methodInfo.ReturnType).ToArray()
		//		));
		//}

		public object Invoke(Delegate @delegate, object instance, object[] parameters)
		{
			_dynamicMethodDelegate = CreateDynamicMethodDelegate(@delegate.GetMethodInfo());
			Instance = instance;
			return _dynamicMethodDelegate.DynamicInvoke(parameters);
		}
	}
}
