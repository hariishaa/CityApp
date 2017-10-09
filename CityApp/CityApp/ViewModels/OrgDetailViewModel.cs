using CityApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.ViewModels
{
    public class OrgDetailViewModel
    {
        public byte[] BytesStaticMap { get; private set; }

        public async Task LoadStaticMap(string lon, string lat)
        {
            if (string.IsNullOrEmpty(lon) || string.IsNullOrEmpty(lat))
            {
                BytesStaticMap = null;
                return;
            }
            var url = "https://static-maps.yandex.ru/1.x/";
            //l=map&ll={lon},{lat}&z={zoom}&pt={lon},{lat},org&size=650,450";
            var parameters = new Dictionary<string, string>
            {
                ["l"] = "map",
                ["ll"] = $"{lon},{lat}",
                ["zoom"] = "17",
                ["pt"] = $"{lon},{lat},org",
                ["size"] = "610,450"
            };
            try
            {
                BytesStaticMap = await WebService.MakeUrlBytesRequest(url, parameters);
            }
            catch (Exception)
            {
                BytesStaticMap = null;
            }

            //try
            //{
            //    
            //    if (Organizations.Count == 0)
            //    {
            //        EmptyListText = Properties.Strings.EmptyListString;
            //    }
            //    else
            //    {
            //        EmptyListText = string.Empty;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Organizations = null;
            //    EmptyListText = Properties.Strings.ExceptionString + ex.Message;
            //}
        }
    }
}
