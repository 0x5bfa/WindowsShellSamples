// Copyright (c) 0x5BFA. All rights reserved.

using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.System.Com;
using Windows.Win32.System.Ole;
using Windows.Win32.System.Registry;
using Windows.Win32.System.Search;
using Windows.Win32.System.SystemServices;
using Windows.Win32.UI.Shell;
using Windows.Win32.UI.Shell.Common;
using Windows.Win32.UI.Shell.PropertiesSystem;
using Windows.Win32.UI.WindowsAndMessaging;


#pragma warning disable CA1416

namespace ShellBrowserApp
{
	internal unsafe class Program
	{
		private static uint SHChangeNotifyRegisterID = 0;
		private static ITEMIDLIST* pidl = default;
		private const uint WM_SHNOTIFY = PInvoke.WM_APP | 0x6;

		static void Main(string[] args)
		{
			Console.WriteLine();
			Console.WriteLine("==================================================");
			Console.WriteLine();
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			PInvoke.CoInitializeEx(null, COINIT.COINIT_APARTMENTTHREADED);

			EnumerateOpenWithMenuItems();

			PInvoke.CoUninitialize();

			stopwatch.Stop();
			Console.WriteLine();
			Console.WriteLine("==================================================");
			Console.WriteLine();
			Console.WriteLine($"Elapsed: {stopwatch.Elapsed}");
		}

		internal static void EnumerateFolder(string parsablePath)
		{
			HRESULT hr = default;

			using ComPtr<IShellItem> pShellItem = default;
			fixed (char* pszPath = parsablePath)
			{
				hr = PInvoke.SHCreateItemFromParsingName(
					pszPath,
					null,
					IID.IID_IShellItem,
					(void**)pShellItem.GetAddressOf());
			}

			using ComPtr<IEnumShellItems> pEnumShellItems = default;
			hr = pShellItem.Get()->BindToHandler(null, BHID.BHID_EnumItems, IID.IID_IEnumShellItems, (void**)pEnumShellItems.GetAddressOf());

			List<string> Names = [];
			int count = 0;

			if (pEnumShellItems.IsNull)
			{
				using ComPtr<IShellFolder> pShellFolder = default;
				using ComPtr<IEnumIDList> pEnumIDList = default;

				pShellItem.Get()->BindToHandler(null, BHID.BHID_SFObject, IID.IID_IShellFolder, (void**)pShellFolder.GetAddressOf());

				pShellFolder.Get()->EnumObjects(HWND.Null, 0, pEnumIDList.GetAddressOf());
			}
			else
			{
				using ComPtr<IShellItem> pChildShellItem = default;
				while (pEnumShellItems.Get()->Next(1, pChildShellItem.GetAddressOf()) == HRESULT.S_OK)
				{
					PWSTR pName = default;
					hr = pChildShellItem.Get()->GetDisplayName(SIGDN.SIGDN_PARENTRELATIVEFORUI, &pName);

					SFGAO_FLAGS attributes = default;
					pChildShellItem.Get()->GetAttributes(SFGAO_FLAGS.SFGAO_HIDDEN, &attributes);
					Names.Add($"{pName} ({attributes})");
					Console.WriteLine(pName);
					count++;
				}
			}

			Console.WriteLine();
			Console.WriteLine($"Count: {count}");
		}

