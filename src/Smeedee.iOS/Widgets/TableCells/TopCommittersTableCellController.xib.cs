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
		
		public TopCommittersTableCellController () : base("TopCommittersTableCellController", null) { }
		
        public override UITableViewCell TableViewCell
        {
            get { return cell; }
        }
		
		public void BindDataToCell(Committer committer, float percent)
        {
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			
			nameLabel.Text = committer.Name;
			commitLabel.Text = committer.Commits.ToString();
			nameLabel.TextColor = StyleExtensions.lightGrayText;
			
			StyleProgressBar(percent);
			
			DisplayImage(committer.ImageUri);
        }
		
		private void StyleProgressBar(float percent)
		{
			float width = (float) (Platform.ScreenWidth - graph.Frame.X - commitLabel.Frame.Width - 25);
			
			graph.Frame = new RectangleF(graph.Frame.X, graph.Frame.Y, width, barHeight);
			graph.BackgroundColor = StyleExtensions.smeedeeOrangeAlpha;
			
			graphTop.Frame = new RectangleF(graph.Frame.X, graph.Frame.Y, width * percent, barHeight);
			graphTop.BackgroundColor = StyleExtensions.smeedeeOrange;
		}
		
		private void DisplayImage(Uri uri)
		{
			UIImageLoader.LoadImageFromUri(uri, (image) => {
				InvokeOnMainThread(() => {
					imageView.Image = image;
				});
			});
		}
	}
}
