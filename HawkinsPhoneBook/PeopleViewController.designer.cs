// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace HawkinsPhoneBook
{
    [Register ("PeopleViewController")]
    partial class PeopleViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView peopleTable { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (peopleTable != null) {
                peopleTable.Dispose ();
                peopleTable = null;
            }
        }
    }
}