		internal static void EnumerateWindows11ContextMenuItems()
		{
			// FOLDERTYPEID_Music
			Guid folderId = new("94D6DDCC-4A68-4175-A374-BD584A510B78");

			Guid IID_IEnumExplorerCommand = IEnumExplorerCommand.IID_Guid;
			using ComPtr<IFolderTypeDescription> pFolderTypeDescription = default;
			using ComPtr<IExplorerCommandProvider> pExplorerCommandProvider = default;
			using ComPtr<IEnumExplorerCommand> pEnumExplorerCommand = default;

			// Call an undocumented function and retrieve an undocumented COM object
			var hr = (HRESULT)PInvoke.SHGetFolderTypeDescription(
				&folderId,
				(Guid*)Unsafe.AsPointer(ref Unsafe.AsRef(in IFolderTypeDescription.Guid)),
				(void**)pFolderTypeDescription.GetAddressOf());

			// Get the command provider
			hr = pFolderTypeDescription.Get()->GetExplorerCommandProvider(pExplorerCommandProvider.GetAddressOf());

			// Get the enumerator
			hr = pExplorerCommandProvider.Get()->GetCommands(
				null,
				&IID_IEnumExplorerCommand,
				(void**)pEnumExplorerCommand.GetAddressOf());

			// Enumerate commands
			ComPtr<IExplorerCommand> pExplorerCommand = default;
			while (pEnumExplorerCommand.Get()->Next(1, pExplorerCommand.GetAddressOf(), null).Succeeded)
			{
				PWSTR pszName = default;
				pExplorerCommand.Get()->GetTitle(null, &pszName);
				Console.WriteLine(pszName.ToString());
			}
		}

		internal static void EnumerateLogicalDrives()
		{
			var availableDrives = PInvoke.GetLogicalDrives();
			if (availableDrives is 0)
				return;

			int count = BitOperations.PopCount(availableDrives);
			var driveLetters = new char[count];

			count = 0;
			char driveLetter = 'A';
			while (availableDrives is not 0)
			{
				if ((availableDrives & 1) is not 0)
					driveLetters[count++] = driveLetter;

				availableDrives >>= 1;
				driveLetter++;
			}

			Console.WriteLine($"Available drives: {string.Join(", ", driveLetters)}");
		}

		internal static void EnumerateDetailsViewColumns()
		{
			HRESULT hr = default;

			using ComPtr<IShellItem> pShellItem = default;
			fixed (char* pszPath = "C:\\")
			{
				hr = PInvoke.SHCreateItemFromParsingName(
					pszPath,
					null,
					IID.IID_IShellItem,
					(void**)pShellItem.GetAddressOf());
			}

			using ComPtr<IShellFolder> pShellFolder = default;
			Guid IID_IShellFolder = IShellFolder.IID_Guid;
			Guid BHID_SFObject = PInvoke.BHID_SFObject;
			hr = pShellItem.Get()->BindToHandler(null, &BHID_SFObject, &IID_IShellFolder, (void**)pShellFolder.GetAddressOf());

			using ComPtr<IShellView> pShellView = default;
			Guid IID_IShellView = IShellView.IID_Guid;
			hr = pShellFolder.Get()->CreateViewObject(default, &IID_IShellView, (void**)pShellView.GetAddressOf());

			using ComPtr<IColumnManager> pColumnManager = pShellView.As<IColumnManager>();

			pColumnManager.Get()->GetColumnCount(CM_ENUM_FLAGS.CM_ENUM_ALL, out uint columnCount);
			PROPERTYKEY* propertyKeys = stackalloc PROPERTYKEY[(int)columnCount];

			pColumnManager.Get()->GetColumns(CM_ENUM_FLAGS.CM_ENUM_ALL, propertyKeys, columnCount);
			var propsKeysSpan = new Span<PROPERTYKEY>(propertyKeys, (int)columnCount);

			foreach (var propKey in propsKeysSpan)
			{
				Guid IID_IPropertyDescription = IPropertyDescription.IID_Guid;
				using ComPtr<IPropertyDescription> pPropertyDescription = default;
				PWSTR pDisplayName = default;

				hr = PInvoke.PSGetPropertyDescription(&propKey, &IID_IPropertyDescription, (void**)pPropertyDescription.GetAddressOf());

				pPropertyDescription.Get()->GetDisplayName(&pDisplayName);
				pPropertyDescription.Get()->GetDefaultColumnWidth(out uint width); // needs avg char width to get pixel width

				Console.WriteLine($"{propKey.fmtid} ({propKey.pid}): name(\"{pDisplayName}\"), width({width})");
			}
		}

		internal static void ShowFormatDriveDialog()
		{
			// This doesn't need to be called on an admin process
			PInvoke.SHFormatDrive(PInvoke.GetConsoleWindow(), 3, SHFMT_ID.SHFMT_ID_DEFAULT, SHFMT_OPT.SHFMT_OPT_FULL);
		}

