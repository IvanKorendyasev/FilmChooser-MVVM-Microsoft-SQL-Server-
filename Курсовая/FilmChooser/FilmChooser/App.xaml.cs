using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FilmChooser
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        //Глобальный обработчик ошибок
        private static void GlobalException(object sender, UnhandledExceptionEventArgs args)
        {
            try
            {
                Exception exception = (Exception)args.ExceptionObject;
            }
            finally
            {
                MessageBox.Show("Смотри не налажай в следующий раз. Это будет фиаско.");
                MessageBox.Show("Это фиаско, братан!");
                Environment.Exit(0);
            }
        }
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(GlobalException);
        }

    }
}
