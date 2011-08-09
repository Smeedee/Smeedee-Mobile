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
                    NotifyPropertyChanged("StatusColor");
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
                    NotifyPropertyChanged("StatusColor");
                }
            }
        }

        private string _statusText = "";
        public string StatusText
        {
            get
            {
                return _statusText;
            }
            set
            {
                if (value != _statusText)
                {
                    _statusText = value;
                    NotifyPropertyChanged("StatusText");
                }
            }
        }

        public SolidColorBrush StatusColor
        {
            get
            {
                if (IsValidating) return new SolidColorBrush(Colors.White);
                if (ValidationFailed) return new SolidColorBrush(Colors.Red);
                return new SolidColorBrush(Colors.White);
            }
        }

        public event EventHandler OnSuccessfulValidation;

        public void Validate()
        {
            IsValidating = true;
            StatusText = "Validating...";
            _login.IsValid(valid => Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                IsValidating = false;
                ValidationFailed = !valid;
                if (valid)
                {
                    StatusText = "Successfully validated url and key!";
                    if (OnSuccessfulValidation != null) OnSuccessfulValidation(this, null);
                } else
                {
                    StatusText = "Validation failed";
                }
            }));
        }
    }
}
