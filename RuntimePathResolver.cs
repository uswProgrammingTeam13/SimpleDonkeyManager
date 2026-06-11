using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleDonkeyManager
{
    internal static class RuntimePathResolver
    {
        private const int MaxParentDepth = 8;

        public static string? FindPythonScript(string scriptName)
        {
            return FindFile("python", scriptName) ?? FindFile(scriptName);
        }

        public static string? FindLocalVenvPython()
        {
            return FindFile("donkey_env", "Scripts", "python.exe");
        }

        public static string? FindFile(params string[] relativePathParts)
        {
            foreach (string root in GetSearchRoots())
            {
                string candidate = Path.Combine(new[] { root }.Concat(relativePathParts).ToArray());
                if (File.Exists(candidate))
                {
                    return Path.GetFullPath(candidate);
                }
            }

            return null;
        }

        public static IReadOnlyList<string> GetSearchRoots()
        {
            var roots = new List<string>();
            AddRootWithParents(roots, AppDomain.CurrentDomain.BaseDirectory);
            AddRootWithParents(roots, Directory.GetCurrentDirectory());
            return roots;
        }

        private static void AddRootWithParents(List<string> roots, string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return;

            DirectoryInfo? currentDir;
            try
            {
                currentDir = new DirectoryInfo(Path.GetFullPath(path));
            }
            catch
            {
                return;
            }

            for (int i = 0; i < MaxParentDepth && currentDir != null; i++)
            {
                AddDistinctRoot(roots, currentDir.FullName);
                currentDir = currentDir.Parent;
            }
        }

        private static void AddDistinctRoot(List<string> roots, string path)
        {
            foreach (string root in roots)
            {
                if (string.Equals(root, path, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            roots.Add(path);
        }
    }
}
