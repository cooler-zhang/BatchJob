using Castle.DynamicProxy;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using Wcf.DynamicProxy;

namespace BatchJob.Domain
{
    public class ServiceEntity : BaseEntity
    {
        /// <summary>
        /// 动态代理缓存
        /// </summary>
        static ConcurrentDictionary<int, DynamicProxy> _dynamicProxyCache = new ConcurrentDictionary<int, DynamicProxy>();

        [Required(ErrorMessage = "服务地址不能为空")]
        [MaxLength(1000, ErrorMessage = "服务地址长度不能超过1000")]
        public string ServiceAddress { get; set; }

        [Required(ErrorMessage = "服务契约不能为空")]
        public string OperationContractName { get; set; }

        [Required(ErrorMessage = "服务方法不能为空")]
        [MaxLength(500, ErrorMessage = "服务方法长度不能超过500")]
        public string MethodName { get; set; }

        public virtual JobEntity Job { get; set; }

        public virtual ICollection<ServiceParameterEntity> ServiceParameters { get; set; }

        public void Call()
        {
            DynamicProxy proxy = null;

            if (_dynamicProxyCache.ContainsKey(this.Id))
                proxy = _dynamicProxyCache[this.Id];
            else
            {
                proxy = CreateDynamicProxy();
                _dynamicProxyCache[this.Id] = proxy;
            }

            List<object> serviceParameters = new List<object>();
            foreach (var parameter in ServiceParameters)
            {
                Type paramterType = Type.GetType(parameter.TypeName);
                serviceParameters.Add(ChangeType(parameter.Value, paramterType));
            }
            object result = proxy.CallMethod(MethodName, serviceParameters.ToArray());
        }

        private DynamicProxy CreateDynamicProxy()
        {
            string serviceWsdlUri = ServiceAddress;

            // create the dynamic proxy factory, that downloads the service metadata 
            // and create the dynamic factory.
            DynamicProxyFactory factory = new DynamicProxyFactory(serviceWsdlUri);

            //Setup timeout
            var bindingList = factory.Bindings;
            if (bindingList != null)
            {
                foreach (var binding in bindingList)
                {
                    binding.OpenTimeout = new TimeSpan(0, 30, 0);
                    binding.ReceiveTimeout = new TimeSpan(0, 30, 0);
                    binding.SendTimeout = new TimeSpan(0, 30, 0);
                    binding.CloseTimeout = new TimeSpan(0, 30, 0);
                }
            }
            // operations
            DynamicProxy proxy = factory.CreateProxy(OperationContractName);
            return proxy;
        }


        public object ChangeType(object value, Type convertsionType)
        {
            //判断convertsionType类型是否为泛型，因为nullable是泛型类,
            if (convertsionType.IsGenericType &&
                //判断convertsionType是否为nullable泛型类
                convertsionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null || value.ToString().Length == 0)
                {
                    return null;
                }
                //如果convertsionType为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                NullableConverter nullableConverter = new NullableConverter(convertsionType);
                //将convertsionType转换为nullable对的基础基元类型
                convertsionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, convertsionType);
        }
    }
}