		internal static void EnumerateJumpList()
		{
			using ComPtr<ICustomDestinationList> pCustomDestinationList = default;

			Guid CLSID_CustomDestinationList = typeof(DestinationList).GUID;
			Guid IID_ICustomDestinationList = ICustomDestinationList.IID_Guid;
			HRESULT hr = PInvoke.CoCreateInstance(
				&CLSID_CustomDestinationList,
				null,
				CLSCTX.CLSCTX_INPROC_SERVER,
				&IID_ICustomDestinationList,
				(void**)pCustomDestinationList.GetAddressOf());

			PInvoke.SetCurrentProcessExplicitAppUserModelID("ShellBrowserConsoleApp");
			hr = pCustomDestinationList.Get()->SetAppID("ShellBrowserConsoleApp");

			uint cMinSlots = 0;
			uint count = 0;

			using ComPtr<IObjectArray> pObjectArray = default;
			Guid IID_IObjectArray = IObjectArray.IID_Guid;

			hr = pCustomDestinationList.Get()->BeginList(&cMinSlots, &IID_IObjectArray, (void**)pObjectArray.GetAddressOf());
			hr = pObjectArray.Get()->GetCount(&count);

			AppendCategory("Recent items", pCustomDestinationList);

			hr = pCustomDestinationList.Get()->CommitList();

			void AppendCategory(string category, ComPtr<ICustomDestinationList> pCDL)
			{
				using ComPtr<IObjectCollection> pOC = default;
				Guid CLSID_EnumerableObjectCollection = typeof(EnumerableObjectCollection).GUID;
				Guid IID_IObjectCollection = IObjectCollection.IID_Guid;

				HRESULT hr = PInvoke.CoCreateInstance(&CLSID_EnumerableObjectCollection, null, CLSCTX.CLSCTX_INPROC_SERVER, &IID_IObjectCollection, (void**)pOC.GetAddressOf());

				Guid FOLDERID_Documents = new("{FDD39AD0-238F-46AF-ADB4-6C85480369C7}");

				using ComPtr<IShellItem> pShellItem = default;
				fixed (char* pszPath = "C:\\Users\\onein\\OneDrive\\Docs\\Microsoft_Sample_1.txt")
				{
					hr = PInvoke.SHCreateItemFromParsingName(
						pszPath,
						null,
						IID.IID_IShellItem,
						(void**)pShellItem.GetAddressOf());
				}

				hr = pOC.Get()->AddObject((IUnknown*)pShellItem.Get());
				using ComPtr<IObjectArray> pObjectArray = pOC.As<IObjectArray>();

				hr = pCustomDestinationList.Get()->AppendCategory(category, pObjectArray.Get());
			}
		}

