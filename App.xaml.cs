using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAppExceptionDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)

        {
            base.OnStartup(e);

            // AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
            // {
            //     Debugger.Break();
            // };
            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                Debugger.Break();
            };
            AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) =>
            {
                Debugger.Break();
            };

            Dispatcher.UnhandledException += (sender, args) =>
            {
                Debugger.Break();
            };
            DispatcherUnhandledException += (sender, eventArgs) =>
            {
                Debugger.Break();
            };
        }
    }
}