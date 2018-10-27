using BatchJob.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BatchJob
{
    public static class DataConverter
    {
        public static IList<SelectListItem> ToSelectList(this IList<JobGroupDto> dtos)
        {
            var selectList = new List<SelectListItem>();
            if (dtos != null)
            {
                foreach (var dto in dtos)
                {
                    selectList.Add(new SelectListItem()
                    {
                        Text = dto.Name,
                        Value = dto.Id.Value.ToString()
                    });
                }
            }
            return selectList;
        }

        public static IList<SelectListItem> ToSelectList(this IList<JobDto> dtos)
        {
            var selectList = new List<SelectListItem>();
            if (dtos != null)
            {
                var groups = dtos.GroupBy(a => new { a.GroupId, a.GroupName }).ToDictionary(a => a.Key, a => a.ToList());
                foreach (var group in groups)
                {
                    var selectListGroup = new SelectListGroup();
                    selectListGroup.Name = group.Key.GroupName;
                    foreach (var job in group.Value)
                    {
                        selectList.Add(new SelectListItem()
                        {
                            Text = job.Name,
                            Value = job.Id.Value.ToString(),
                            Group = selectListGroup
                        });
                    }
                }
            }
            return selectList;
        }
    }
}