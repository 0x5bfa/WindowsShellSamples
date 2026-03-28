using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Windows.Win32.Foundation;

namespace Windows.Win32.UI.Shell;

[GeneratedComInterface, Guid("ca01c124-2769-4576-bf12-8a54ee671a86"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal unsafe partial interface IStorageProviderInfo
{
	// Identifier
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetFullProviderAndUserAndAccountIdentifier([MarshalAs(UnmanagedType.LPWStr)] out string value);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT SetFullProviderAndUserAndAccountIdentifier(char* value);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetProviderIdentifier(char** value);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT IsSameIdentifier(IStorageProviderInfo* other);

	// Display name
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetDisplayName(char** value);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetDisplayNameResource(char** value);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT SetDisplayNameResource(char* value);

	// Icon
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetIcon(char** iconPath, int* iconIndex);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetIconResource(char** value);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT SetIconResource(char* value);

	// Handlers
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT SetHandlerClsid(in Guid clsid);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetHandler(in Guid riid, void** obj);

	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT SetBannerHandlerClsid(in Guid clsid);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetBannerHandler(in Guid riid, void** obj);

	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT SetCustomStateHandlerClsid(in Guid clsid);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetCustomStateHandler(in Guid riid, void** obj);

	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT SetThumbnailProviderClsid(in Guid clsid);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetThumbnailProvider(char* arg, in Guid riid, void** obj);

	// Flags
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetFlags(STORAGE_PROVIDER_INFO_FLAGS* flags);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT SetFlags(STORAGE_PROVIDER_INFO_FLAGS mask, STORAGE_PROVIDER_INFO_FLAGS value);

	// Custom states
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetCustomStateIdList(uint** ids, uint* count);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetCustomStateDisplayName(uint id, char** name);

	// Protection
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetProtectionMode(char** mode);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT SetProtectionMode(char* mode);

	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetRecycleBinUrl(char** url);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT SetRecycleBinUrl(char* url);

	// Extended properties
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT SetExtendedPropertiesHandlerClsid(in Guid clsid);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetExtendedPropertiesHandler(in Guid riid, void** obj);

	// Context menu verbs
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetContextMenuVerbs(uint* a1, Guid** rrclsid, int* reserved);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT SetContextMenuVerbs(uint count, Guid* verbs);

	// URI handler
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT SetUriHandlerClsid(in Guid clsid);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetUriHandler(in Guid riid, void** obj);

	// AUMID
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT SetProviderAUMID(char* aumid);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetProviderAUMID(char** aumid);

	// Namespace
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetNamespaceClsid(Guid* clsid);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT SetNamespaceClsid(in Guid clsid);

	// Overlay handlers
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetIconOverlayHandlerClsidArray(uint* count, Guid** clsids, int* something);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT SetIconOverlayHandlerClsidArray(uint count, Guid* clsids);

	// Handler type
	[PreserveSig] StorageProviderHandlerType GetHandlerType(in Guid clsid);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT HasHandlerType(StorageProviderHandlerType type, int* result);

	// Factory / misc
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT SetHandlerFactoryClsid(in Guid clsid);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetJsonRequestHandler(in Guid riid, void** obj);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetEventSource(char* arg, in Guid riid, void** obj);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT SetCopyHookClsid(in Guid clsid);
	[PreserveSig][return: MarshalAs(UnmanagedType.Interface)] HRESULT GetCopyHook(in Guid riid, void** obj);
}

[Flags]
internal enum STORAGE_PROVIDER_INFO_FLAGS
{ }

internal enum StorageProviderHandlerType
{ }