		internal static void EnumerateSearchFolder()
		{
			HRESULT hr = default;

			using ComPtr<IShellItem> pSearchTargetFolder = default;
			fixed (char* pszPath = "Shell:::{A8CDFF1C-4878-43be-B5FD-F8091C1C60D0}")
			{
				hr = PInvoke.SHCreateItemFromParsingName(
					pszPath,
					null,
					IID.IID_IShellItem,
					(void**)pSearchTargetFolder.GetAddressOf());
			}

			// Get an IShellItemArray from the array of the PIDL
			using ComPtr<IShellItemArray> pShellItemArray = default;
			hr = PInvoke.SHCreateShellItemArrayFromShellItem(pSearchTargetFolder.Get(), IID.IID_IShellItemArray, (void**)pShellItemArray.GetAddressOf()).ThrowOnFailure();

			using ComPtr<ISearchFolderItemFactory> pSearchFolderItemFactory = default;
			hr = pSearchFolderItemFactory.CoCreateInstance<SearchFolderItemFactory>(CLSCTX.CLSCTX_INPROC_SERVER);

			hr = pSearchFolderItemFactory.Get()->SetScope(pShellItemArray.Get());
			hr = pSearchFolderItemFactory.Get()->SetDisplayName("Search Results");

			using ComPtr<IQueryParserManager> pQueryParserManager = default;
			using ComPtr<IQueryParser> pQueryParser = default;
			using ComPtr<IQuerySolution> pQuerySolution = default;
			using ComPtr<ICondition> pCondition = default;

			hr = pQueryParserManager.CoCreateInstance<QueryParserManager>(CLSCTX.CLSCTX_INPROC_SERVER);
			fixed (char* pszCatalog = "SystemIndex")
				pQueryParserManager.Get()->CreateLoadedParser(pszCatalog, (ushort)PInvoke.LOCALE_USER_DEFAULT, IID.IID_IQueryParser, (void**)pQueryParser.GetAddressOf());

			hr = pQueryParser.Get()->Parse("*txt", null, pQuerySolution.GetAddressOf());
			hr = pQuerySolution.Get()->GetQuery(pCondition.GetAddressOf(), null);

			hr = pSearchFolderItemFactory.Get()->SetCondition(pCondition.Get());

			using ComPtr<IShellItem> pSearchResultFolder = default;

			hr = pSearchFolderItemFactory.Get()->GetShellItem(IID.IID_IShellItem, (void**)pSearchResultFolder.GetAddressOf());

			using ComPtr<IEnumShellItems> pEnumShellItems = default;
			hr = pSearchResultFolder.Get()->BindToHandler(null, BHID.BHID_EnumItems, IID.IID_IEnumShellItems, (void**)pEnumShellItems.GetAddressOf());

			using ComPtr<IShellItem> pChildShellItem = default;
			while (pEnumShellItems.Get()->Next(1, pChildShellItem.GetAddressOf()) == HRESULT.S_OK)
			{
				PWSTR pName = default;
				hr = pChildShellItem.Get()->GetDisplayName(SIGDN.SIGDN_PARENTRELATIVEFORUI, &pName);

				Console.WriteLine(pName);
			}
		}

		internal static void RegisterFolderChangeNotification()
		{
			HINSTANCE hInst = PInvoke.GetModuleHandle(default(PWSTR));

			PInvoke.SHParseDisplayName("C:\\Users\\onein\\Downloads", null, out var pIDL, 0, null);
			pidl = pIDL;

			fixed (char* pszClassName = "FolderWatcherWindowClass")
			{
				WNDCLASSEXW wndClass = default;
				wndClass.cbSize = (uint)sizeof(WNDCLASSEXW);
				wndClass.lpfnWndProc = &WndProc;
				wndClass.hInstance = hInst;
				wndClass.lpszClassName = pszClassName;

				PInvoke.RegisterClassEx(&wndClass);

				HWND hwnd = PInvoke.CreateWindowEx(
					0,
					pszClassName,
					null,
					0,
					0, 0, 0, 0,
					HWND.HWND_MESSAGE,
					default,
					hInst,
					null);
			}

			Console.ReadLine();

			return;

			[UnmanagedCallersOnly(CallConvs = [ typeof(CallConvStdcall) ])]
			unsafe static LRESULT WndProc(HWND hWnd, uint uMessage, WPARAM wParam, LPARAM lParam)
			{
				switch (uMessage)
				{
					case PInvoke.WM_CREATE:
						{
							PInvoke.CoInitialize();

							SHChangeNotifyEntry changeNotifyEntry = default;
							changeNotifyEntry.pidl = pidl;

							SHChangeNotifyRegisterID = PInvoke.SHChangeNotifyRegister(
								hWnd,
								SHCNRF_SOURCE.SHCNRF_ShellLevel | SHCNRF_SOURCE.SHCNRF_NewDelivery,
								(int)SHCNE_ID.SHCNE_ALLEVENTS,
								WM_SHNOTIFY,
								1,
								&changeNotifyEntry);

							if (SHChangeNotifyRegisterID is 0U)
								break;
						}
						break;
					case PInvoke.WM_DESTROY:
						{
							PInvoke.SHChangeNotifyDeregister(SHChangeNotifyRegisterID);

							if (pidl is not null)
								PInvoke.CoTaskMemFree(pidl);

							PInvoke.CoUninitialize();
							PInvoke.PostQuitMessage(0);
						}
						break;
					case WM_SHNOTIFY:
						{
							ITEMIDLIST** ppidl;
							int lEvent = 0;
							HANDLE hLock = PInvoke.SHChangeNotification_Lock((HANDLE)(nint)wParam.Value, (uint)lParam.Value, &ppidl, &lEvent);

							if (hLock.IsNull)
								break;

							if (lEvent == (int)SHCNE_ID.SHCNE_DELETE)
							{
								// DO SOMETHING
							}

							PInvoke.SHChangeNotification_Unlock(hLock);
						}
						break;
				}

				return PInvoke.DefWindowProc(hWnd, uMessage, wParam, lParam);
			}
		}

