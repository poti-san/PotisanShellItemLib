namespace PotisanShellItemLib.Core;

/// <summary>
/// Windowsのファイル日時表現。
/// </summary>
public readonly record struct FileTime(uint LowDateTime, uint HighDateTime);
