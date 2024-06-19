namespace GlideLog.DirectoryManagement
{
    public static class FilePathHelper
    {
        public static string GetInternalDocumentsPath()
        {
            var filePathService = DependencyService.Get<IFilePathService>();
            return filePathService?.GetInternalDocumentsPath() ?? string.Empty;
        }
    }
}