		internal static void PinFolderToQuickAccess()
		{
			HRESULT hr = default;

			using ComPtr<IExecuteCommand> pExecuteCommand = default;
			using ComPtr<IObjectWithSelection> pObjectWithSelection = default;
			Guid IID_IInitializeCommand = IObjectWithSelection.IID_Guid;

			PInvoke.CoCreateInstance(CLSID.CLSID_PinToFrequentExecute, null, CLSCTX.CLSCTX_INPROC_SERVER, IID.IID_IExecuteCommand, (void**)pExecuteCommand.GetAddressOf());

			using ComPtr<IShellItem> pShellItem = default;
			fixed (char* pszPath = "D:\\Study\\Files-thesis")
				hr = PInvoke.SHCreateItemFromParsingName(pszPath, null, IID.IID_IShellItem, (void**)pShellItem.GetAddressOf());

			using ComPtr<IShellItemArray> pShellItemArray = default;
			PInvoke.SHCreateShellItemArrayFromShellItem(pShellItem.Get(), IID.IID_IShellItemArray, (void**)pShellItemArray.GetAddressOf());

			pExecuteCommand.Get()->QueryInterface(&IID_IInitializeCommand, (void**)pObjectWithSelection.GetAddressOf());
			pObjectWithSelection.Get()->SetSelection(pShellItemArray.Get());

			pExecuteCommand.Get()->Execute();
		}

		internal static void SetVolumeLabel()
		{
			HRESULT hr = default;

			ComPtr<IMountPointRename> pMountPointRename = default;
			Guid CLSID_MountPointRename = new("60173D16-A550-47f0-A14B-C6F9E4DA0831");
			Guid IID_IMountPointRename = new("92F8D886-AB61-4113-BD4F-2E894397386F");

			BIND_OPTS3 bo = default;
			bo.Base.Base.cbStruct = (uint)sizeof(BIND_OPTS3);
			bo.Base.dwClassContext = (uint)CLSCTX.CLSCTX_LOCAL_SERVER;
			fixed (char* pMonikerName = "Elevation:Administrator!new:{60173D16-A550-47f0-A14B-C6F9E4DA0831}")
				hr = PInvoke.CoGetObject(pMonikerName, null, &IID_IMountPointRename, (void**)pMountPointRename.GetAddressOf());

			// The same usage as SetVolumeLabel function (the lpRootPathName must include a trailing backslash)
			fixed (char* lpRootPathName = "C:\\") fixed(char* lpVolumeName = "Local Disk x2")
				hr = pMountPointRename.Get()->Rename(lpRootPathName, lpVolumeName);
		}

		internal static void ParseShellUrl()
		{
			Guid CLSID_ShellUrl = new("4BEC2015-BFA1-42FA-9C0C-59431BBE880E");
			Guid IID_IShellUrl = new("00000000-0000-0000-C000-000000000046");
			Guid IID_IShellUrl2 = new("4f33718d-bae1-4f9b-96f2-d2a16e683346");
			ComPtr<IShellUrl> pShellUrl = default;
			ComPtr<IShellUrl> pShellUrl2 = default;

			PInvoke.CoCreateInstance(&CLSID_ShellUrl, null, CLSCTX.CLSCTX_INPROC_SERVER, &IID_IShellUrl, (void**)pShellUrl.GetAddressOf());

			pShellUrl.Get()->QueryInterface(&IID_IShellUrl2, (void**)pShellUrl2.GetAddressOf());
		}

