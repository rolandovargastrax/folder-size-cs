using System.Collections.Generic;

namespace FolderSize;

/// <summary>
/// Comparer used to sort the folders from most to lease space consumed
/// </summary>
public class FolderSizeAnalyzerResultsComparer : IComparer<FolderSizeAnalyzerResults> {
	public int Compare(FolderSizeAnalyzerResults x, FolderSizeAnalyzerResults y) {
		return y.SizeInBytes.CompareTo(x.SizeInBytes);
	}
}
