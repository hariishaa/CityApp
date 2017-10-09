using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CityApp.ViewModels;
using CityApp.Droid.Adapters;
using CityApp.Droid.Activities;

namespace CityApp.Droid.Fragments
{
    public class OrgMasterFragment : Fragment
    {
        OrgMasterViewModel vm = new OrgMasterViewModel();
        Context context;

        ProgressBar progressBar;
        TextView emptyTextView;
        ExpandableListView orgCategoriesExpListView;

        public override void OnAttach(Context context)
        {
            base.OnAttach(context);

            this.context = context;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.activity_org_master, container, false);
            progressBar = view.FindViewById<ProgressBar>(Resource.Id.progressBar);
            emptyTextView = view.FindViewById<TextView>(Resource.Id.emptyTextView);
            orgCategoriesExpListView = view.FindViewById<ExpandableListView>(Resource.Id.orgCategoriesExpListView);
            return view;
        }

        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            progressBar.Visibility = ViewStates.Visible;
            await vm.LoadCategories();
            emptyTextView.Text = vm.EmptyListText;
            if (vm.Categories != null)
            {
                orgCategoriesExpListView.SetAdapter(new OrgCategoriesExpListViewAdapter(context, vm.Categories, vm.Subcategories));
                orgCategoriesExpListView.GroupClick += OrgCategoriesExpListView_GroupClick;
                orgCategoriesExpListView.ChildClick += OrgCategoriesExpListView_ChildClick;
            }
            progressBar.Visibility = ViewStates.Gone;
        }

        private void OrgCategoriesExpListView_ChildClick(object sender, ExpandableListView.ChildClickEventArgs e)
        {
            var intent = new Intent(context, typeof(OrgListActivity));
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
                var intent = new Intent(context, typeof(OrgListActivity));
                intent.PutExtra("cat_id", vm.Categories[e.GroupPosition].id);
                intent.PutExtra("title", vm.Categories[e.GroupPosition].name);
                StartActivity(intent);
            }
        }
    }
}