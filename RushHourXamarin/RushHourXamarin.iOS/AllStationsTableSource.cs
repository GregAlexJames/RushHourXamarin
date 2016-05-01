using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using RushHourXamarin.Portable;
using UIKit;

namespace RushHourXamarin.iOS
{

    public class AllStationsTableSource : UITableViewSource
    {

        // there is NO database or storage of Tasks in this example, just an in-memory List<>
        TrainStation[] tableItems;
        string cellIdentifier = "taskcell"; // set in the Storyboard

        public AllStationsTableSource(TrainStation[] items)
        {
            tableItems = items;
        }
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return tableItems.Length;
        }
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // in a Storyboard, Dequeue will ALWAYS return a cell,
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
            // now set the properties as normal
            cell.TextLabel.Text = tableItems[indexPath.Row].Name;
            //if (tableItems[indexPath.Row].Done)
            //    cell.Accessory = UITableViewCellAccessory.Checkmark;
            //else
            //    cell.Accessory = UITableViewCellAccessory.None;
            return cell;
        }

        public TrainStation GetItem(nint id)
        {
            return tableItems[id];
        }
    }
}
