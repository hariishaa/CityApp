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
    public class TransportMasterFragment : Fragment
    {
        TransportMasterViewModel vm = new TransportMasterViewModel();
        Context context;

        ProgressBar progressBar;
        TextView emptyTextView;
        ListView routesListView;

        public override void OnAttach(Context context)
        {
            base.OnAttach(context);

            this.context = context;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.activity_transport_master, container, false);
            progressBar = view.FindViewById<ProgressBar>(Resource.Id.progressBar);
            emptyTextView = view.FindViewById<TextView>(Resource.Id.emptyTextView);
            routesListView = view.FindViewById<ListView>(Resource.Id.routesListView);
            return view;
        }

        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            progressBar.Visibility = ViewStates.Visible;
            await vm.LoadRoutes();
            emptyTextView.Text = vm.EmptyListText;
            if (vm.AllRoutes != null)
            {
                routesListView.Adapter = new TransportRoutesListViewAdapter(context,
                    Resource.Layout.list_item_transport_routes, vm.AllRoutes);
                routesListView.ItemClick += (s, e) =>
                {
                    var route = vm.AllRoutes[e.Position];
                    var intent = new Intent(context, typeof(TransportDetailActivity));
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