using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SafeboardSnake.WPF.Services;

namespace SafeboardSnake.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static RestService _restService;

        public static RestService RestService
        {
            get
            {
                if (_restService == null)
                {
                    _restService = new RestService();
                }

                return _restService;
            }
        }
    }
}
