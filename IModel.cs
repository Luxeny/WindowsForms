public interface IModel
{
    void SynchronizeDirectories(string sourcePath, string targetPath, Action<string> logAction);
}
