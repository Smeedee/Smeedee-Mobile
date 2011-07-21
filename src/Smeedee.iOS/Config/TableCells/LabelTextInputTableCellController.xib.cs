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
        
        public void BindDataToCell(string text)
        {
			this.label.Text = text;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
        }
		
		public void BindTextChangedAction(Action<string> callback) {
			
			// TODO: Make this work somehow
			textInput.EditingDidEnd += delegate {
				callback(textInput.Text);
				
				
			};
		}
	}
}

