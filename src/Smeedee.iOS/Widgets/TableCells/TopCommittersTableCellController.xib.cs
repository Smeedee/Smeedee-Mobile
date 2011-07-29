using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;
using Smeedee.iOS.Lib;

namespace Smeedee.iOS
{
	public partial class TopCommittersTableCellController : TableViewCellController
	{
		public TopCommittersTableCellController () : base("TopCommittersTableCellController", null)
		{
		}
		
        public override UITableViewCell TableViewCell
        {
            get { return cell; }
        }
		
		public void BindDataToCell(Committer committer, float percent)
        {
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			
			nameLabel.Text = committer.Name;
			commitLabel.Text = committer.Commits.ToString();
            
			//graph.BackgroundColor = UIColor.FromRGB(20, 20, 20);
			//commitLabel.TextColor = UIColor.FromRGB(70, 70, 70);
			
			graph.Frame = new RectangleF(graph.Frame.X, graph.Frame.Y, percent * (graph.Frame.Width - 50f), graph.Frame.Height);
			commitLabel.Frame = new RectangleF(graph.Frame.X + graph.Frame.Width + 5, graph.Frame.Y, 50f, commitLabel.Frame.Height);
			
			new UIImageLoader().LoadImageFromUri(committer.ImageUri, (image) => {
				InvokeOnMainThread(() => imageView.Image = image);
			});
			
        }
	}
}
