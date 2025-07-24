// Copyright (c) Files Community
// Licensed under the MIT License.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Windows.Win32
{
	public static unsafe partial class IID
	{
		[GuidRVAGen.Guid("000214E4-0000-0000-C000-000000000046")]
		public static partial Guid* IID_IContextMenu { get; }

		[GuidRVAGen.Guid("000214F4-0000-0000-C000-000000000046")]
		public static partial Guid* IID_IContextMenu2 { get; }

		[GuidRVAGen.Guid("70629033-E363-4A28-A567-0DB78006E6D7")]
		public static partial Guid* IID_IEnumShellItems { get; }

		[GuidRVAGen.Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
		public static partial Guid* IID_IShellItem { get; }

		[GuidRVAGen.Guid("7E9FB0D3-919F-4307-AB2E-9B1860310C93")]
		public static partial Guid* IID_IShellItem2 { get; }

		[GuidRVAGen.Guid("947AAB5F-0A5C-4C13-B4D6-4BF7836FC9F8")]
		public static partial Guid* IID_IFileOperation { get; }

		[GuidRVAGen.Guid("2EBDEE67-3505-43F8-9946-EA44ABC8E5B0")]
		public static partial Guid* IID_IQueryParser { get; }

		[GuidRVAGen.Guid("B63EA76D-1F85-456F-A19C-48159EFA858B")]
		public static partial Guid* IID_IShellItemArray { get; }

		[GuidRVAGen.Guid("000214E6-0000-0000-C000-000000000046")]
		public static partial Guid* IID_IShellFolder { get; }

		[GuidRVAGen.Guid("7F9185B0-CB92-43C5-80A9-92277A4F7B54")]
		public static partial Guid* IID_IExecuteCommand { get; }

		[GuidRVAGen.Guid("000214E8-0000-0000-C000-000000000046")]
		public static partial Guid* IID_IShellExtInit { get; }

		[GuidRVAGen.Guid("0000010E-0000-0000-C000-000000000046")]
		public static partial Guid* IID_IDataObject { get; }
	}

	public static unsafe partial class CLSID
	{
		[GuidRVAGen.Guid("3AD05575-8857-4850-9277-11B85BDB8E09")]
		public static partial Guid* CLSID_FileOperation { get; }

		[GuidRVAGen.Guid("B455F46E-E4AF-4035-B0A4-CF18D2F6F28E")]
		public static partial Guid* CLSID_PinToFrequentExecute { get; }

		[GuidRVAGen.Guid("09799AFB-AD67-11D1-ABCD-00C04FC30936")]
		public static partial Guid* CLSID_OpenWithMenu { get; }
	}

	public static unsafe partial class BHID
	{
		[GuidRVAGen.Guid("3981E225-F559-11D3-8E3A-00C04F6837D5")]
		public static partial Guid* BHID_SFUIObject { get; }

		[GuidRVAGen.Guid("3981e224-f559-11d3-8e3a-00c04f6837d5")]
		public static partial Guid* BHID_SFObject { get; }

		[GuidRVAGen.Guid("94F60519-2850-4924-AA5A-D15E84868039")]
		public static partial Guid* BHID_EnumItems { get; }
	}

	public static unsafe partial class FOLDERID
	{
		[GuidRVAGen.Guid("B7534046-3ECB-4C18-BE4E-64CD4CB7D6AC")]
		public static partial Guid* FOLDERID_RecycleBinFolder { get; }

		[GuidRVAGen.Guid("7D9B7B71-9D2A-11CF-97C5-00AA006319F1")]
		public static partial Guid* FOLDERID_DocumentsLibrary { get; }
	}
}
