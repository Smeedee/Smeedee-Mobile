using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iOS
{
	public partial class LabelTextInputTableCellController : TableViewCellController
	{
		public LabelTextInputTableCellController() : base("LabelTextInputTableCellController", null)
        {
        }
		public UITextField TextInput 
		{	
			get { return this.textInput;}
		}
        
        public override UITableViewCell TableViewCell
        {
            get { return cell; }
        }
		
        public void BindDataToCell(string currentValue)
        {
			this.textInput.Text = currentValue;
			
			cell.StyleAsSettingsTableCell();
			cell.BackgroundColor = UIColor.White;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        }
		
		public void BindActionToReturn(Action<UITextField> action)
		{
			textInput.ShouldReturn = delegate(UITextField textField) {
				action(textField);
				textField.ResignFirstResponder();
				return true;
			};
		}
	}
}

