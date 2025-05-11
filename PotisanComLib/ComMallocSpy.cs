using Potisan.Windows.Com.ComTypes;

namespace Potisan.Windows.Com;

/// <summary>
/// COMメモリ割り当てスパイ。このクラスを継承して特殊化したスパイを作成できます。
/// </summary>
/// <remarks>
/// このクラスはCOMタスクメモリのみ監視します。
/// ローカルメモリ、グローバルメモリ、ヒープ、仮想メモリ等は対象外です。
/// </remarks>
/// <example>
/// <code>
///<![CDATA[using System.Runtime.InteropServices;
///
///using Potisan.Windows.Com;
///
///var mallocSpy = new ComMallocSpyDebugWriter();
///mallocSpy.RegisterSpy();
///
///var p1 = Marshal.AllocCoTaskMem(1024);]]>
/// </code>
/// </example>
public abstract partial class ComMallocSpy : IMallocSpy
{
	public ComResult RegisterSpyNoThrow()
	{
		[DllImport("ole32.dll")]
		static extern int CoRegisterMallocSpy(IMallocSpy pMallocSpy);

		return new(CoRegisterMallocSpy(this));
	}

	public void RegisterSpy()
		=> RegisterSpyNoThrow().ThrowIfError();

	public static ComResult RevokeSpyNoThrow()
	{
		[DllImport("ole32.dll")]
		static extern int CoRevokeMallocSpy();

		return new(CoRevokeMallocSpy());
	}

	public static void RevokeSpy()
		=> RevokeSpyNoThrow().ThrowIfError();

	public abstract nuint PreAlloc(nuint RequestSize);
	public abstract nint PostAlloc(nint ActualPointer);
	public abstract nint PreFree(nint RequestSize, [MarshalAs(UnmanagedType.Bool)] bool Spyed);
	public abstract void PostFree([MarshalAs(UnmanagedType.Bool)] bool Spyed);
	public abstract nuint PreRealloc(nint RequestPointer, nuint RequestSize, out nint NewRequestPointer, [MarshalAs(UnmanagedType.Bool)] bool Spyed);
	public abstract nint PostRealloc(nint ActualPointer, [MarshalAs(UnmanagedType.Bool)] bool Spyed);
	public abstract nint PreGetSize(nint RequestPointer, [MarshalAs(UnmanagedType.Bool)] bool Spyed);
	public abstract nuint PostGetSize(nuint ActualSize, [MarshalAs(UnmanagedType.Bool)] bool Spyed);
	public abstract nint PreDidAlloc(nint RequestPointer, [MarshalAs(UnmanagedType.Bool)] bool Spyed);
	public abstract int PostDidAlloc(nint RequestPointer, [MarshalAs(UnmanagedType.Bool)] bool Spyed, int Actual);
	public abstract void PreHeapMinimize();
	public abstract void PostHeapMinimize();
}

public partial class ComMallocSpyStringCaller(Action<string> action) : ComMallocSpy
{
	private readonly Action<string> _action = action;

	public override nint PostAlloc(nint ActualPointer)
	{
		_action($"PostAlloc({ActualPointer})");
		return ActualPointer;
	}

	public override int PostDidAlloc(nint RequestPointer, [MarshalAs(UnmanagedType.Bool)] bool Spyed, int Actual)
	{
		_action($"PostDibAlloc({RequestPointer}, {Spyed}, {Actual})");
		return Actual;
	}

	public override void PostFree([MarshalAs(UnmanagedType.Bool)] bool Spyed)
	{
		_action($"PostFeee({Spyed})");
	}

	public override nuint PostGetSize(nuint ActualSize, [MarshalAs(UnmanagedType.Bool)] bool Spyed)
	{
		_action($"PostGetSize({ActualSize}, {Spyed})");
		return ActualSize;
	}

	public override void PostHeapMinimize()
	{
		_action("PostHeapMinimize()");
	}

	public override nint PostRealloc(nint ActualPointer, [MarshalAs(UnmanagedType.Bool)] bool Spyed)
	{
		_action($"PostRealloc({Spyed})");
		return ActualPointer;
	}

	public override nuint PreAlloc(nuint RequestSize)
	{
		_action($"PreAlloc({RequestSize})");
		return RequestSize;
	}

	public override nint PreDidAlloc(nint RequestPointer, [MarshalAs(UnmanagedType.Bool)] bool Spyed)
	{
		_action($"PreDidAlloc({RequestPointer}, {Spyed})");
		return RequestPointer;
	}

	public override nint PreFree(nint RequestSize, [MarshalAs(UnmanagedType.Bool)] bool Spyed)
	{
		_action($"PreFree({RequestSize}, {Spyed})");
		return RequestSize;
	}

	public override nint PreGetSize(nint RequestPointer, [MarshalAs(UnmanagedType.Bool)] bool Spyed)
	{
		_action($"PreGetSize({RequestPointer}, {Spyed})");
		return RequestPointer;
	}

	public override void PreHeapMinimize()
	{
		_action("PreHeapMinimize()");
	}

	public override nuint PreRealloc(nint RequestPointer, nuint RequestSize, out nint NewRequestPointer, [MarshalAs(UnmanagedType.Bool)] bool Spyed)
	{
		_action($"PreRealloc({RequestPointer}, {RequestSize}, {Spyed})");
		NewRequestPointer = RequestPointer;
		return RequestSize;
	}
}

public partial class ComMallocSpyDebugWriter : ComMallocSpyStringCaller
{
	public ComMallocSpyDebugWriter()
		: base(s => Debug.WriteLine(s))
	{
	}
}
