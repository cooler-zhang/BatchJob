using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Common
{
    public class EnumHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            if (fi == null)
            {
                return string.Empty;
            }
            else
            {
                DescriptionAttribute[] attributes =
                  (DescriptionAttribute[])fi.GetCustomAttributes(
                  typeof(DescriptionAttribute), false);
                return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
            }
        }

        public static string GetEnumDescription(Type value, string name)
        {
            FieldInfo fi = value.GetField(name);
            DescriptionAttribute[] attributes =
              (DescriptionAttribute[])fi.GetCustomAttributes(
              typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : name;
        }

        public static IList<EnumObject> ConvertEnumToList<T>()
        {
            return ConvertEnumToList(typeof(T));
        }

        public static IList<EnumObject> ConvertEnumToList(Type type)
        {
            IList<EnumObject> statusListItemList = new List<EnumObject>();
            var statusArr = Enum.GetValues(type);
            foreach (var status in statusArr)
            {
                statusListItemList.Add(new EnumObject((int)status, GetEnumDescription(status as Enum)));
            }
            return statusListItemList;
        }
    }

    public class EnumObject
    {
        public int? EnumId { get; set; }
        public string EnumDescription { get; set; }
        public EnumObject(int? enumId, string enumDescription)
        {
            this.EnumId = enumId;
            this.EnumDescription = enumDescription;
        }
    }
}
