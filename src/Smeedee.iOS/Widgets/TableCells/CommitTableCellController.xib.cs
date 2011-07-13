
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee;
using Smeedee.Model;
using System.Drawing;

namespace Smeedee.iOS
{
	public partial class CommitTableCellController : TableViewCellController
	{
		
		public CommitTableCellController() : base("CommitTableCellController", null)
        {
        }
        
        public override UITableViewCell TableViewCell
        {
            get { return cell; }
        }
        
        public void BindDataToCell(Changeset commit)
        {
			username.Text = commit.User;
			message.Text = commit.Message;
			timestamp.Text = (DateTime.Now-commit.Date).PrettyPrint();
			
			message.SizeToFit();
			var bounds = timestamp.Frame;
			var heightDiff = message.SizeThatFits(new SizeF(240, float.MaxValue)).Height - bounds.Height;
			timestamp.Frame = RectangleF.FromLTRB(bounds.Left, bounds.Top+heightDiff, bounds.Right, bounds.Bottom+heightDiff);
        }
	}
}

