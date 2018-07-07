using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace FolderSize {

	/// <summary>
	/// Class to house the results from analyzing the folder
	/// </summary>
	public class FolderSizeAnalyzerResults {
		private string name;
		public string Name {
			get { return name; }
			set { name = value; }
		}

		private string fullPath;
		public string FullPath {
			get { return fullPath; }
			set { fullPath = value; }
		}

		private long sizeInBytes = 0;
		public long SizeInBytes {
			get {
				return sizeInBytes;
			}
			set {
				sizeInBytes = value;
			}
		}

		private int fileCount = 0;
		public int FileCount {
			get { return fileCount; }
			set { fileCount = value; }
		}

		private int totalFileCount = 0;
		public int TotalFileCount {
			get { return totalFileCount; }
			set { totalFileCount = value; }
		}

		private List<FolderSizeAnalyzerResults> subDirectories = new List<FolderSizeAnalyzerResults>();
		public List<FolderSizeAnalyzerResults> SubDirectories {
			get {
				return subDirectories;
			}
			set {
				subDirectories = value;
			}
		}

		public double SizeInKilobytes {
			get {
				return this.SizeInBytes / 1024f;

			}
		}
		public string SizeInKilobytesFriendly {
			get {
				return string.Format("{0:0.##} KB", this.SizeInKilobytes);
			}
		}

		public double SizeInMegabytes {
			get {
				return this.SizeInKilobytes / 1024f;

			}
		}
		public string SizeInMegabytesFriendly {
			get {
				return string.Format("{0:0.##} MB", this.SizeInMegabytes);
			}
		}

		public double SizeInGigabytes {
			get {
				return this.SizeInMegabytes / 1024f;

			}
		}
		public string SizeInGigabytesFriendly {
			get {
				return string.Format("{0:0.##} GB", this.SizeInGigabytes);
			}
		}

		/// <summary>
		/// overrides the ToString method to display the results better when debugging
		/// </summary>
		override public string ToString() {
			string size = string.Empty;
			if (this.SizeInGigabytes > 1) {
				size = this.SizeInGigabytesFriendly;
			} else if (this.SizeInMegabytes > 1) {
				size = this.SizeInMegabytesFriendly;
			} else {
				size = this.SizeInKilobytesFriendly;
			}

			return string.Format("{0} - {1} - {2} files", this.Name, size, this.TotalFileCount);
		}
	}

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

		/// <summary>
		/// Comparer used to sort the folders from most to lease space consumed
		/// </summary>
		public class FolderSizeAnalyzerResultsComparer : IComparer<FolderSizeAnalyzerResults> {
			public int Compare(FolderSizeAnalyzerResults x, FolderSizeAnalyzerResults y) {
				return y.SizeInBytes.CompareTo(x.SizeInBytes);
			}
		}
	}
}
