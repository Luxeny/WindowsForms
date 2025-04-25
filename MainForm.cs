public partial class MainForm : Form, IView
{
    public MainForm()
    {
        InitializeComponent();
    }

    public string SourceDirectory
    {
        get => txtSourceDir.Text;
        set => txtSourceDir.Text = value;
    }

    public string TargetDirectory
    {
        get => txtTargetDir.Text;
        set => txtTargetDir.Text = value;
    }

    public event EventHandler SynchronizeClicked;

    public void AddLogMessage(string message)
    {
        if (InvokeRequired)
        {
            Invoke(new Action<string>(AddLogMessage), message);
            return;
        }
        
        txtLog.AppendText($"{DateTime.Now:HH:mm:ss} - {message}{Environment.NewLine}");
    }

    public void ClearLog()
    {
        if (InvokeRequired)
        {
            Invoke(new Action(ClearLog));
            return;
        }
        
        txtLog.Clear();
    }

    private void btnBrowseSource_Click(object sender, EventArgs e)
    {
        using (var dialog = new FolderBrowserDialog())
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SourceDirectory = dialog.SelectedPath;
            }
        }
    }

    private void btnBrowseTarget_Click(object sender, EventArgs e)
    {
        using (var dialog = new FolderBrowserDialog())
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                TargetDirectory = dialog.SelectedPath;
            }
        }
    }

    private void btnSynchronize_Click(object sender, EventArgs e)
    {
        SynchronizeClicked?.Invoke(this, EventArgs.Empty);
    }
}
