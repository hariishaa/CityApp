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
using CityApp.Droid;
using CityApp.Models.Transport;

namespace CityApp.DroId.Adapters
{
    public class TransportTimetablesListViewAdapter : ArrayAdapter<Timetable>
    {
        private LayoutInflater inflater;
        private int layout;
        private List<Timetable> timetables;

        public TransportTimetablesListViewAdapter(Context context, int resource, List<Timetable> timetables) : 
            base(context, resource, timetables)
        {
            this.timetables = timetables;
            this.layout = resource;
            this.inflater = LayoutInflater.From(context);
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Timetable timetable = timetables.ElementAt(position);
            TransportTimetablesViewHolder viewHolder;
            if (convertView == null)
            {
                convertView = inflater.Inflate(layout, parent, false);
                viewHolder = new TransportTimetablesViewHolder(convertView);
                convertView.Tag = viewHolder;
            }
            else
            {
                viewHolder = (TransportTimetablesViewHolder)convertView.Tag;
            }
            viewHolder.TimeTextView.Text = timetable.hour + ":" + timetable.minutes;
            viewHolder.FromCityTextView.Text = timetable.city_from_name;
            viewHolder.FromStationTextView.Text = "(" + timetable.station_from_name + ")";
            viewHolder.ToCityTextView.Text = timetable.city_to_name;
            viewHolder.ToStationTextView.Text = "(" + timetable.station_to_name + ")";
            string remark = timetable.remark;
            if (remark != null && remark != string.Empty)
            {
                viewHolder.RemarkTextView.Text = remark;
                viewHolder.RemarkTextView.Visibility = ViewStates.Visible;
            }
            else
            {
                viewHolder.RemarkTextView.Visibility = ViewStates.Gone;
            }
            return convertView;
        }
    }

    public class TransportTimetablesViewHolder : Java.Lang.Object
    {
        public TextView TimeTextView { get; private set; }
        public TextView FromCityTextView { get; private set; }
        public TextView FromStationTextView { get; private set; }
        public TextView ToCityTextView { get; private set; }
        public TextView ToStationTextView { get; private set; }
        public TextView RemarkTextView { get; private set; }

        public TransportTimetablesViewHolder(View view)
        {
            TimeTextView = view.FindViewById<TextView>(Resource.Id.timeTextView);
            FromCityTextView = view.FindViewById<TextView>(Resource.Id.fromCityTextView);
            FromStationTextView = view.FindViewById<TextView>(Resource.Id.fromStationTextView);
            ToCityTextView = view.FindViewById<TextView>(Resource.Id.toCityTextView);
            ToStationTextView = view.FindViewById<TextView>(Resource.Id.toStationTextView);
            RemarkTextView = view.FindViewById<TextView>(Resource.Id.remarkTextView);
        }
    }
}