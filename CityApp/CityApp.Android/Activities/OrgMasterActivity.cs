using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CityApp.Droid.Adapters;
using CityApp.ViewModels;
using Android.Support.Design.Widget;
using CityApp.Droid.Fragments;

namespace CityApp.Droid.Activities
{
    [Activity(Label = "OrgMasterActivity")]
    public class OrgMasterActivity : ActivityBase
    {
        OrgMasterViewModel vm = new OrgMasterViewModel();
        ExpandableListView orgCategoriesExpListView;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetLayout(Resource.Layout.activity_org_master, Resource.String.title_org_master, true);

            var progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            progressBar.Visibility = ViewStates.Visible;
            await vm.LoadCategories();
            var emptyTextView = FindViewById<TextView>(Resource.Id.emptyTextView);
            emptyTextView.Text = vm.EmptyListText;
            if (vm.Categories != null)
            {
                orgCategoriesExpListView = FindViewById<ExpandableListView>(Resource.Id.orgCategoriesExpListView);
                orgCategoriesExpListView.SetAdapter(new OrgCategoriesExpListViewAdapter(this, vm.Categories, vm.Subcategories));
                orgCategoriesExpListView.GroupClick += OrgCategoriesExpListView_GroupClick;
                orgCategoriesExpListView.ChildClick += OrgCategoriesExpListView_ChildClick;
            }
            progressBar.Visibility = ViewStates.Gone;
        }

        private void OrgCategoriesExpListView_ChildClick(object sender, ExpandableListView.ChildClickEventArgs e)
        {
            var intent = new Intent(this, typeof(OrgListActivity));
            intent.PutExtra("cat_id", vm.Categories[e.GroupPosition].id);
            intent.PutExtra("subcat_id", vm.Categories[e.GroupPosition].subcategories[e.ChildPosition].id);
            intent.PutExtra("title", vm.Categories[e.GroupPosition].subcategories[e.ChildPosition].name);
            StartActivity(intent);
        }

        private void OrgCategoriesExpListView_GroupClick(object sender, ExpandableListView.GroupClickEventArgs e)
        {
            if (vm.Categories[e.GroupPosition].subcategories != null && vm.Categories[e.GroupPosition].subcategories.Count != 0)
            {
                if (!orgCategoriesExpListView.IsGroupExpanded(e.GroupPosition))
                {
                    orgCategoriesExpListView.ExpandGroup(e.GroupPosition);
                }
                else
                {
                    orgCategoriesExpListView.CollapseGroup(e.GroupPosition);
                }
            }
            else
            {
                var intent = new Intent(this, typeof(OrgListActivity));
                intent.PutExtra("cat_id", vm.Categories[e.GroupPosition].id);
                intent.PutExtra("title", vm.Categories[e.GroupPosition].name);
                StartActivity(intent);
            }
        }
    }
}