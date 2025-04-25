public interface IView
{
    string SourceDirectory { get; set; }
    string TargetDirectory { get; set; }
    event EventHandler SynchronizeClicked;
    void AddLogMessage(string message);
    void ClearLog();
}
