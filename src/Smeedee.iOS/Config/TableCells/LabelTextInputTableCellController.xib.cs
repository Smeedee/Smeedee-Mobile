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
        
        public override UITableViewCell TableViewCell
        {
            get { return cell; }
        }
		
		public UITextField TextField
		{
			get { return textInput; }
		}
        
        public void BindDataToCell(string labelText, string currentValue)
        {
			this.label.Text = labelText;
			this.textInput.Text = currentValue;
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

