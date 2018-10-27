using BatchJob.Dto;
using BatchJob.Models;
using BatchJob;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wcf.DynamicProxy;
using System.ServiceModel.Description;
using System.Reflection;
using Newtonsoft.Json;
using BatchJob.Filters;
using BatchJob.ServiceInterface;

namespace BatchJob.Controllers
{
    public class HomeController : ControllerBase
    {
        [Dependency]
        public ISchedulerService SchedulerRepository { get; set; }

        [Dependency]
        public ITriggerService TriggerRepository { get; set; }

        [Dependency]
        public IJobService JobRepository { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        #region Scheduler
        [ActionName("scheduler-manager")]
        public ActionResult SchedulerManager()
        {
            var dtos = SchedulerRepository.GetSchedulers();
            var vms = new List<SchedulerViewModel>();
            foreach (var dto in dtos)
            {
                vms.Add(SchedulerViewModel.Create(dto));
            }
            return View(vms);
        }

        [ActionName("create-scheduler")]
        public ActionResult CreateScheduler()
        {
            return View();
        }

        [ActionName("create-scheduler"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateScheduler(SchedulerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var dto = model.ToDto();
            var vm = SchedulerViewModel.Create(dto);
            SchedulerRepository.Create(model.ToDto());
            return RedirectToAction("scheduler-manager");
        }

        [ActionName("edit-scheduler")]
        public ActionResult EditScheduler(int id)
        {
            var dto = SchedulerRepository.GetScheduler(id);
            if (dto != null)
            {
                var vm = SchedulerViewModel.Create(dto);
                return View(vm);
            }
            return RedirectToAction("scheduler-manager");
        }

        [ActionName("edit-scheduler"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditScheduler(SchedulerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            SchedulerRepository.Update(model.ToDto());
            return RedirectToAction("scheduler-manager");
        }

        [ActionName("delete-scheduler"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteScheduler(int schedulerId)
        {
            SchedulerRepository.Delete(schedulerId);
            return RedirectToAction("scheduler-manager");
        }
        #endregion

        #region Trigger
        [ActionName("trigger-manager")]
        public ActionResult TriggerManager()
        {
            var dtos = TriggerRepository.GetTriggers();
            var vms = new List<TriggerViewModel>();
            foreach (var dto in dtos)
            {
                vms.Add(TriggerViewModel.Create(dto));
            }
            return View(vms);
        }

        [ActionName("create-cron-trigger")]
        public ActionResult CreateCronTrigger()
        {
            var vm = new CronTriggerViewModel();
            vm.Jobs = JobRepository.GetJobs(null).ToSelectList();
            return View(vm);
        }

        [ActionName("create-cron-trigger"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateCronTrigger(CronTriggerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            TriggerRepository.CreateCronTrigger(model.ToDto());
            return RedirectToAction("trigger-manager");
        }

        [ActionName("edit-cron-trigger")]
        public ActionResult EditCronTrigger(int id)
        {
            var dto = TriggerRepository.GetCronGrigger(id);
            if (dto != null)
            {
                var vm = CronTriggerViewModel.Create(dto);
                vm.Jobs = JobRepository.GetJobs(null).ToSelectList();
                return View(vm);
            }
            return RedirectToAction("cron-trigger-manager");
        }

        [ActionName("edit-cron-trigger"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditCronTrigger(CronTriggerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            TriggerRepository.UpdateCronTrigger(model.ToDto());
            return RedirectToAction("trigger-manager");
        }

        [ActionName("delete-trigger"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteTrigger(int triggerId)
        {
            SchedulerRepository.Delete(triggerId);
            return RedirectToAction("trigger-manager");
        }
        #endregion

        #region Job Group
        [ActionName("job-group-manager")]
        public ActionResult JobGroupManager(int schedulerId)
        {
            var dtos = SchedulerRepository.GetJobGroups(schedulerId);
            var vms = new List<JobGroupViewModel>();
            foreach (var dto in dtos)
            {
                vms.Add(JobGroupViewModel.Create(dto));
            }
            ViewBag.SchedulerId = schedulerId;
            return View(vms);
        }

        [ActionName("create-job-group")]
        public ActionResult CreateJobGroup(int schedulerId)
        {
            var scheduler = SchedulerRepository.GetScheduler(schedulerId);
            var vm = new JobGroupViewModel();
            vm.SchedulerId = scheduler.Id.Value;
            vm.SchedulerName = scheduler.Name;
            return View(vm);
        }

        [ActionName("create-job-group"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateJobGroup(JobGroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            SchedulerRepository.AddJobGroup(model.ToDto());
            return RedirectToAction("job-group-manager", new { schedulerId = model.SchedulerId });
        }

        [ActionName("delete-job-group"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteJobGroup(int jobGroupId)
        {
            int? schedulerId = null;
            bool result = SchedulerRepository.TryRemoveJobGroup(jobGroupId, out schedulerId);
            if (result)
            {
                return RedirectToAction("job-group-manager", new { schedulerId = schedulerId.Value });
            }
            return RedirectToAction("scheduler-manager");
        }
        #endregion

        #region Job
        [ActionName("job-manager")]
        public ActionResult JobManager(int? groupId = null)
        {
            var dtos = JobRepository.GetJobs(groupId);
            var vms = new List<JobViewModel>();
            foreach (var dto in dtos)
            {
                vms.Add(JobViewModel.Create(dto));
            }
            ViewBag.GroupId = groupId;
            return View(vms);
        }

        [ActionName("create-job")]
        public ActionResult CreateJob(int? groupId = null)
        {
            var vm = new JobViewModel();
            if (groupId.HasValue)
            {
                vm.GroupId = groupId.Value;
            }
            vm.JobGroups = SchedulerRepository.GetJobGroups().ToSelectList();
            return View(vm);
        }

        [ActionName("create-job"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateJob(JobViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            JobRepository.Create(model.ToDto());
            return RedirectToAction("job-manager", new { groupId = model.GroupId });
        }

        [ActionName("edit-job")]
        public ActionResult EditJob(int id)
        {
            var dto = JobRepository.GetJob(id);
            var vm = JobViewModel.Create(dto);
            vm.JobGroups = SchedulerRepository.GetJobGroups().ToSelectList();
            return View(vm);
        }

        [ActionName("edit-job"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult EditJob(JobViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            JobRepository.Update(model.ToDto());
            return RedirectToAction("job-manager", new { groupId = model.GroupId });
        }

        [ActionName("delete-job"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteJob(int jobId)
        {
            int? jobGroupId = null;
            bool result = JobRepository.TryDelete(jobId, out jobGroupId);
            if (result)
            {
                return RedirectToAction("job-manager", new { groupId = jobGroupId.Value });
            }
            return RedirectToAction("index");
        }

        [ActionName("run-job"), ChildForm]
        public ActionResult RunJob(int id)
        {
            JobRepository.RunJob(id);
            return RedirectToAction("job-manager");
        }

        [ActionName("stop-job"), ChildForm]
        public ActionResult StopJob(int id)
        {
            JobRepository.StopJob(id);
            return RedirectToAction("job-manager");
        }
        #endregion

        #region Service
        [ActionName("service-manager")]
        public ActionResult ServiceManager(int jobId)
        {
            ViewBag.JobId = jobId;
            var dtos = JobRepository.GetServices(jobId);
            var vms = new List<ServiceViewModel>();
            foreach (var dto in dtos)
            {
                vms.Add(ServiceViewModel.Create(dto));
            }
            return View(vms);
        }

        [ActionName("add-service")]
        public ActionResult AddService(int jobId)
        {
            var vm = new ServiceViewModel();
            vm.JobId = jobId;
            return View(vm);
        }

        [ActionName("add-service"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddService(ServiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var contractMethod = model.ContractMethod.Split('_');
            model.OperationContractName = contractMethod[0];
            model.MethodName = contractMethod[1];
            var dto = model.ToDto();
            var parameterJson = Request["MethodParameter"];
            if (parameterJson != null && !string.IsNullOrWhiteSpace(parameterJson))
            {
                var parameters = JsonConvert.DeserializeObject<OperationParameterInfo[]>(parameterJson);
                dto.Parameters = new List<ServiceDto.ServiceParameterDto>();
                foreach (var parameter in parameters)
                {
                    dto.Parameters.Add(new ServiceDto.ServiceParameterDto()
                    {
                        TypeName = parameter.ParameterType,
                        Name = parameter.ParameterName,
                        Value = Request[parameter.ParameterName]
                    });
                }
            }
            JobRepository.AddService(dto);
            return RedirectToAction("service-manager", new { jobId = model.JobId });
        }

        [ActionName("remove-service"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult RemoveService(int serviceId)
        {
            int? jobId = null;
            bool result = JobRepository.TryRemoveService(serviceId, out jobId);
            if (result)
            {
                return RedirectToAction("service-manager", new { jobId = jobId.Value });
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        [ActionName("resolve-service"), HttpPost]
        public ActionResult ResolveService()
        {
            string serviceAddress = Request["serviceAddress"];
            ServiceInfo serviceInfo = new ServiceInfo
            {
                ServiceAddress = serviceAddress,
                ContractInfos = new List<ServiceContractInfo>()
            };

            DynamicProxyFactory factory = new DynamicProxyFactory(serviceAddress);
            //get contracts
            var contracts = factory.Contracts;
            foreach (ContractDescription contract in contracts)
            {
                var contractInfo = new ServiceContractInfo
                {
                    ContractName = contract.Name,
                    OperationInfos = new List<ContractOperationInfo>()
                };
                serviceInfo.ContractInfos.Add(contractInfo);
                //get methods in each contract
                var operations = contract.Operations;
                foreach (OperationDescription operation in operations)
                {
                    try
                    {
                        var operationInfo = new ContractOperationInfo
                        {
                            OperationName = operation.Name,
                            ParameterInfos = new List<OperationParameterInfo>()
                        };
                        contractInfo.OperationInfos.Add(operationInfo);
                        //get parameters in each method
                        Type contractType = factory.ProxyAssembly.GetType(contract.Name);

                        var method = contractType.GetMethod(operation.Name);
                        if (method != null)
                        {
                            var parameters = method.GetParameters();

                            foreach (ParameterInfo parameter in parameters)
                            {
                                operationInfo.ParameterInfos.Add(new OperationParameterInfo
                                {
                                    ParameterName = parameter.Name,
                                    ParameterType = parameter.ParameterType.ToString()
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return Json(serviceInfo);
        }
        #endregion
    }
}