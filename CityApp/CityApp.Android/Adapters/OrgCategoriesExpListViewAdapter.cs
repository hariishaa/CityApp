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
using CityApp.Models.Org;

namespace CityApp.Droid.Adapters
{
    public class OrgCategoriesExpListViewAdapter : BaseExpandableListAdapter
    {
        private Context context;
        private List<string> listGroup;
        private Dictionary<string, List<string>> listChild;

        public OrgCategoriesExpListViewAdapter(Context context, List<Category> listGroup,
            Dictionary<Category, List<Subcategory>> listChild)
        {
            this.context = context;
            this.listGroup = new List<string>();
            foreach (var cat in listGroup)
            {
                this.listGroup.Add(cat.name);
            }
            this.listChild = new Dictionary<string, List<string>>();
            foreach (var cat in listChild)
            {
                var subs = new List<string>();
                foreach (var sub in cat.Value)
                {
                    subs.Add(sub.name);
                }
                this.listChild.Add(cat.Key.name, subs);
            }
        }

        public override int GroupCount
        {
            get
            {
                return listGroup.Count;
            }
        }

        public override bool HasStableIds
        {
            get
            {
                return false;
            }
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            var result = new List<string>();
            listChild.TryGetValue(listGroup[groupPosition], out result);
            return result[childPosition];
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            var result = new List<string>();
            listChild.TryGetValue(listGroup[groupPosition], out result);
            return result.Count;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(Resource.Layout.list_item_org_categories, null);
            }
            TextView subcategoryTextView = convertView.FindViewById<TextView>(Resource.Id.subcategoryTextView);
            string content = (string)GetChild(groupPosition, childPosition);
            subcategoryTextView.Text = content;
            return convertView;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return listGroup[groupPosition];
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(Resource.Layout.list_group_org_categories, null);
            }
            string textGroup = (string)GetGroup(groupPosition);
            TextView categoryTextView = convertView.FindViewById<TextView>(Resource.Id.categoryTextView);
            categoryTextView.Text = textGroup;
            ImageView indicatorImageView = convertView.FindViewById<ImageView>(Resource.Id.indicatorImageView);
            if (GetChildrenCount(groupPosition) == 0)
            {
                indicatorImageView.Visibility = ViewStates.Gone;
            }
            else
            {
                indicatorImageView.Visibility = ViewStates.Visible;
            }
            return convertView;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }
}