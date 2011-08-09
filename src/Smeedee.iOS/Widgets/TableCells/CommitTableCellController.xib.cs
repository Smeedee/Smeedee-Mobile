using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.iOS.Lib;
using Smeedee;
using Smeedee.Model;

namespace Smeedee.iOS
{
	public partial class CommitTableCellController : TableViewCellController
	{
		public CommitTableCellController() : base("CommitTableCellController", null) { }
        
        public override UITableViewCell TableViewCell
        {
            get { return cell; }
        }
        
        public void BindDataToCell(Commit commit, bool highlightEmpty)
        {
			username.Text = commit.User;
			message.Text = commit.Message;
			timestamp.Text = (DateTime.Now-commit.Date).PrettyPrint();
			
			if (commit.Message == "" && highlightEmpty)
			{
				message.Text = "No commit message";
				message.TextColor = UIColor.Red;
			}
			
			UIImageLoader.LoadImageFromUri(commit.ImageUri, (img) => {
				InvokeOnMainThread(() => image.Image = img);
			});
			
			message.SizeToFit();
			var bounds = timestamp.Frame;
			var heightDiff = message.SizeThatFits(new SizeF(240, float.MaxValue)).Height - bounds.Height;
			timestamp.Frame = RectangleF.FromLTRB(bounds.Left, bounds.Top+heightDiff, bounds.Right, bounds.Bottom+heightDiff);
        }
	}
}
