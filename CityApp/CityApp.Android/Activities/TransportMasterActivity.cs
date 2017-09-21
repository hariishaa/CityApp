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
    [Activity]
    public class TransportMasterActivity : ActivityBase
    {
        TransportMasterViewModel vm = new TransportMasterViewModel();

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetLayout(Resource.Layout.activity_transport_master, Resource.String.title_transport_master);

            var progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            progressBar.Visibility = ViewStates.Visible;
            await vm.LoadRoutes();
            var emptyTextView = FindViewById<TextView>(Resource.Id.emptyTextView);
            emptyTextView.Text = vm.EmptyListText;
            if (vm.AllRoutes != null)
            {
                var listView = FindViewById<ListView>(Resource.Id.routesListView);
                listView.Adapter = new TransportRoutesListViewAdapter(this,
                    Resource.Layout.list_item_transport_routes, vm.AllRoutes);
                listView.ItemClick += (s, e) =>
                {
                    var route = vm.AllRoutes[e.Position];
                    var intent = new Intent(this, typeof(TransportDetailActivity));
                    intent.PutExtra("routeNum", route.route_num);
                    intent.PutExtra("routeId", route.route_id);
                    intent.PutExtra("cityFromName", route.city_from_name);
                    intent.PutExtra("stationFromName", route.station_from_name);
                    intent.PutExtra("cityToName", route.city_to_name);
                    intent.PutExtra("stationToName", route.station_to_name);
                    StartActivity(intent);
                };
            }
            progressBar.Visibility = ViewStates.Gone;
        }
    }
}