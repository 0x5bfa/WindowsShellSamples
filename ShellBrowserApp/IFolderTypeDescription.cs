// Copyright (c) Files Community
// Licensed under the MIT License.

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.UI.Shell
{
	public unsafe struct IFolderTypeDescription : IComIID
	{
		private void** lpVtbl;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT GetExplorerCommandProvider(IExplorerCommandProvider** a2)
		{
			return ((delegate* unmanaged[Stdcall]<IFolderTypeDescription*, IExplorerCommandProvider**, HRESULT>)lpVtbl[5])((IFolderTypeDescription*)Unsafe.AsPointer(ref this), a2);
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
