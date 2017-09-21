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
using Java.Lang;

namespace CityApp.Droid.Adapters
{
    public class TransportDetailViewPagerAdapter : FragmentStatePagerAdapter
    {
        private List<Fragment> mFragmentList = new List<Fragment>();
        private List<string> mFragmentTitleList = new List<string>();

        public TransportDetailViewPagerAdapter(FragmentManager fm) : base(fm)
        {

        }

        public override int Count
        {
            get
            {
                return mFragmentList.Count;
            }
        }

        public override Fragment GetItem(int position)
        {
            return mFragmentList.ElementAt(position);
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(mFragmentTitleList.ElementAt(position));
        }

        public void AddFragment(Fragment fragment, string title)
        {
            mFragmentList.Add(fragment);
            mFragmentTitleList.Add(title);
        }
    }
}