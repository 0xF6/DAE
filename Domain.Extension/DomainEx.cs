namespace Domain.Extension
{
    using System;
    using System.Threading;
    using System.IO;
    using System.Security.Policy;
    using Internal;

    public delegate void ExMain();
    public delegate void UnhandledException(Exception e);

    public class DomainEx
    {
        /// <summary>
        /// Domain CE
        /// </summary>
        private static AppDomain _domain;
        /// <summary>
        /// Domain Evidence
        /// </summary>
        private static Evidence _evidence = null;
        private string _name;
        private UnhandledException _handler;
        private readonly ExMain _main;

        public DomainEx(ExMain inAppPoint) { _main = inAppPoint; }

        public void setEvidence(Evidence e) => _evidence = e;
        public void setName(string e) => _name = e;

        public void Init()
        {
            AppDomain.CurrentDomain.SetData("name", $"{_name}-LowLevelDomain");

            Thread.Sleep(200);
            Console.CursorVisible = false;
            Console.Title = "AlfheimCE - Base kernel.";
            Console.SetWindowSize(Console.WindowWidth / 2, 2);
            Console.SetBufferSize(Console.BufferWidth / 2, 2);
            Native.CenterConsole();

            AppDomainSetup info = new AppDomainSetup
            {
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory,
                ApplicationName = $"{_name}",
                LoaderOptimization = LoaderOptimization.MultiDomainHost
            };

            _domain = AppDomain.CreateDomain($"{_name}-HightLevelDomain", _evidence, info);


            _domain.AssemblyLoad += AssemblyLoad;
            _domain.UnhandledException += UnhandledException;

            CrossAppDomainDelegate gate = () =>
            {
                Thread thread = new Thread(() =>
                {
                    _main();
                    // TODO: Add WPF Extension
                    //LogManager.Configuration = new XmlLoggingConfiguration("config\\log.config");
                    //AppDomain.CurrentDomain.Load(typeof(xApp).Assembly.GetName());
                    //Application.ResourceAssembly = typeof(xApp).Assembly;

                    //App app = new App();
                    //app.InitializeComponent();
                    //app.Run();
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            };
            _domain.DoCallBack(gate);
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (_handler == null)
            {
                File.WriteAllText($"{_name}_UnhandledException.log", e.ExceptionObject.ToString());
                Environment.Exit(-1);
            }
            else _handler(e.ExceptionObject as Exception);
        }

        private void AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            string s = $"[§cDomain§f]: Loading {args.LoadedAssembly.GetName().Name}...";
            Terminal.WriteLine($"{s}{new string(' ', (Console.WindowWidth - 1) - s.Length)}");
            Thread.Sleep(200);
            Console.SetCursorPosition(0, 0);
        }
    }
}
