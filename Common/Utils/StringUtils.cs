using System;
using System.IO;

namespace Common.Utils
{
    public static class StringUtils
    {
        public static string GetRelativePath(string directory, string filePath)
        {
            Uri pathUri = new Uri(filePath);
            if (!directory.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                directory += Path.DirectorySeparatorChar;
            }
            Uri folderUri = new Uri(directory);
            return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace('/', Path.DirectorySeparatorChar));

        }
    }
}
