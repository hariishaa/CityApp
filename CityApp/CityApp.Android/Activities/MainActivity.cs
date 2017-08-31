using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace CityApp.Droid.Activities
{
	[Activity (Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.main);

            //Button transportButton = FindViewById<Button>(Resource.Id.transportButton);
            //transportButton.Click += delegate {
            //    var intent = new Intent(this, typeof(TransportMasterActivity));
            //    StartActivity(intent);
            //};

            //Button taxiButton = FindViewById<Button>(Resource.Id.taxiButton);
            //taxiButton.Click += delegate {
            //    var intent = new Intent(this, typeof(TaxiActivity));
            //    StartActivity(intent);
            //};

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
        }
    }
}


