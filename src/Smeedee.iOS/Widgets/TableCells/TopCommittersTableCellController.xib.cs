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
			
			FadeInImage(committer.ImageUri);
        }
		
		private void StyleProgressBar(float percent)
		{
			graph.Frame = new RectangleF(graph.Frame.X, graph.Frame.Y, graph.Frame.Width, barHeight);
			graph.BackgroundColor = StyleExtensions.smeedeeOrangeAlpha;
			
			graphTop.Frame = new RectangleF(graph.Frame.X, graph.Frame.Y, graph.Frame.Width * percent, barHeight);
			graphTop.BackgroundColor = StyleExtensions.smeedeeOrange;
		}
		
		private void FadeInImage(Uri uri)
		{
			imageView.Alpha = 0.4f;
			UIImageLoader.LoadImageFromUri(uri, (image) => {
				InvokeOnMainThread(() => {
					UIView.BeginAnimations("FadeImage");
					UIView.SetAnimationDuration(0.5f);
					imageView.Image = image;
					imageView.Alpha = 1.0f;
					UIView.CommitAnimations();
				});
			});
		}
	}
}
