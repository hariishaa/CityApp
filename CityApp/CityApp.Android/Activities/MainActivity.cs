using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace CityApp.Droid.Activities
{
	[Activity (MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ActivityBase
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetLayout(Resource.Layout.activity_main, Resource.String.app_name, false);

            Button transportButton = FindViewById<Button>(Resource.Id.transportButton);
            transportButton.Click += delegate
            {
                var intent = new Intent(this, typeof(TransportMasterActivity));
                StartActivity(intent);
            };
            Button orgButton = FindViewById<Button>(Resource.Id.orgButton);
            orgButton.Click += delegate
            {
                var intent = new Intent(this, typeof(OrgMasterActivity));
                StartActivity(intent);
            };
            Button televisionButton = FindViewById<Button>(Resource.Id.televisionButton);
            televisionButton.Click += delegate
            {
                var intent = new Intent(this, typeof(TelevisionActivity));
                StartActivity(intent);
            };
            //Button taxiButton = FindViewById<Button>(Resource.Id.taxiButton);
            //taxiButton.Click += delegate {
            //    var intent = new Intent(this, typeof(TaxiActivity));
            //    StartActivity(intent);
            //};
        }
    }
}


