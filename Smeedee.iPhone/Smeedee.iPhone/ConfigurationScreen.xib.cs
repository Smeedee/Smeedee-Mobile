using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iPhone
{
	public partial class ConfigurationScreen : UINavigationController
	{
		#region Constructors

		public ConfigurationScreen(IntPtr handle) : base(handle)
		{
		}

		[Export ("initWithCoder:")]
		public ConfigurationScreen(NSCoder coder) : base(coder)
		{
		}

		public ConfigurationScreen() : base("ConfigurationScreen", null)
		{
		}
		
		#endregion
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
			var configurationTableController = new ConfigurationTableViewController();
			PushViewController(configurationTableController, true);
		}
	}
	
	public class ConfigurationTableViewController : UITableViewController
	{
		public ConfigurationTableViewController()
			: base(UITableViewStyle.Grouped)
		{
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
			Title = "Configuration";
			TableView.Source = new ConfigurationTableSource();
		}
	}
	
	public class ConfigurationTableSource : UITableViewSource
	{
		public override int NumberOfSections(UITableView tableView)
		{
			return 2;
		}
		
		public override int RowsInSection(UITableView tableview, int section)
		{
			if (section.IsYourWidgets()) return 3;
			if (section.IsAvailableWidgets()) return 6;
			
			throw new Exception("No section at index " + section);
		}
		
		public override string TitleForHeader(UITableView tableView, int section)
		{
			if (section.IsYourWidgets()) return "Your widgets";
			if (section.IsAvailableWidgets()) return "Available widgets";
			
			throw new Exception("No section at index " + section);
		}
		
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("ConfigurationCell") ??
					   new UITableViewCell(UITableViewCellStyle.Subtitle, "ConfigurationCell");
			
			cell.TextLabel.Text = "Cell " + indexPath.Row;
			cell.DetailTextLabel.Text = "Section " + indexPath.Section;
			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			
			return cell;
		}
		
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			Console.WriteLine("The user selected row {0} in section {1}", indexPath.Row, indexPath.Section);
		}
	}
	
	public static class ConfigurationTableSourceExtensions
	{
		public static bool IsYourWidgets(this int section)
		{
			return section == 0;
		}
		
		public static bool IsAvailableWidgets(this int section)
		{
			return section == 1;
		}
	}
}
