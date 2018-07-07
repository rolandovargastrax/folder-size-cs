using System;

namespace FolderSize {
    class Program {
        static void Main(string[] args) {
            if (args.Length == 0) {
                Console.WriteLine("Missing path to calculate.");
                Console.WriteLine("usage: FolderSize '<path>'");
                return;
            }
            FolderSizeAnalyzer analyzer = new FolderSizeAnalyzer();
            FolderSizeAnalyzerResults results = analyzer.CalculateFolderSize(args[0]);
            Console.WriteLine("------------------------------");
            Console.WriteLine(results.ToString());
            foreach (FolderSizeAnalyzerResults subDir in results.SubDirectories) {
                Console.WriteLine("\t" + subDir.ToString());
            }
        }
    }
}
