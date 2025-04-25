public class DirectoryPresenter : IPresenter
{
    private readonly IView _view;
    private readonly IModel _model;

    public DirectoryPresenter(IView view, IModel model)
    {
        _view = view;
        _model = model;
        _view.SynchronizeClicked += (sender, args) => Synchronize();
    }

    public void Synchronize()
    {
        if (string.IsNullOrEmpty(_view.SourceDirectory) || string.IsNullOrEmpty(_view.TargetDirectory))
        {
            MessageBox.Show("Пожалуйста, укажите обе директории", "Ошибка", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (!Directory.Exists(_view.SourceDirectory) || !Directory.Exists(_view.TargetDirectory))
        {
            MessageBox.Show("Одна или обе директории не существуют", "Ошибка", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        _view.ClearLog();
        _view.AddLogMessage("Начало синхронизации...");
        
        try
        {
            _model.SynchronizeDirectories(_view.SourceDirectory, _view.TargetDirectory, _view.AddLogMessage);
            _view.AddLogMessage("Синхронизация завершена успешно!");
        }
        catch (Exception ex)
        {
            _view.AddLogMessage($"Ошибка при синхронизации: {ex.Message}");
        }
    }
}
