using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Smeedee.Model;

namespace Smeedee.WP7.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private Login _login;
        public LoginViewModel()
        {
            _login = new Login();
        }

        public string Key
        {
            get
            {
                return _login.Key;
            }
            set
            {
                if (value != _login.Key)
                {
                    _login.Key = value;
                    NotifyPropertyChanged("Key");
                }
            }
        }
        
        public string Url
        {
            get
            {
                return _login.Url;
            }
            set
            {
                if (value != _login.Url)
                {
                    _login.Url = value;
                    NotifyPropertyChanged("Url");
                }
            }
        }

        private bool _isValidating;
        public bool IsValidating
        {
            get
            {
                return _isValidating;
            }
            set
            {
                if (value != _isValidating)
                {
                    _isValidating = value;
                    NotifyPropertyChanged("IsValidating");
                }
            }
        }

        private bool _validationFailed;
        public bool ValidationFailed
        {
            get
            {
                return _validationFailed;
            }
            set
            {
                if (value != _validationFailed)
                {
                    _validationFailed = value;
                    NotifyPropertyChanged("ValidationFailed");
                }
            }
        }
    }
}
