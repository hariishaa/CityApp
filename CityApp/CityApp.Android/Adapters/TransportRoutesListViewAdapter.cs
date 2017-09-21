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
using CityApp.Models.Transport;

namespace CityApp.Droid.Adapters
{
    public class TransportRoutesListViewAdapter : ArrayAdapter<Route>
    {
        private LayoutInflater inflater;
        private int layout;
        private List<Route> routes;

        public TransportRoutesListViewAdapter(Context context, int resource, List<Route> routes) : 
            base(context, resource, routes)
        {
            this.routes = routes;
            this.layout = resource;
            this.inflater = LayoutInflater.From(context);
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Route route = routes.ElementAt(position);
            ViewHolder viewHolder;
            if (convertView == null)
            {
                convertView = inflater.Inflate(layout, parent, false);
                viewHolder = new ViewHolder(convertView);
                convertView.Tag = viewHolder;
            }
            else
            {
                viewHolder = (ViewHolder)convertView.Tag;
            }
            viewHolder.RouteNumTextView.Text = route.route_num;
            viewHolder.FromCityTextView.Text = route.city_from_name;
            viewHolder.FromStationTextView.Text = "(" + route.station_from_name + ")";
            viewHolder.ToCityTextView.Text = route.city_to_name;
            viewHolder.ToStationTextView.Text = "(" + route.station_to_name + ")";
            //string remark = route.remark;
            //if (remark != null && remark != string.Empty)
            //{
            //    viewHolder.RemarkTextView.Text = remark;
            //}
            //else
            //{
            //    viewHolder.RemarkTextView.Visibility = ViewStates.Gone;
            //}
            return convertView;
        }
    }

    public class ViewHolder : Java.Lang.Object
    {
        public TextView RouteNumTextView { get; private set; }
        public TextView FromCityTextView { get; private set; }
        public TextView FromStationTextView { get; private set; }
        public TextView ToCityTextView { get; private set; }
        public TextView ToStationTextView { get; private set; }
        //public TextView RemarkTextView { get; private set; }

        public ViewHolder(View view)
        {
            RouteNumTextView = view.FindViewById<TextView>(Resource.Id.routeNumTextView);
            FromCityTextView = view.FindViewById<TextView>(Resource.Id.fromCityTextView);
            FromStationTextView = view.FindViewById<TextView>(Resource.Id.fromStationTextView);
            ToCityTextView = view.FindViewById<TextView>(Resource.Id.toCityTextView);
            ToStationTextView = view.FindViewById<TextView>(Resource.Id.toStationTextView);
            //RemarkTextView = view.FindViewById<TextView>(Resource.Id.remarkTextView);
        }
    }
}