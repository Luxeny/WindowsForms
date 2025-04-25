public class DirectoryModel : IModel
{
    public void SynchronizeDirectories(string sourcePath, string targetPath, Action<string> logAction)
    {
        SyncDirectories(sourcePath, targetPath, logAction);
        SyncDirectories(targetPath, sourcePath, logAction);
    }

    private void SyncDirectories(string sourcePath, string targetPath, Action<string> logAction)
    {
        if (!Directory.Exists(targetPath))
        {
            Directory.CreateDirectory(targetPath);
            logAction($"Директория \"{targetPath}\" создана");
        }

        var sourceFiles = Directory.GetFiles(sourcePath);
        var targetFiles = Directory.GetFiles(targetPath);

        foreach (var targetFile in targetFiles)
        {
            var fileName = Path.GetFileName(targetFile);
            var sourceFile = Path.Combine(sourcePath, fileName);

            if (!File.Exists(sourceFile))
            {
                File.Delete(targetFile);
                logAction($"Файл \"{fileName}\" удален");
            }
        }

        foreach (var sourceFile in sourceFiles)
        {
            var fileName = Path.GetFileName(sourceFile);
            var targetFile = Path.Combine(targetPath, fileName);

            if (!File.Exists(targetFile))
            {
                File.Copy(sourceFile, targetFile);
                logAction($"Файл \"{fileName}\" создан");
            }
            else
            {
                var sourceFileInfo = new FileInfo(sourceFile);
                var targetFileInfo = new FileInfo(targetFile);

                if (sourceFileInfo.LastWriteTime > targetFileInfo.LastWriteTime)
                {
                    File.Copy(sourceFile, targetFile, true);
                    logAction($"Файл \"{fileName}\" изменен");
                }
            }
        }

        var sourceSubDirs = Directory.GetDirectories(sourcePath);
        var targetSubDirs = Directory.GetDirectories(targetPath);

        foreach (var targetDir in targetSubDirs)
        {
            var dirName = Path.GetFileName(targetDir);
            var sourceDir = Path.Combine(sourcePath, dirName);

            if (!Directory.Exists(sourceDir))
            {
                Directory.Delete(targetDir, true);
                logAction($"Директория \"{dirName}\" удалена");
            }
        }

        foreach (var sourceDir in sourceSubDirs)
        {
            var dirName = Path.GetFileName(sourceDir);
            var targetDir = Path.Combine(targetPath, dirName);
            SyncDirectories(sourceDir, targetDir, logAction);
        }
    }
