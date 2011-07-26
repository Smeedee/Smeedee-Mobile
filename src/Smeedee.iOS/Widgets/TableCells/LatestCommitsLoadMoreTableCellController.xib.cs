using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iOS
{
	public partial class LatestCommitsLoadMoreTableCellController : TableViewCellController
	{
		public override UITableViewCell TableViewCell {
			get {
				return cell;
			}
		}
		
		public LatestCommitsLoadMoreTableCellController() : base ("LatestCommitsLoadMoreTableCellController", null)
		{
			
		}
		
		public void BindAction(Action callback)
		{
			button.TouchUpInside += delegate {
				callback();
			};	
		}
	}
}

