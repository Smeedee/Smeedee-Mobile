using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using Smeedee.Model;


namespace Smeedee.WP7
{
    public class TopCommittersViewModel : INotifyPropertyChanged
    {
        public TopCommittersViewModel()
        {
            this.model = new TopCommitters();
            this.Items = new ObservableCollection<TopCommittersItemViewModel>();
        }
        public ObservableCollection<TopCommittersItemViewModel> Items { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        private TopCommitters model;

        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {
            model.Load(() => Deployment.Current.Dispatcher.BeginInvoke( () =>
            {
                foreach (var committer in model.Committers)
                {
                    Items.Add(new TopCommittersItemViewModel { Name = committer.Name, Commits = committer.Commits + "", LineThree = "blah" });
                }                      
            }));
            IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}