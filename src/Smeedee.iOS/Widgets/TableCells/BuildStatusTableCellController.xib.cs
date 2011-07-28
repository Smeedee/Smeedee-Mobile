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
            buildStatusLabel.Text = "";//build.BuildSuccessState.ToString();
            /*
            buildStatusLabel.TextColor = (build.BuildSuccessState == BuildState.Broken)
                ? UIColor.Red
                : UIColor.FromRGB(50, 200, 50);
            */
			
			//cell.BackgroundColor = StyleExtensions.grayTableCell;
			
			//cell.ContentView.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("images/logo.png"));
			
			/*projectNameLabel.TextColor = UIColor.Black;
			usernameLabel.TextColor = UIColor.DarkGray;
			lastBuildTimeLabel.TextColor = UIColor.DarkGray;*/
			
			switch (build.BuildSuccessState) {
			case BuildState.Broken:
				//cell.BackgroundColor = UIColor.FromRGB(50, 10, 10);
				
				buildStatusLabel.Text = BuildState.Broken.ToString();
				buildStatusLabel.TextColor = UIColor.Red;
				
				break;
			/*case BuildState.Unknown:
				cell.BackgroundColor = UIColor.FromRGB(20, 20, 40);
				break;*/
			case BuildState.Working:
				//cell.BackgroundColor = UIColor.FromRGB(10, 60, 10);
				buildStatusLabel.Text = BuildState.Working.ToString();
				buildStatusLabel.TextColor = UIColor.FromRGB(50, 200, 50);
				break;
			}
			
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        }
    }
}
