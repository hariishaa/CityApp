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
using System.Threading.Tasks;
using CityApp.ViewModels;
using Android.Graphics;

namespace CityApp.Droid.Activities
{
    [Activity(Label = "OrgDetailActivity")]
    public class OrgDetailActivity : ActivityBase
    {
        OrgDetailViewModel vm = new OrgDetailViewModel();

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var orgName = Intent.GetStringExtra("orgName");
            var orgAddress = Intent.GetStringExtra("orgAddress");
            var orgLon = Intent.GetStringExtra("orgLon");
            var orgLat = Intent.GetStringExtra("orgLat");
            var orgDesc = Intent.GetStringExtra("orgDesc");

            SetLayout(Resource.Layout.activity_org_detail, "", true); // change title
            var progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            progressBar.Visibility = ViewStates.Visible;
            await vm.LoadStaticMap(orgLon, orgLat);
            if (vm.BytesStaticMap != null && vm.BytesStaticMap.Length > 0)
            {
                var mapImageView = FindViewById<ImageView>(Resource.Id.mapImageView);
                var mapImage = await BitmapFactory.DecodeByteArrayAsync(vm.BytesStaticMap, 0, vm.BytesStaticMap.Length);
                float imageRatio = (float)mapImage.Width / (float)mapImage.Height;
                int imageViewWidth = mapImageView.Width;
                int imageRealHeight = (int)(imageViewWidth / imageRatio);
                Bitmap imageToShow = Bitmap.CreateScaledBitmap(mapImage, imageViewWidth, imageRealHeight, true);
                mapImageView.SetImageBitmap(imageToShow);
            }
            var orgTextView = FindViewById<TextView>(Resource.Id.orgNameTextView);
            var orgAddressTextView = FindViewById<TextView>(Resource.Id.orgAddressTextView);
            orgTextView.Text = orgName;
            orgAddressTextView.Text = orgAddress;
            if (!string.IsNullOrEmpty(orgDesc))
            {
                var orgDescriptionTextView = FindViewById<TextView>(Resource.Id.orgDescriptionTextView);
                var orgDescriptionTableRow = FindViewById<TableRow>(Resource.Id.orgDescriptionTableRow);
                orgDescriptionTextView.Text = orgDesc;
                orgDescriptionTableRow.Visibility = ViewStates.Visible;
            }
            var orgInfoTableLayout = FindViewById<TableLayout>(Resource.Id.orgInfoTableLayout);
            orgInfoTableLayout.Visibility = ViewStates.Visible;
            progressBar.Visibility = ViewStates.Gone;
        }
    }
}