		internal static void EnumerateOpenWithMenuItems()
		{
			HRESULT hr = default;

			using ComPtr<IContextMenu> pOpenWithContextMenu = default;
			using ComPtr<IContextMenu2> pOpenWithContextMenu2 = default;
			using ComPtr<IShellExtInit> pShellExtInit = default;
			using ComPtr<IShellItem> pShellItem = default;
			using ComPtr<IShellItem> pParentFolderShellItem = default;
			using ComPtr<IDataObject> pDataObject = default;
			ComHeapPtr<ITEMIDLIST> pParentAbsolutePidl = default;
			ComHeapPtr<ITEMIDLIST> pThisAbsolutePidl = default;
			ComHeapPtr<ITEMIDLIST> pThisRelativePidl = default;

			PInvoke.CoCreateInstance(CLSID.CLSID_OpenWithMenu, null, CLSCTX.CLSCTX_INPROC_SERVER, IID.IID_IContextMenu, (void**)pOpenWithContextMenu.GetAddressOf());
			hr = pOpenWithContextMenu.Get()->QueryInterface(IID.IID_IShellExtInit, (void**)pShellExtInit.GetAddressOf());
			hr = pOpenWithContextMenu.Get()->QueryInterface(IID.IID_IContextMenu2, (void**)pOpenWithContextMenu2.GetAddressOf());
			fixed (char* pszPath = "D:\\Branding.svg") hr = PInvoke.SHCreateItemFromParsingName(pszPath, null, IID.IID_IShellItem, (void**)pShellItem.GetAddressOf());

			// Get the absolute PIDL of the parent folder
			pShellItem.Get()->GetParent(pParentFolderShellItem.GetAddressOf());
			PInvoke.SHGetIDListFromObject((IUnknown*)pParentFolderShellItem.Get(), pParentAbsolutePidl.GetAddressOf());

			// Get the relative PIDL of the current item
			PInvoke.SHGetIDListFromObject((IUnknown*)pShellItem.Get(), pThisAbsolutePidl.GetAddressOf());
			pThisRelativePidl.Attach(PInvoke.ILFindLastID(pThisAbsolutePidl.Get()));

			hr = PInvoke.SHCreateDataObject(pParentAbsolutePidl.Get(), 1U, pThisRelativePidl.GetAddressOf(), null, IID.IID_IDataObject, (void**)pDataObject.GetAddressOf());

			// The 2nd parameter must not be null, others aren't used.
			hr = pShellExtInit.Get()->Initialize(null, pDataObject.Get(), HKEY.Null);

			// Inserts "New (&W)"
			HMENU hMenu = PInvoke.CreatePopupMenu();
			hr = pOpenWithContextMenu.Get()->QueryContextMenu(hMenu, 0, 1, 256, 0);

			// Invokes CNewMenu::_InitMenuPopup(), which populates the hSubMenu
			HMENU hSubMenu = PInvoke.GetSubMenu(hMenu, 0);
			hr = pOpenWithContextMenu2.Get()->HandleMenuMsg(PInvoke.WM_INITMENUPOPUP, (WPARAM)(nuint)hSubMenu.Value, 0);

			uint dwCount = unchecked((uint)PInvoke.GetMenuItemCount(hSubMenu));
			if (dwCount is unchecked((uint)-1)) return;

			// Enumerates and populates the list
			for (uint dwIndex = 0U; dwIndex < dwCount; dwIndex++)
			{
				MENUITEMINFOW mii = default;
				mii.cbSize = (uint)sizeof(MENUITEMINFOW);
				mii.fMask = MENU_ITEM_MASK.MIIM_STRING | MENU_ITEM_MASK.MIIM_ID | MENU_ITEM_MASK.MIIM_STATE;
				mii.dwTypeData = (char*)NativeMemory.Alloc(256U);
				mii.cch = 256;

				if (PInvoke.GetMenuItemInfo(hSubMenu, dwIndex, true, &mii))
				{
					Console.WriteLine($"{mii.wID}: \"{mii.dwTypeData.ToString()}\" ({mii.fState})");
				}

				NativeMemory.Free(mii.dwTypeData);
			}
		}
	}
}
