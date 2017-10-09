using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using CityApp.Droid.Fragments;

namespace CityApp.Droid.Activities
{
	[Activity (MainLauncher = true, Icon = "@drawable/app_icon")]
    public class MainActivity : ActivityBase
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetLayout(Resource.Layout.activity_main, Resource.String.app_name, false);

            //Button transportButton = FindViewById<Button>(Resource.Id.transportButton);
            //transportButton.Click += delegate
            //{
            //    var intent = new Intent(this, typeof(TransportMasterActivity));
            //    StartActivity(intent);
            //};
            //Button orgButton = FindViewById<Button>(Resource.Id.orgButton);
            //orgButton.Click += delegate
            //{
            //    var intent = new Intent(this, typeof(OrgMasterActivity));
            //    StartActivity(intent);
            //};
            //Button televisionButton = FindViewById<Button>(Resource.Id.televisionButton);
            //televisionButton.Click += delegate
            //{
            //    var intent = new Intent(this, typeof(TelevisionActivity));
            //    StartActivity(intent);
            //};
            var botNavBar = FindViewById<BottomNavigationView>(Resource.Id.bot_nav_bar);
            botNavBar.NavigationItemSelected += (s, e) => LoadFragment(e.Item.ItemId);
            LoadFragment(Resource.Id.menu_item_transport);
        }

        void LoadFragment(int id)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.menu_item_transport:
                    fragment = new TransportMasterFragment();
                    break;
                case Resource.Id.menu_item_orgs:
                    fragment = new OrgMasterFragment();
                    break;
                case Resource.Id.menu_item_news:
                    fragment = new TelevisionFragment();
                    break;
            }

            if (fragment == null)
                return;

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.fragmentFrameLayout, fragment)
                .Commit();
        }
    }
}


