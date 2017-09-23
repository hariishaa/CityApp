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
using CityApp.Models.Org;

namespace CityApp.Droid.Adapters
{
    public class OrgListViewAdapter :ArrayAdapter<OrgCard>
    {
        private LayoutInflater inflater;
        private int layout;
        private List<OrgCard> orgs;

        public OrgListViewAdapter(Context context, int resource, List<OrgCard> orgs) :
            base(context, resource, orgs)
        {
            this.orgs = orgs;
            this.layout = resource;
            this.inflater = LayoutInflater.From(context);
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            OrgCard org = orgs.ElementAt(position);
            OrgListViewHolder viewHolder;
            if (convertView == null)
            {
                convertView = inflater.Inflate(layout, parent, false);
                viewHolder = new OrgListViewHolder(convertView);
                convertView.Tag = viewHolder;
            }
            else
            {
                viewHolder = (OrgListViewHolder)convertView.Tag;
            }
            viewHolder.OrgNameTextView.Text = org.name;
            viewHolder.OrgAddressTextView.Text = $"{org.city}, {org.street}, {org.building_num}";
            return convertView;
        }
    }

    public class OrgListViewHolder : Java.Lang.Object
    {
        public TextView OrgNameTextView { get; private set; }
        public TextView OrgAddressTextView { get; private set; }

        public OrgListViewHolder(View view)
        {
            OrgNameTextView = view.FindViewById<TextView>(Resource.Id.orgNameTextView);
            OrgAddressTextView = view.FindViewById<TextView>(Resource.Id.orgAddressTextView);
        }
    }
}