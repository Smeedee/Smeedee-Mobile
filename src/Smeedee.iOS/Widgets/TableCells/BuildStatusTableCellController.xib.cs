using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;
using Smeedee.iOS.Lib;
using Smeedee.Lib;

namespace Smeedee.iOS
{
    public partial class BuildStatusTableCellController : TableViewCellController
    {
        public BuildStatusTableCellController() : base("BuildStatusTableCellController", null)
        {
        }
        
        public override UITableViewCell TableViewCell
        {
            get { return cell; }
        }
        
        public void BindDataToCell(Build build)
        {
            projectNameLabel.Text = build.ProjectName;
			usernameLabel.Text = build.Username;
			lastBuildTimeLabel.Text = TimeSpanPrettyPrintExtension.PrettyPrint(DateTime.Now - build.BuildTime);
			
			projectNameLabel.TextColor = StyleExtensions.smeedeeOrange;
			usernameLabel.TextColor = StyleExtensions.lightGrayText;
			
			switch (build.BuildSuccessState) {
			case BuildState.Broken:
				InvokeOnMainThread(() => image.Image = UIImage.FromFile("Images/icon_buildfailure.png"));
				break;
			case BuildState.Unknown:
				image.Image = UIImage.FromFile("Images/icon_buildunknown.png");
				break;
			case BuildState.Working:
				InvokeOnMainThread(() => image.Image = UIImage.FromFile("Images/icon_buildsuccess.png"));
				break;
			}
			
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        }
    }
}
