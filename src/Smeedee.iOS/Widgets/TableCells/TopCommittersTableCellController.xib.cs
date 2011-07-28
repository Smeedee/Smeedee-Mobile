using System;
using System.Collections.Generic;
using System.Linq;
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
		
		public void BindDataToCell(Committer committer)
        {
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			
			nameLabel.Text = committer.Name;
			commitLabel.TextColor = UIColor.LightGray;
			commitLabel.Text = committer.Commits.ToString();
			commitLabel.Text += committer.Commits == 1 ? " commit" : " commits";
            
			new UIImageLoader().LoadImageFromUri(committer.ImageUri, (image) => {
				InvokeOnMainThread(() => imageView.Image = image);
			});
			
        }
	}
}
