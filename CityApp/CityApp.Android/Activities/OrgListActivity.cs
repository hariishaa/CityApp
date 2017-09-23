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
using CityApp.ViewModels;
using CityApp.Droid.Adapters;

namespace CityApp.Droid.Activities
{
    [Activity(Label = "OrgListActivity")]
    public class OrgListActivity : ActivityBase
    {
        OrgListViewModel vm = new OrgListViewModel();

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var catId = Intent.GetIntExtra("cat_id", 0);
            var subcatId = Intent.GetIntExtra("subcat_id", 0);
            var title = Intent.GetStringExtra("title");

            SetLayout(Resource.Layout.activity_org_list, title, true);

            var progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            progressBar.Visibility = ViewStates.Visible;
            await vm.LoadOrganizations(catId, subcatId);
            var emptyTextView = FindViewById<TextView>(Resource.Id.emptyTextView);
            emptyTextView.Text = vm.EmptyListText;
            if (vm.Organizations != null)
            {
                var listView = FindViewById<ListView>(Resource.Id.orgListView);
                listView.Adapter = new OrgListViewAdapter(this,
                    Resource.Layout.list_item_org_list, vm.Organizations);
                //listView.ItemClick += (s, e) =>
                //{
                //    var route = vm.AllRoutes[e.Position];
                //    var intent = new Intent(this, typeof(TransportDetailActivity));
                //    intent.PutExtra("routeNum", route.route_num);
                //    intent.PutExtra("routeId", route.route_id);
                //    intent.PutExtra("cityFromName", route.city_from_name);
                //    intent.PutExtra("stationFromName", route.station_from_name);
                //    intent.PutExtra("cityToName", route.city_to_name);
                //    intent.PutExtra("stationToName", route.station_to_name);
                //    StartActivity(intent);
                //};
            }
            progressBar.Visibility = ViewStates.Gone;
        }
    }
}