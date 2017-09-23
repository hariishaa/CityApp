using CityApp.Helpers;
using CityApp.Models.Org;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.ViewModels
{
    public class OrgMasterViewModel
    {
        public List<Category> Categories { get; private set; }
        public Dictionary<Category, List<Subcategory>> Subcategories { get; private set; }
        public string EmptyListText { get; private set; }

        public async Task LoadCategories()
        {
            var method = "org/get_cats";
            try
            {
                Categories = await WebService.MakeApiRequest<List<Category>>(method);
                SetSubcategories();
                if (Categories.Count == 0)
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
                Categories = null;
                Subcategories = null;
                EmptyListText = Properties.Strings.ExceptionString + ex.Message;
            }
        }

        void SetSubcategories()
        {
            Subcategories = new Dictionary<Category, List<Subcategory>>();
            if (Categories == null)
            {
                return;
            }
            foreach (var cat in Categories)
            {
                Subcategories.Add(cat, cat.subcategories ?? new List<Subcategory>());
            }
        }
    }
}
