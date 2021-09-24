using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Repository.Extensions.Utility
{
    public static class SearchService
    {
        public static int SearchByProperties<T>(string searchTerm, PropertyInfo[] propertyInfos, T inputClass)
        {
            int propertyCount = 0;
            foreach (var property in propertyInfos)
            {
                object value = property.GetValue(inputClass, null);
                if (value != null)
                {
                    Regex regex = new Regex("^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$");
                    Match match = regex.Match(value.ToString());
                    if (!regex.Match(value.ToString()).Success)
                    {
                        if (!value.ToString().ToLower().Contains(searchTerm.Trim().ToLower()))
                        {
                            propertyCount++;
                        }
                    }
                    else
                    {
                        propertyCount++;
                    }
                }
                else
                {
                    propertyCount++;
                }
            }

            return propertyCount;
        }
    }
}
