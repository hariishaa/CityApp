using CityApp.Helpers;
using CityApp.Models.Transport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.ViewModels
{
    public class TransportDetailViewModel
    {
        string routeNum;
        int routeId;

        public List<Timetable> Timetables { get; private set; }
        public string EmptyListText { get; private set; }

        public int CurrentDayId
        {
            get
            {
                return (DateTime.Now.DayOfWeek == 0) ? 6 : (int)DateTime.Now.DayOfWeek - 1;
            }
        }

        public TransportDetailViewModel(string routeNum, int routeId)
        {
            this.routeNum = routeNum;
            this.routeId = routeId;
        }

        public async Task LoadTimetables(int dayId)
        {
            var method = "transport/get_timetable_daily";
            var parameters = new Dictionary<string, string>
            {
                ["route_num"] = routeNum,
                ["route_id"] = routeId.ToString(),
                ["day_id"] = dayId.ToString()
            };
            try
            {
                Timetables = await WebService.MakeApiRequest<List<Timetable>>(method, parameters);
                EmptyListText = Properties.Strings.EmptyListString;
            }
            catch (Exception ex)
            {
                Timetables = null;
                EmptyListText = Properties.Strings.ExceptionString + ex.Message;
            }
        }
    }
}
