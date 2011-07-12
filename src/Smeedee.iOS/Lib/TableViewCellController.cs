using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Smeedee.iOS
{
	public abstract class TableViewCellController : UIViewController
	{
		public TableViewCellController()
		{
		}

		public TableViewCellController(string nibName, NSBundle bundle) : base(nibName, bundle)
		{
		}

		public abstract UITableViewCell TableViewCell { get; }
	}
}
