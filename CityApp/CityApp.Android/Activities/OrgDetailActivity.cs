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
using Android.Support.CustomTabs;
using Android.Support.V4.Content;

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
            var orgPhone = Intent.GetStringExtra("orgPhone");
            var orgUrl = Intent.GetStringExtra("orgUrl");
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
            var orgAddressTableRow = FindViewById<TableRow>(Resource.Id.orgAddressTableRow);
            var orgAddressTextView = FindViewById<TextView>(Resource.Id.orgAddressTextView);
            orgTextView.Text = orgName;
            orgAddressTableRow.Click += (s, e) =>
            {
                var mapIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse($"geo:{orgLat},{orgLon}"));
                try
                {
                    StartActivity(mapIntent);
                }
                catch (ActivityNotFoundException)
                {
                    Toast.MakeText(this, "На устройстве не установлено приложение для отображения карт", ToastLength.Long)
                    .Show();
                }
            };
            orgAddressTextView.Text = orgAddress;
            if (!string.IsNullOrEmpty(orgPhone))
            {
                var orgPhoneTextView = FindViewById<TextView>(Resource.Id.orgPhoneTextView);
                var orgPhoneTableRow = FindViewById<TableRow>(Resource.Id.orgPhoneTableRow);
                orgPhoneTextView.Text = "+7 (" + orgPhone.Substring(1, 3) + ") " + orgPhone.Substring(4, 3) +
                    "-" + orgPhone.Substring(7, 2) + "-" + orgPhone.Substring(9, 2);
                orgPhoneTableRow.Click += (s, e) =>
                {
                    var dialerIntent = new Intent(Intent.ActionDial, Android.Net.Uri.Parse($"tel:+{orgPhone}"));
                    StartActivity(dialerIntent);
                };
                orgPhoneTableRow.Visibility = ViewStates.Visible;
            }
            if (!string.IsNullOrEmpty(orgUrl))
            {
                var orgUrlTextView = FindViewById<TextView>(Resource.Id.orgUrlTextView);
                var orgUrlTableRow = FindViewById<TableRow>(Resource.Id.orgUrlTableRow);
                orgUrlTextView.Text = orgUrl;
                orgUrlTableRow.Click += (s, e) =>
                {
                    var builder = new CustomTabsIntent.Builder()
                    .SetToolbarColor(ContextCompat.GetColor(this, Resource.Color.colorPrimary))
                    .SetShowTitle(true);
                    var intent = builder.Build();
                    var mgr = new CustomTabsActivityManager(this);
                    mgr.CustomTabsServiceConnected += delegate {
                        mgr.LaunchUrl(orgUrl, intent);
                    };
                    mgr.BindService();
                    if (!mgr.BindService())
                    {
                        var webIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(orgUrl));
                        StartActivity(webIntent);
                    }
                };
                orgUrlTableRow.Visibility = ViewStates.Visible;
            }
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