using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Smeedee.Model;
using Smeedee.WP7.Converters;

namespace Smeedee.WP7.ViewModels.Widgets
{
    public class BuildStatusViewModel
    {
        public ObservableCollection<BuildStatusItemViewModel> Items { get; private set; }
        private readonly BuildStatus model;

        public BuildStatusViewModel()
        {
            model = new BuildStatus();
            Items = new ObservableCollection<BuildStatusItemViewModel>();
        }

        public void LoadData()
        {
            model.Load(() => Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                Items.Clear();
                foreach (var build in model.Builds)
                {
                    var statusImageUri = "";

                    if (build.BuildSuccessState == BuildState.Broken) statusImageUri = "../Resources/Images/icon_buildfailure.png";
                    if (build.BuildSuccessState == BuildState.Working) statusImageUri = "../Resources/Images/icon_buildsuccess.png";
                    if (build.BuildSuccessState == BuildState.Unknown) statusImageUri = "../Resources/Images/icon_buildunknown.png";
                    
                    Items.Add(new BuildStatusItemViewModel 
                    {   ProjectName = build.ProjectName, 
                        UserName = build.Username, 
                        BuildTime = (DateTime.Now - build.BuildTime).PrettyPrint(),
                        BuildStatusImage = statusImageUri
                    });
                }
            }));
        }
    }
}
