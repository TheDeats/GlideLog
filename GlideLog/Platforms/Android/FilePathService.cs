using GlideLog.DirectoryManagement;
using Environment = Android.OS.Environment;

[assembly: Dependency(typeof(GlideLog.Platforms.Android.FilePathService))]
namespace GlideLog.Platforms.Android
{
    public class FilePathService : IFilePathService
    {
        public string GetInternalDocumentsPath()
        {
            return Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDocuments)!.AbsolutePath;
        }
    }
}
