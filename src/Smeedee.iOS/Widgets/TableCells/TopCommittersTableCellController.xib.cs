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
		private static int barHeight = 5;
		
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
			StyleProgressBar(percent);
			
			UIImageLoader.LoadImageFromUri(committer.ImageUri, (image) => {
				InvokeOnMainThread(() => imageView.Image = image);
			});
        }
		
		private void StyleProgressBar(float percent)
		{
			graph.Frame = new RectangleF(graph.Frame.X, graph.Frame.Y, graph.Frame.Width, barHeight);
			graph.BackgroundColor = StyleExtensions.smeedeeOrangeAlpha;
			
			graphTop.Frame = new RectangleF(graph.Frame.X, graph.Frame.Y, graph.Frame.Width * percent, barHeight);
			graphTop.BackgroundColor = StyleExtensions.smeedeeOrange;
		}
	}
}
