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
using CityApp.Models;
using CityApp.DroId.Adapters;
using CityApp.ViewModels;
using CityApp.Models.Transport;

namespace CityApp.Droid.Fragments
{
    public class TransportTimetablesListFragment : ListFragment
    {
        TransportDetailViewModel vm;
        List<Timetable> timetables;

        public TransportTimetablesListFragment(TransportDetailViewModel vm)
        {
            this.vm = vm;
        }

        // todo: загуглить как выводить сообщение об ошибки в listfragment
        public async void SetTimetable(int dayId)
        {
            if (timetables == null)
            {
                await vm.LoadTimetables(dayId);
                timetables = vm.Timetables;
                SetEmptyText(vm.EmptyListText);
                if (timetables != null)
                {
                    ListAdapter = new TransportTimetablesListViewAdapter(Activity,
                        Resource.Layout.list_item_transport_timetables, timetables);
                }
                else
                {
                    ListAdapter = null;
                }
            }
        }
    }
}