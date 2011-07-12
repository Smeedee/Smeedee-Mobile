using System;
using System.Collections.Generic;
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
        
        public void BindDataToCell(BuildStatus build)
        {
            projectNameLabel.Text = build.ProjectName;
            lastBuildTimeLabel.Text = "Last build was " + build.BuildTime;
            buildStatusLabel.Text = build.BuildSuccessState.ToString();
            
            buildStatusLabel.TextColor = (build.BuildSuccessState == BuildSuccessState.Failure)
                ? UIColor.Red
                : UIColor.Green;
        }
    }
}
