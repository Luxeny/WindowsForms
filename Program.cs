static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        
        var view = new MainForm();
        var model = new DirectoryModel();
        var presenter = new DirectoryPresenter(view, model);
        
        Application.Run(view);
    }
}
