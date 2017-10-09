using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using CityApp.Droid.Adapters;
using CityApp.Droid.Fragments;
using Android.App;
using CityApp.ViewModels;
using System.Threading.Tasks;
using Java.Util;

namespace CityApp.Droid.Activities
{
    [Activity]
    public class TransportDetailActivity : ActivityBase
    {
        string routeNum;
        int routeId;
        TransportDetailViewModel vm;
        List<TransportTimetablesListFragment> fragments = new List<TransportTimetablesListFragment>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetLayout(Resource.Layout.activity_transport_detail, Resource.String.title_transport_detail);

            routeNum = Intent.GetStringExtra("routeNum");
            routeId = Intent.GetIntExtra("routeId", 0);
            vm = new TransportDetailViewModel(routeNum, routeId);

            FindViewById<TextView>(Resource.Id.routeNumTextView).Text = routeNum;
            FindViewById<TextView>(Resource.Id.fromCityTextView).Text = Intent.GetStringExtra("cityFromName");
            FindViewById<TextView>(Resource.Id.fromStationTextView).Text = 
                "(" + Intent.GetStringExtra("stationFromName") + ")";
            FindViewById<TextView>(Resource.Id.toCityTextView).Text = Intent.GetStringExtra("cityToName");
            FindViewById<TextView>(Resource.Id.toStationTextView).Text = 
                "(" + Intent.GetStringExtra("stationToName") + ")";
            var viewPager = FindViewById<ViewPager>(Resource.Id.viewPager);
            SetupViewPager(viewPager);
            var tabLayout = FindViewById<TabLayout>(Resource.Id.tabs);
            tabLayout.SetupWithViewPager(viewPager);
            tabLayout.TabSelected += TabLayout_TabSelected;
            // костыль
            if (vm.CurrentDayId != 0)
            {
                tabLayout.GetTabAt(vm.CurrentDayId).Select();
            }
            else
            {
                fragments[vm.CurrentDayId].SetTimetable(vm.CurrentDayId + 1);
            }
        }

        private void TabLayout_TabSelected(object sender, TabLayout.TabSelectedEventArgs e)
        {
            var fragment = fragments[e.Tab.Position];
            fragment.SetTimetable(e.Tab.Position + 1);
        }

        private void SetupViewPager(ViewPager viewPager)
        {
            TransportDetailViewPagerAdapter adapter = new TransportDetailViewPagerAdapter(SupportFragmentManager);
            var days = new string[] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС" };
            foreach (var day in days)
            {
                var fragment = new TransportTimetablesListFragment(vm);
                fragments.Add(fragment);
                adapter.AddFragment(fragment, day);
            }
            viewPager.Adapter = adapter;
        }
    }
}