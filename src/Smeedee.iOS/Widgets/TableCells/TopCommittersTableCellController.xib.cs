
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;


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
			nameLabel.Text = committer.Name;
			commitLabel.Text = committer.Commits.ToString();
			commitLabel.Text += committer.Commits == 1 ? " commit" : " commits";
        }
	}
}

