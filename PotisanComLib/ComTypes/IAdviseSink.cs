// TODO

// TODO

namespace Potisan.Windows.Com.ComTypes;

/// <summary>
/// TYMED
/// </summary>
[Flags]
public enum MediumType : uint
{
	HGbloal = 1,
	File = 2,
	IStream = 4,
	IStorage = 8,
	GDI = 16,
	MetaFilePicture = 32,
	EnhMetafile = 64,
	Null = 0,
}

//public struct RemSTGMEDIUM0
//{
//	uint tymed;
//	uint dwHandleType;
//	ULONG pData;
//	ULONG pUnkForRelease;
//	ULONG cbData;
//	/* [size_is] */
//	byte data[1]; // 可変長
//}

//public struct GDI_OBJECT
//{
//	uint ObjectType;
//	/* [switch_is] */ /* [switch_type] */
//	union __MIDL_IAdviseSink_0002
//	{
//		/* [case()] */
//		wireHBITMAP hBitmap;
//		/* [case()] */
//		wireHPALETTE hPalette;
//		/* [default] */
//		wireHGLOBAL hGeneric;
//	}
//	u;
//    }

//struct _STGMEDIUM_UNION
//{
//	uint tymed;
//	/* [switch_is] */ /* [switch_type] */
//	union __MIDL_IAdviseSink_0003
//	{
//		/* [case()] */  /* Empty union arm */
//		/* [case()] */
//		wireHMETAFILEPICT hMetaFilePict;
//		/* [case()] */
//		wireHENHMETAFILE hHEnhMetaFile;
//		/* [case()] */
//		GDI_OBJECT *hGdiHandle;
//		/* [case()] */
//		wireHGLOBAL hGlobal;
//		/* [case()] */
//		LPOLESTR lpszFileName;
//		/* [case()] */
//		BYTE_BLOB *pstm;
//		/* [case()] */
//		BYTE_BLOB *pstg;
//	}
//	u;
//        }
//DUMMYUNIONNAME;
//    IUnknown* pUnkForRelease;
//}
//userSTGMEDIUM;

//typedef struct _userFLAG_STGMEDIUM
//{
//	LONG ContextFlags;
//	LONG fPassOwnership;
//	userSTGMEDIUM Stgmed;
//}
//userFLAG_STGMEDIUM;

//typedef /* [wire_marshal] */ struct _FLAG_STGMEDIUM
//{
//	LONG ContextFlags;
//	LONG fPassOwnership;
//	STGMEDIUM Stgmed;
//}
//FLAG_STGMEDIUM;

[ComImport]
[Guid("0000010f-0000-0000-C000-000000000046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IAdviseSink
{
	[PreserveSig]
	int OnDataChange(
		ComFormatEtc pFormatetc,
		ComStorageMedium pStgmed);

	[PreserveSig]
	int OnViewChange(
		uint dwAspect,
		int lindex);

	[PreserveSig]
	int OnRename(
		IMoniker pmk);

	[PreserveSig]
	int OnSave();

	[PreserveSig]
	int OnClose();
}