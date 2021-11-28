using System.Collections.Generic;

namespace FolderSize;

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
