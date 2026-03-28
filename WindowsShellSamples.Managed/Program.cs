using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;
using Windows.Win32.UI.Shell;

namespace WindowsShellSamples.Managed;

unsafe class Program
{
	static void Main(string[] args)
	{
		CreateSyncRootManager();

		//IShellItem psi;
	}

	static void CreateSyncRootManager()
	{
		HRESULT hr = PInvoke.CoCreateInstance(
			new("F324E4F9-8496-40B2-A1FF-9617C1C9AFFE"),
			null,
			CLSCTX.CLSCTX_INPROC_SERVER,
			typeof(ISyncRootManager).GUID,
			out object syncRootManagerObj);

		var syncRootManager = (ISyncRootManager)syncRootManagerObj;

		hr = syncRootManager.GetStorageProviderInfoFromPath(
			"C:\\Users\\onein\\OneDrive - Academic\\resume.pdf", out var storageProviderInfo, out _, null);

		uint d1;
		Guid* d2;
		int d3;
		// Unallocate the second arg with CoTaskMemFree
		hr = storageProviderInfo.GetContextMenuVerbs(&d1, &d2, &d3);

		hr = storageProviderInfo.GetFullProviderAndUserAndAccountIdentifier(out var val);


	}
}
