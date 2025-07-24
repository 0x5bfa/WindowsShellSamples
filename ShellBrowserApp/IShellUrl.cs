// Copyright (c) 0x5BFA. All rights reserved.

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;
using Windows.Win32.UI.Shell.Common;

namespace Windows.Win32.UI.Shell
{
	public unsafe struct IShellUrl : IComIID
	{
		private void** lpVtbl;

		public unsafe HRESULT QueryInterface(Guid* riid, void** ppvObject)
			=> ((delegate* unmanaged[Stdcall]<IShellUrl*, Guid*, void**, HRESULT>)lpVtbl[0])((IShellUrl*)Unsafe.AsPointer(ref this), riid, ppvObject);



		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT ParseFromOutsideSource(PCWSTR pcszUrlIn, uint dwParseFlags)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<IShellUrl*, PCWSTR, uint, int>)lpVtbl[3])((IShellUrl*)Unsafe.AsPointer(ref this), pcszUrlIn, dwParseFlags);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT GetUrl(PWSTR pszUrlOut, uint cchUrlOutSize)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<IShellUrl*, PWSTR, uint, int>)lpVtbl[4])((IShellUrl*)Unsafe.AsPointer(ref this), pszUrlOut, cchUrlOutSize);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT SetUrl(PCWSTR pcszUrlIn, uint dwGenType)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<IShellUrl*, PCWSTR, uint, int>)lpVtbl[5])((IShellUrl*)Unsafe.AsPointer(ref this), pcszUrlIn, dwGenType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT GetDisplayName(PWSTR pszUrlOut, uint cchUrlOutSize)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<IShellUrl*, PCWSTR, uint, int>)lpVtbl[6])((IShellUrl*)Unsafe.AsPointer(ref this), pszUrlOut, cchUrlOutSize);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT GetPidl(ITEMIDLIST** ppidl)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<IShellUrl*, ITEMIDLIST**, int>)lpVtbl[7])((IShellUrl*)Unsafe.AsPointer(ref this), ppidl);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT SetPidl(ITEMIDLIST* pidl)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<IShellUrl*, ITEMIDLIST*, int>)lpVtbl[8])((IShellUrl*)Unsafe.AsPointer(ref this), pidl);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT SetPidlAndArgs(ITEMIDLIST* pidl, PCWSTR pszArgsIn)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<IShellUrl*, ITEMIDLIST*, PCWSTR, int>)lpVtbl[9])((IShellUrl*)Unsafe.AsPointer(ref this), pidl, pszArgsIn);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public PWSTR GetArgs()
			=> (PWSTR)((delegate* unmanaged[MemberFunction]<IShellUrl*, char*>)lpVtbl[10])((IShellUrl*)Unsafe.AsPointer(ref this));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT AddPath(ITEMIDLIST* pidl)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<IShellUrl*, ITEMIDLIST*, int>)lpVtbl[11])((IShellUrl*)Unsafe.AsPointer(ref this), pidl);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT SetCancelObject(ICancelMethodCalls* a1)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<IShellUrl*, ICancelMethodCalls*, int>)lpVtbl[12])((IShellUrl*)Unsafe.AsPointer(ref this), a1);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT StartAsyncPathParse(HWND a1, PCWSTR a2, uint a3, ICancelMethodCalls* a4)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<IShellUrl*, HWND, PCWSTR, uint, ICancelMethodCalls*, int>)lpVtbl[13])((IShellUrl*)Unsafe.AsPointer(ref this), a1, a2, a3, a4);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT GetParseResult()
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<IShellUrl*, int>)lpVtbl[14])((IShellUrl*)Unsafe.AsPointer(ref this));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT SetRequestID(int a1)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<IShellUrl*, int, int>)lpVtbl[15])((IShellUrl*)Unsafe.AsPointer(ref this), a1);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT GetRequestID(int* a1)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<IShellUrl*, int*, int>)lpVtbl[16])((IShellUrl*)Unsafe.AsPointer(ref this), a1);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT GetNavFlags(int a1, int a2)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<IShellUrl*, int, int, int>)lpVtbl[17])((IShellUrl*)Unsafe.AsPointer(ref this), a1, a2);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT SetNavFlags(long* a1)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<IShellUrl*, long*, int>)lpVtbl[18])((IShellUrl*)Unsafe.AsPointer(ref this), a1);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT Execute(void* /* IShellNavigationTarget* */ a1, int* a2, uint a3)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<IShellUrl*, void*, int*, uint, int>)lpVtbl[19])((IShellUrl*)Unsafe.AsPointer(ref this), a1, a2, a3);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT SetCurrentWorkingDir(ITEMIDLIST* pidl)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<IShellUrl*, ITEMIDLIST*, int>)lpVtbl[20])((IShellUrl*)Unsafe.AsPointer(ref this), pidl);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT SetMessageBoxParent(HWND hwnd)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<IShellUrl*, HWND, int>)lpVtbl[21])((IShellUrl*)Unsafe.AsPointer(ref this), hwnd);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT GetPidlNoGenerate(ITEMIDLIST** ppidl)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<IShellUrl*, ITEMIDLIST**, int>)lpVtbl[22])((IShellUrl*)Unsafe.AsPointer(ref this), ppidl);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HRESULT GetStandardParsingFlags(BOOL a1)
			=> (HRESULT)((delegate* unmanaged[MemberFunction]<IShellUrl*, bool, int>)lpVtbl[23])((IShellUrl*)Unsafe.AsPointer(ref this), a1);

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
