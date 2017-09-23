using CityApp.Helpers;
using CityApp.Models.Org;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.ViewModels
{
    public class OrgListViewModel
    {
        public List<OrgCard> Organizations { get; private set; }
        public string EmptyListText { get; private set; }

        public async Task LoadOrganizations(int catId, int subcatId = 0)
        {
            var method = "org/get_orgs";
            var parameters = new Dictionary<string, string>
            {
                ["cat_id"] = catId.ToString()
            };
            if (subcatId > 0)
            {
                parameters["subcat_id"] = subcatId.ToString();
            }
            try
            {
                Organizations = await WebService.MakeApiRequest<List<OrgCard>>(method, parameters);
                if (Organizations.Count == 0)
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
                Organizations = null;
                EmptyListText = Properties.Strings.ExceptionString + ex.Message;
            }
        }
    }
}
