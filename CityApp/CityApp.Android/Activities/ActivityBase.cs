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
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace CityApp.Droid.Activities
{
    public abstract class ActivityBase : AppCompatActivity
    {
        public void SetLayout(int layoutResId, int titleResId, bool backNavigationEnabled = true)
        {
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            SetContentView(layoutResId);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            if (toolbar != null)
            {
                SetSupportActionBar(toolbar);
                SupportActionBar.SetTitle(titleResId);
                SupportActionBar.SetDisplayHomeAsUpEnabled(backNavigationEnabled);
                SupportActionBar.SetHomeButtonEnabled(backNavigationEnabled);
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                Finish();

            return base.OnOptionsItemSelected(item);
        }
    }
}