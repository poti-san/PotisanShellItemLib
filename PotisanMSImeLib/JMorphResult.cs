using System.Collections.Immutable;

namespace Potisan.Windows.MSIme;

public sealed class JMorphResult
{
	public string OutputString;
	public string InputString;
	public ImmutableArray<WordDescriptor> WordDescriptors { get; }

	internal JMorphResult(SafeHandle p)
	{
		var res = Marshal.PtrToStructure<MORRSLT>(p.DangerousGetHandle());

		unsafe
		{
			OutputString = Marshal.PtrToStringUni((nint)res.pwchOutput, res.cchOutput);
			InputString = Marshal.PtrToStringUni((nint)res.pwchReadOrComp, res.cchReadOrComp);

			var descs = new WordDescriptor[res.cWDD];
			for (var i = 0; i < descs.Length; i++)
				descs[i] = new(this, res.pWDD[i]);
			WordDescriptors = ImmutableCollectionsMarshal.AsImmutableArray(descs);
		}
	}
}

public sealed class WordDescriptor
{
	public ushort OutputStringOffset { get; }
	public ushort InputStringOffset { get; }
	public ushort OutputStringLength { get; }
	public ushort InputStringLength { get; }
	public string OutputString { get; }
	public string InputString { get; }
	public ushort Pos { get; }
	public ushort Flags { get; }
	#pragma warning disable format
		public bool IsPhrase         => (Flags & 0b00000000_00000001) != 0;
		public bool AutoCorrects     => (Flags & 0b00000000_00000010) != 0;
		public bool HasNumericPrefix => (Flags & 0b00000000_00000100) != 0;
		public bool IsUserRegistered => (Flags & 0b00000000_00001000) != 0;
		public bool IsUnknown        => (Flags & 0b00000000_00010000) != 0;
		public bool IsRecentUsed     => (Flags & 0b00000000_00100000) != 0;
		public ushort FlagsReserved  => (ushort)((Flags & 0b11111111_11000000) >> 6);
	#pragma warning restore format

	internal WordDescriptor(JMorphResult res, in WDD wdd)
	{
		OutputStringOffset = wdd.wDispPos;
		InputStringOffset = wdd.wReadOrCompPos;
		OutputStringLength = wdd.cchDisp;
		InputStringLength = wdd.cchReadOrComp;
		OutputString = res.OutputString[OutputStringOffset..(OutputStringOffset + OutputStringLength)];
		InputString = res.InputString[InputStringOffset..(InputStringOffset + InputStringLength)];
		Pos = wdd.nPos;
		Flags = wdd.Flags;
	}
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct WDD
{
	// 出力文字列のオフセット
	public readonly ushort wDispPos;
	public readonly ushort wReadOrCompPos;
	// ptchDispの個数
	public readonly ushort cchDisp;
	public readonly ushort cchReadOrComp;
	public readonly uint WDD_nReserve1;
	public readonly ushort nPos;
	public readonly ushort Flags;
	//points directly to WORDITEM
	public readonly nint pReserved;
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe readonly struct MORRSLT
{
	public readonly uint dwSize;
	public readonly short* pwchOutput;
	public readonly ushort cchOutput;
	public readonly ushort* pwchReadOrComp;
	public readonly ushort cchReadOrComp;
	public readonly ushort* pchInputPos;
	public readonly ushort* pchOutputIdxWDD; // TODO
	public readonly ushort* pchReadOrCompIdxWDD; // TODO
	public readonly ushort* paMonoRubyPos; // TODO
	public readonly WDD* pWDD;
	public readonly int cWDD;
	public readonly void* pPrivate;
	public readonly ushort BLKBuff0;
}