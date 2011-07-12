using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

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
        
        public void BindDataToCell(string projectName, DateTime buildTime, string status)
        {
            // TODO: pass in a single parameter: a BuildStatus object from the model
            projectNameLabel.Text = projectName;
            lastBuildTimeLabel.Text = "Last build was " + buildTime.ToString();
            buildStatusLabel.Text = status;
        }
    }
}
