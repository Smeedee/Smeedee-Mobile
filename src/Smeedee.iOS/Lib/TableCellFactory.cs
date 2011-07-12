using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Smeedee.iOS
{
	public class TableCellFactory
	{
        protected Dictionary<int, TableViewCellController> cellControllers;
        
		private NSString cellNibName;
		protected Type controllerType;
		
		public TableCellFactory(string cellNibName, Type controllerType)
		{
            this.cellControllers = new Dictionary<int, TableViewCellController>();
            
			this.controllerType = controllerType;
			this.cellNibName = new NSString(cellNibName);
		}
		
		public TableViewCellController NewTableCellController(UITableView tableView, NSIndexPath indexPath)
		{
			if (controllerType == null)
				throw new InvalidOperationException("You can only call this method when you have created the factory with a specified controller type");
			
			UITableViewCell cell = tableView.DequeueReusableCell(cellNibName);
			TableViewCellController cellController = null;
			
			if (cell == null) 
			{
				cellController = Activator.CreateInstance(controllerType) as TableViewCellController;
				NSBundle.MainBundle.LoadNib(cellNibName, cellController, null);
				
				cell = cellController.TableViewCell;
				cell.Tag = GenerateUniqueCellTag(indexPath);
				if (!cellControllers.ContainsKey(cell.Tag))
                {
					cellControllers.Add(cell.Tag, cellController);
				}
			}
			else
			{
				cellController = cellControllers[cell.Tag];
			}
			
			return cellController;
		}
		
		private int GenerateUniqueCellTag(NSIndexPath indexPath)
		{
			return Environment.TickCount + (indexPath.Section * 100) + indexPath.Row;
		}
	}
}
