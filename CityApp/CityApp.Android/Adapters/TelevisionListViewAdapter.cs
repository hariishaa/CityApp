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
using CityApp.Models.Television;
using Java.Net;
using Android.Graphics;
using Square.Picasso;

namespace CityApp.Droid.Adapters
{
    public class TelevisionListViewAdapter : ArrayAdapter<YoutubeItem>
    {
        private LayoutInflater inflater;
        private int layout;
        private List<YoutubeItem> items;

        public TelevisionListViewAdapter(Context context, int resource, List<YoutubeItem> items) :
            base(context, resource, items)
        {
            this.items = items;
            this.layout = resource;
            this.inflater = LayoutInflater.From(context);
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            YoutubeItem item = items.ElementAt(position);
            TelevisionListViewHolder viewHolder;
            if (convertView == null)
            {
                convertView = inflater.Inflate(layout, parent, false);
                viewHolder = new TelevisionListViewHolder(convertView);
                convertView.Tag = viewHolder;
            }
            else
            {
                viewHolder = (TelevisionListViewHolder)convertView.Tag;
            }
            Picasso.With(Context).Load(item.DefaultThumbnailUrl).Into(viewHolder.VideoPreviewImageView);
            viewHolder.VideoTitleTextView.Text = item.Title;
            viewHolder.DateCaptionTextView.Text = item.PublishedAt.ToString("dd MMM yyyy");
            return convertView;
        }
    }

    public class TelevisionListViewHolder : Java.Lang.Object
    {
        public ImageView VideoPreviewImageView { get; private set; }
        public TextView VideoTitleTextView { get; private set; }
        public TextView DateCaptionTextView { get; private set; }

        public TelevisionListViewHolder(View view)
        {
            VideoPreviewImageView = view.FindViewById<ImageView>(Resource.Id.videoPreviewImageView);
            VideoTitleTextView = view.FindViewById<TextView>(Resource.Id.videoTitleTextView);
            DateCaptionTextView = view.FindViewById<TextView>(Resource.Id.dateCaptionTextView);
        }
    }
}