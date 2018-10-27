using AutoMapper;
using BatchJob.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BatchJob.Models
{
    public class ServiceViewModel
    {
        public int? Id { get; set; }

        public int JobId { get; set; }

        [Display(Name = "作业")]
        public string JobName { get; set; }

        [Display(Name = "服务地址")]
        public string ServiceAddress { get; set; }

        [Display(Name = "契约方法"), Required]
        public string ContractMethod { get; set; }

        [Display(Name = "契约")]
        public string OperationContractName { get; set; }

        [Display(Name = "方法名称")]
        public string MethodName { get; set; }

        [Display(Name = "参数列表")]
        public string Parameters { get; set; }

        public static ServiceViewModel Create(ServiceDto dto)
        {
            return Mapper.Map<ServiceViewModel>(dto);
        }

        public ServiceDto ToDto()
        {
            return Mapper.Map<ServiceDto>(this);
        }
    }

    public class ServiceParameterViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "类型")]
        public string TypeName { get; set; }

        [Display(Name = "字段名称")]
        public string Name { get; set; }

        [Display(Name = "字段值")]
        public string Value { get; set; }

        public static ServiceParameterViewModel Create(ServiceDto.ServiceParameterDto dto)
        {
            return Mapper.Map<ServiceParameterViewModel>(dto);
        }

        public ServiceDto.ServiceParameterDto ToDto()
        {
            return Mapper.Map<ServiceDto.ServiceParameterDto>(this);
        }
    }

    public class ServiceInfo
    {
        public string ServiceAddress { get; set; }

        public List<ServiceContractInfo> ContractInfos { get; set; }
    }

    public class ServiceContractInfo
    {
        public string ContractName { get; set; }

        public List<ContractOperationInfo> OperationInfos { get; set; }
    }

    public class ContractOperationInfo
    {
        public string OperationName { get; set; }

        public List<OperationParameterInfo> ParameterInfos { get; set; }
    }

    public class OperationParameterInfo
    {
        public string ParameterName { get; set; }

        public string ParameterType { get; set; }
    }
}