using System.Collections.Immutable;

using Microsoft.Win32;

namespace Potisan.Windows.Shell;

/// <summary>
/// レジストリに登録されたフォルダ種類情報。
/// </summary>
/// <param name="id"></param>
/// <param name="writable"></param>
public sealed class FolderTypeInfo(in Guid id, bool writable = false) : IDisposable
{
	/// <summary>
	/// HKEY_LOCAL_MACHINE以下のFolderTypesキーのパス。
	/// </summary>
	public const string FolderTypesRegistryKeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FolderTypes";

	public static IEnumerable<Guid> FolderTypeGuidEnumerable
	{
		get
		{
			using var key = Registry.LocalMachine.OpenSubKey(FolderTypesRegistryKeyPath)
				?? throw new InvalidDataException();
			return key.GetSubKeyNames()
				.Select(s => Guid.TryParseExact(s, "B", out var x) ? new Guid?(x) : new Guid?())
				.Where(x => x != null)
				.Select(x => x!.Value);
		}
	}

	public static ImmutableArray<Guid> FolderTypeGuids
		=> [.. FolderTypeGuidEnumerable];

	public static IEnumerable<FolderTypeInfo> FolderTypeInfoEnumerable
	{
		get
		{
			using var key = Registry.LocalMachine.OpenSubKey(FolderTypesRegistryKeyPath)
				?? throw new InvalidDataException();
			return key.GetSubKeyNames()
				.Select(s => Guid.TryParseExact(s, "B", out var x) ? new Guid?(x) : new Guid?())
				.Where(x => x != null)
				.Select(x => new FolderTypeInfo(x!.Value));
		}
	}

	public static ImmutableArray<FolderTypeInfo> FolderTypeInfos
		=> [.. FolderTypeInfoEnumerable];

	public static RegistryKey OpenFolderTypesKey(bool writable = false)
		=> Registry.LocalMachine.OpenSubKey(FolderTypesRegistryKeyPath, writable)
			?? throw new InvalidDataException();

	public Guid ID { get; } = id;
	public RegistryKey RegKey { get; } = Registry.LocalMachine.OpenSubKey(@$"{FolderTypesRegistryKeyPath}\{id:B}", writable)
		?? throw new InvalidDataException();

	public FolderTypeInfo(FolderTypeID id, bool writable = false)
		: this(id.Value, writable)
	{
	}

	public void Dispose()
	{
		RegKey.Dispose();
		GC.SuppressFinalize(this);
	}

	public object? GetItemValue(string name)
		=> RegKey.GetValue(name);

	public object? GetItemValue(string name, object? defaultValue)
		=> RegKey.GetValue(name, defaultValue);

	public void SetItemValue(string name, object? value)
	{
		if (value != null)
			RegKey.SetValue(name, value);
		else
			RegKey.DeleteValue(name);
	}

	public ImmutableArray<string> RegistedItemKeys
		=> ImmutableCollectionsMarshal.AsImmutableArray(RegKey.GetValueNames());

	public ImmutableDictionary<string, object?> RegistedItems
		=> RegistedItemKeys.Select(name => KeyValuePair.Create(name, RegKey.GetValue(name))).ToImmutableDictionary();

	public string? CanonicalName
	{
		get => GetItemValue("CanonicalName") as string;
		set => SetItemValue("CanonicalName", value);
	}
}
