using System;

using UIKit;
using System.Collections.Generic;
using System.Linq;

namespace HawkinsPhoneBook
{
	public partial class PeopleViewController : UITableViewController
	{
		public Dictionary<string, string[]> Data;

		protected PeopleViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Data = new Dictionary<string, string[]>();
			Data["Byers"] = new[] { "Joyce", "Jonathan", "Will" };
			Data["High School"] = new[] { "Barb", "Carol", "Steve", "Tommy" };
			Data["Wheeler"] = new[] { "Holly", "Karen", "Mike", "Nancy", "Ted" };
			Data["Police"] = new[] { "Callahan", "Florence", "Hopper", "Powell" };

			peopleTable.Source = new PeopleSource(this);

		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}

	class PeopleSource : UITableViewSource
	{
		WeakReference<PeopleViewController> wr_pvc;
		Dictionary<string, string[]> data;

		public PeopleSource(PeopleViewController peopleViewController)
		{
			wr_pvc = new WeakReference<PeopleViewController>(peopleViewController);
			peopleViewController = null;

			PeopleViewController pvc;
			if (wr_pvc.TryGetTarget(out pvc))
			{
				data = pvc.Data;
			}
				
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = new UITableViewCell(UITableViewCellStyle.Default, indexPath.ToString());

			string[] people = null;

			if (data.TryGetValue(data.Keys.ElementAt((int)indexPath.Section), out people))
			{
				var item = people[indexPath.Row];
				cell.TextLabel.Text = item ?? "";
			}
			else
			{
				cell.TextLabel.Text = "";
			}

			return cell;
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return data.Count;
		}

		public override string TitleForHeader(UITableView tableView, nint section)
		{
			return data.Keys.ElementAt((int)section);
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			string[] people = null;
			if (data.TryGetValue(data.Keys.ElementAt((int)section), out people))
			{
				return people.Length;
			}

			return 0;
		}
	}
}

