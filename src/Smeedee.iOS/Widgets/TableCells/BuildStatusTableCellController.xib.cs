using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

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
            buildStatusLabel.Text = build.BuildSuccessState.ToString();
            
            buildStatusLabel.TextColor = (build.BuildSuccessState == BuildState.Broken)
                ? UIColor.Red
                : UIColor.FromRGB(50, 200, 50);
            
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        }
    }
}
