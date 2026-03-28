using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Windows.Win32.Foundation;

namespace Windows.Win32.UI.Shell;

[GeneratedComInterface, Guid("692d40a4-efa1-4089-88f8-15fd6f5f8b64"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal unsafe partial interface ISyncRootManager
{
	[PreserveSig]
	[return: MarshalAs(UnmanagedType.Error)]
	HRESULT GetStorageProviderInfo();

	[PreserveSig]
	[return: MarshalAs(UnmanagedType.Error)]
	HRESULT GetStorageProviderInfoFromPath(
		[MarshalAs(UnmanagedType.LPWStr)] string filePath,
		out IStorageProviderInfo ppv,
		out PWSTR displayName,
		int* outCompareFlags);
}
