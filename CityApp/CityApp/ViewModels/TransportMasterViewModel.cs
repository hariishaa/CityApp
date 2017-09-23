using CityApp.Helpers;
using CityApp.Models.Transport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.ViewModels
{
    public class TransportMasterViewModel
    {
        public List<Route> AllRoutes { get; private set; }
        public string EmptyListText { get; private set; }

        public async Task LoadRoutes()
        {
            var method = "transport/get_all_routes";
            try
            {
                AllRoutes = await WebService.MakeApiRequest<List<Route>>(method);
                if (AllRoutes.Count == 0)
                {
                    EmptyListText = Properties.Strings.EmptyListString;
                }
                else
                {
                    EmptyListText = string.Empty;
                }
            }
            catch (Exception ex)
            {
                AllRoutes = null;
                EmptyListText = Properties.Strings.ExceptionString + ex.Message;
            }
        }
    }
}
