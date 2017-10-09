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

namespace CityApp.Droid.Fragments
{
    public class TelevisionFragment : Fragment
    {
        TelevisionViewModel vm = new TelevisionViewModel();
        Context context;

        ProgressBar progressBar;
        TextView emptyTextView;
        ListView televisionListView;

        public override void OnAttach(Context context)
        {
            base.OnAttach(context);

            this.context = context;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.activity_television, container, false);
            progressBar = view.FindViewById<ProgressBar>(Resource.Id.progressBar);
            emptyTextView = view.FindViewById<TextView>(Resource.Id.emptyTextView);
            televisionListView = view.FindViewById<ListView>(Resource.Id.televisionListView);
            return view;
        }

        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            progressBar.Visibility = ViewStates.Visible;
            await vm.LoadYoutubeData();
            emptyTextView.Text = vm.EmptyListText;
            if (vm.YoutubeItems != null)
            {
                televisionListView.Adapter = new TelevisionListViewAdapter(context,
                    Resource.Layout.list_item_television_list, vm.YoutubeItems);
                televisionListView.ItemClick += (s, e) =>
                {
                    var youtubeItem = vm.YoutubeItems[e.Position];
                    var uri = Android.Net.Uri.Parse("https://www.youtube.com/watch?v=" + youtubeItem?.VideoId);
                    var intent = new Intent(Intent.ActionView, uri);
                    StartActivity(intent);
                };
            }
            progressBar.Visibility = ViewStates.Gone;
        }
    }
}