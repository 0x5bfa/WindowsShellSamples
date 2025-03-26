// Copyright (c) Files Community
// Licensed under the MIT License.

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Windows.Win32
{
	public static unsafe partial class PInvoke
	{
		[LibraryImport("Shell32", EntryPoint = "#823", SetLastError = true)]
		public static unsafe partial int SHGetFolderTypeDescription(Guid* a1, Guid* a2, void** a3);
	}

	namespace Foundation
	{
		[DebuggerDisplay("{Value,h}")]
		public readonly partial struct HRESULT {}
	}
}
