using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASiNet.FSTools.Models;
public static class DirectorySearch
{
    public static bool PathIsParentOrRoot(string path, string current) =>
        path == Path.GetDirectoryName(current) || path == "\\";

    public static IEnumerable<string> Search(string path)
    {
        if (Directory.Exists(path))
        {
            foreach (var item in Directory.GetDirectories(path).Where(x => !File.GetAttributes(x).HasFlag(FileAttributes.Hidden)))
            {
                yield return item;
            }
        }
        else
        {
            var parent = Path.GetDirectoryName(path);
            if(!Directory.Exists(parent))
                yield break;
            var np = parent.LastOrDefault() == '\\' ? parent : parent + '\\';
            var dn = Path.GetFileNameWithoutExtension(path);
            foreach (var item in Directory.GetDirectories(np, $"{dn}*").Where(x => !File.GetAttributes(x).HasFlag(FileAttributes.Hidden)))
            {
                yield return item;
            }
        }
    }

}
