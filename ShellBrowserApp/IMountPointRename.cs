// Copyright (c) 0x5BFA. All rights reserved.

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.UI.Shell
{
	public unsafe struct IMountPointRename : IComIID
	{
		private void** lpVtbl;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT Rename(PCWSTR lpRootPathName, PCWSTR lpVolumeName)
		{
			return ((delegate* unmanaged[Stdcall]<IMountPointRename*, PCWSTR, PCWSTR, HRESULT>)lpVtbl[3])
				((IMountPointRename*)Unsafe.AsPointer(ref this), lpRootPathName, lpVolumeName);
		}

		public static ref readonly Guid Guid
		{
			get
			{
				// E2BA9629-B18F-4E3A-ABA5-42D879E79C80
				ReadOnlySpan<byte> data =
				[
					0x29, 0x96, 0xBA, 0xE2,
					0x8F, 0xB1,
					0x3A, 0x4E,
					0xAB, 0xA5,
					0x42, 0xD8, 0x79, 0xE7, 0x9C, 0x80
				];

				Debug.Assert(data.Length == sizeof(Guid));
				return ref Unsafe.As<byte, Guid>(ref MemoryMarshal.GetReference(data));
			}
		}
	}
}
