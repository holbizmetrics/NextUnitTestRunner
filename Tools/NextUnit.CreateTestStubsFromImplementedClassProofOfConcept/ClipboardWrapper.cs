using System;
using System.Runtime.InteropServices;
using System.Text;

namespace NextUnit.CreateTestStubsFromImplementedClassProofOfConcept
{
	public static class ClipboardWrapper
	{
		[DllImport("User32.dll", SetLastError = true)]
		private static extern bool OpenClipboard(IntPtr hWndNewOwner);

		[DllImport("User32.dll", SetLastError = true)]
		private static extern bool EmptyClipboard();

		[DllImport("User32.dll", SetLastError = true)]
		private static extern IntPtr SetClipboardData(uint uFormat, IntPtr data);

		[DllImport("User32.dll", SetLastError = true)]
		private static extern bool CloseClipboard();

		[DllImport("User32.dll", SetLastError = true)]
		private static extern IntPtr GetClipboardData(uint uFormat);

		public static void CopyText(string text)
		{
			if (!OpenClipboard(IntPtr.Zero))
				return;

			EmptyClipboard();

			var ptr = Marshal.StringToHGlobalUni(text);
			SetClipboardData(13, ptr); // 13 = CF_UNICODETEXT

			CloseClipboard();
			Marshal.FreeHGlobal(ptr);
		}

		public static string GetClipboardText()
		{
			if (!OpenClipboard(IntPtr.Zero))
				return null;

			var ptr = GetClipboardData(13); // 13 = CF_UNICODETEXT
			var text = Marshal.PtrToStringUni(ptr);

			CloseClipboard();

			return text;
		}
	}
}
