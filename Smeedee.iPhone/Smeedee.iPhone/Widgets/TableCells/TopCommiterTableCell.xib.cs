using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iPhone
{
	public partial class TopCommiterTableCell : UIViewController
	{
		public TopCommiterTableCell() : base("TopCommiterTableCell", null)
		{
		}
		
		public UITableViewCell Cell
		{
			get { return cell; }
			set { cell = value; }
		}
		
		public void BindDataToCell(string user, int commits)
		{
			ApplyStyle();
			
			userLabel.Text = user;
			commitsLabel.Text = commits + " commits";
			avatarImage.Image = UIImage.FromFile("Images/user.png");
		}
		
		private void ApplyStyle()
		{
			cell.ContentView.BackgroundColor = UIColor.DarkGray;
			userLabel.TextColor = UIColor.White;
			commitsLabel.TextColor = UIColor.White;
		}
	}
}
