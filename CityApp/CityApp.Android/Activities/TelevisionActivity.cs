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
    [Activity(Label = "TelevisionActivity")]
    public class TelevisionActivity : ActivityBase
    {
        TelevisionViewModel vm = new TelevisionViewModel();

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetLayout(Resource.Layout.activity_television, Resource.String.title_television, true);

            var progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            progressBar.Visibility = ViewStates.Visible;
            await vm.LoadYoutubeData();
            var emptyTextView = FindViewById<TextView>(Resource.Id.emptyTextView);
            emptyTextView.Text = vm.EmptyListText;
            if (vm.YoutubeItems != null)
            {
                var listView = FindViewById<ListView>(Resource.Id.televisionListView);
                listView.Adapter = new TelevisionListViewAdapter(this,
                    Resource.Layout.list_item_television_list, vm.YoutubeItems);
                listView.ItemClick += (s, e) =>
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