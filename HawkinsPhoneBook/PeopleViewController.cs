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

#if DO_CYCLES
			// This adds a cycle, as the *managed* PeopleViewController will have an [obj retain]
			// on the *unmanaged* PeopleViewController, which will be holding on to the *unmanaged*
			// peopleSource, which in turn will have a GCHandle to the *managed* 
			// peopleSource, which references the *managed* PeopleViewController.
			peopleTable.Source = new PeopleSource(this);

#else
			// this can be fixed by not referencing the controller from the Source:
			peopleTable.Source = new PeopleSource (Data);
#endif
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}

	class PeopleSource : UITableViewSource
	{
#if DO_CYCLES
		readonly PeopleViewController peopleViewController;

		public PeopleSource(PeopleViewController peopleViewController)
		{
			this.peopleViewController = peopleViewController;
		}
#else
		readonly Dictionary<string, string []> data;
		public PeopleSource (Dictionary<string, string[]> data)
		{
			this.data = data;
		}
#endif

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = new UITableViewCell(UITableViewCellStyle.Default, indexPath.ToString());

			string[] people = null;
#if DO_CYCLES
			if (peopleViewController.Data.TryGetValue(peopleViewController.Data.Keys.ElementAt((int)indexPath.Section), out people))
			{
#else
			if (data.TryGetValue (data.Keys.ElementAt ((int) indexPath.Section), out people)) {
#endif
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
#if DO_CYCLES
			return peopleViewController.Data.Count;
#else
			return data.Count;
#endif
		}

		public override string TitleForHeader(UITableView tableView, nint section)
		{
#if DO_CYCLES
			return peopleViewController.Data.Keys.ElementAt((int)section);
#else
			return data.Keys.ElementAt ((int) section);
#endif
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			string[] people = null;
#if DO_CYCLES
			if (peopleViewController.Data.TryGetValue(peopleViewController.Data.Keys.ElementAt((int)section), out people))
			{
#else
			if (data.TryGetValue (data.Keys.ElementAt ((int) section), out people)) {
#endif
				return people.Length;
			}

			return 0;
		}
	}
}

