using System;
using System.IO;

namespace FolderSize;

/// <summary>
/// Class to calculate the size of the folder
/// </summary>
public class FolderSizeAnalyzer {

	/// <summary>
	/// Calculates the size of the folder given
	/// </summary>
	public FolderSizeAnalyzerResults CalculateFolderSize(string path) {
		DirectoryInfo dirInfo = new DirectoryInfo(path);
		return this.CalculateFolderSize(dirInfo);
	}

	/// <summary>
	/// Calculates the size of the folder given
	/// </summary>
	public FolderSizeAnalyzerResults CalculateFolderSize(DirectoryInfo dirInfo) {
		FolderSizeAnalyzerResults result = new FolderSizeAnalyzerResults();
		result.Name = dirInfo.Name;
		result.FullPath = dirInfo.FullName;
		try {
			// get all the files from the folder
			FileInfo[] folderFiles = dirInfo.GetFiles();
			foreach (FileInfo currentFile in folderFiles) {
				result.SizeInBytes += currentFile.Length;
				result.FileCount++;
				result.TotalFileCount++;
			}

			// get the size from the children folders
			DirectoryInfo[] subDirectories = dirInfo.GetDirectories();
			foreach (DirectoryInfo currentDir in subDirectories) {
				FolderSizeAnalyzerResults subDirAnalysis = CalculateFolderSize(currentDir);
				if (subDirAnalysis.SizeInBytes > 0) {
					result.SizeInBytes += subDirAnalysis.SizeInBytes;
					result.TotalFileCount += subDirAnalysis.TotalFileCount;
					result.SubDirectories.Add(subDirAnalysis);
				}
			}
			result.SubDirectories.Sort(new FolderSizeAnalyzerResultsComparer());
		} catch {
			Console.WriteLine("Cannot access '{0}'", dirInfo.FullName);
		}
		return result;
	}
}
