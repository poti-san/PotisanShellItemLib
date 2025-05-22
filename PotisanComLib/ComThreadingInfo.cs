using System.ComponentModel;

using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// COMスレッド情報を取得・設定します。
/// </summary>
/// <remarks>
/// <para><c>IComThreadingInfo</c> COMインターフェイスのラッパーです。</para> 
/// <para>マルチスレッド対応のラッパー関数として<c>CoGetApartmentType</c>があります。
/// また、C#では<see cref="System.Threading.Thread.CurrentThread"/>から様々な情報を取得できます。</para>
/// </remarks>
/// <example>
/// <code>
///<![CDATA[using Potisan.Windows.Com;
///
///var threadingInfo = ComThreadingInfo.GetCurrentContext();
///
///Console.WriteLine($"""
///	アパートメントタイプ: {threadingInfo.CurrentApartmentType}
///	スレッドタイプ: {threadingInfo.CurrentThreadType}
///	論理スレッドID: {threadingInfo.CurrentLogicalThreadID}
///	""");
///]]>
/// </code>
/// </example>
public class ComThreadingInfo(object? o) : ComUnknownWrapperBase<IComThreadingInfo>(o)
{
	public static ComResult<ComThreadingInfo> GetCurrentContextNoThrow()
	{
		[DllImport("ole32.dll")]
		static extern int CoGetContextToken(out nint pToken);

		var hr = CoGetContextToken(out var x);
		return new(hr, hr == CommonHResults.SOK ? new(Marshal.GetObjectForIUnknown(x)) : null!);
	}

	public static ComThreadingInfo GetCurrentContext()
		=> GetCurrentContextNoThrow().Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ComApartmentType> CurrentApartmentTypeNoThrow
		=> new(_obj.GetCurrentApartmentType(out var x), x);

	public ComApartmentType CurrentApartmentType
		=> CurrentApartmentTypeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<ComThreadType> CurrentThreadTypeNoThrow
		=> new(_obj.GetCurrentThreadType(out var x), x);

	public ComThreadType CurrentThreadType
		=> CurrentThreadTypeNoThrow.Value;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public ComResult<Guid> CurrentLogicalThreadIDNoThrow
		=> new(_obj.GetCurrentLogicalThreadId(out var x), x);

	public ComResult SetCurrentLogicalThreadIDNoThrow(in Guid value)
		=> new(_obj.SetCurrentLogicalThreadId(value));

	public Guid CurrentLogicalThreadID
	{
		get => CurrentLogicalThreadIDNoThrow.Value;
		set => SetCurrentLogicalThreadIDNoThrow(value).ThrowIfError();
	}
}

/// <summary>
/// 
/// </summary>
/// <remarks>
/// <c>APTTYPEQUALIFIER</c>。
/// </remarks>
public enum ComApartmentTypeQualifier
{
	None = 0,
	ImplicitMta = 1,
	NaturalOnMta = 2,
	NaturalOnSta = 3,
	NaturalOnImplicitMta = 4,
	NaturalOnMainSta = 5,
	ApplicationSta = 6,
	[EditorBrowsable(EditorBrowsableState.Never)]
	Reserved1 = 7,
}

/// <summary>
/// 
/// </summary>
/// <remarks>
/// <c>APTTYPE</c>。
/// </remarks>
public enum ComApartmentType
{
	Current = -1,
	Sta = 0,
	Mta = 1,
	Natural = 2,
	MainSta = 3,
}

///<summary>
///
/// </summary>
/// <remarks>
/// <c>THDTYPE</c>。
/// </remarks>
public enum ComThreadType
{
	BlockMessages = 0,
	ProcessMessages = 1,
}
