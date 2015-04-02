/** @file   IgniteModels.cs
 *  @author Ian Lamb
 *  @date   2012-10-25
 *  @brief  Helper classes for the Ignite User Portal
 *          Most importantly IgniteService
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Ignite.Models
{
    //
    // IgniteService is a wrapper class for handling HttpClient and is extended
    // by Fitness, Nutrition, etc. for making data calls to the web api
    public class IgniteService
    {
        public string hostUrl { get; set; }
        public string rootSuffix { get; set; }
        public HttpClient client { get; set; }
        public CancellationTokenModelBinder cancellationToken { get; set; }

        public IgniteService()
        {
            //
            // Change this if there is any change to the service's _root_ url
            // Anything after the trailing slash should be put in the rootSuffix
            // Example:      IgniteService.rootUrl       FitnessService.rootSuffix   Action
            //                          V                           V                  V
            //          [http://ignitedemo.cloudapp.net/][api/FitnessService.svc/][routine/22]
            //
            //hostUrl = "http://ignitedemo.cloudapp.net/";
            //hostUrl = "http://192.168.0.100:81/";
            //hostUrl = "http://localhost:81/";
            hostUrl = "http://localhost:25883/";

            // initialize the HttpClient object which is the basis for all of our data requests
            client = new HttpClient();

            // the BaseAddress can only be the root url, 
            // so anything after 'http://domain:port/' goes in the rootSuffix property
            client.BaseAddress = new Uri(hostUrl);

            // this header is required to work with our service
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }

    //
    // Helper class to streamline things like error logging
    public class IgniteHelper
    {
        public IgniteHelper() { }

        /*
        public static bool IsNothing(T value)
        {
            if (value == null)
                return true;
            if (value.Equals(""))
                return true;
            if (value.Equals(0))
                return true;

            return false;
        }*/

        public static void LogError(Exception ex)
        {
            Console.WriteLine(ex.Message);

            //logging
            //TODO
        }
    }

    //
    // Custom null values for the Json Serializer
    // Thanks to 32bitkid@stackoverflow for the original code
    public class NullableValueProvider : Newtonsoft.Json.Serialization.IValueProvider
    {
        private readonly object _defaultValue;
        private readonly Newtonsoft.Json.Serialization.IValueProvider _underlyingValueProvider;

        public NullableValueProvider(MemberInfo memberInfo, object defaultValue)
        {
            _underlyingValueProvider = new DynamicValueProvider(memberInfo);
            _defaultValue = defaultValue;
        }

        public void SetValue(object target, object value)
        {
            _underlyingValueProvider.SetValue(target, value);
        }

        public object GetValue(object target)
        {
            return _underlyingValueProvider.GetValue(target) ?? _defaultValue;
        }
    }

    //
    // Custom contract resolver to give to the Json Serializer to implement the custom null values with
    // Thanks to 32bitkid@stackoverflow for the original code
    public class SpecialContractResolver : DefaultContractResolver
    {
        protected override Newtonsoft.Json.Serialization.IValueProvider CreateMemberValueProvider(MemberInfo member)
        {
            if (member.MemberType == MemberTypes.Property)
            {
                var pi = (PropertyInfo)member;
                if (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    return new NullableValueProvider(member, Activator.CreateInstance(pi.PropertyType.GetGenericArguments().First()));
                }
                else if (Type.GetType(pi.PropertyType.FullName) == typeof(string))
                {
                    return new NullableValueProvider(member, "" as string);
                }
            }
            else if (member.MemberType == MemberTypes.Field)
            {
                var fi = (FieldInfo)member;
                if (fi.FieldType.IsGenericType && fi.FieldType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    return new NullableValueProvider(member, Activator.CreateInstance(fi.FieldType.GetGenericArguments().First()));
                }
                else if (Type.GetType(fi.FieldType.FullName) == typeof(string))
                {
                    return new NullableValueProvider(member, Type.GetType(fi.FieldType.FullName));
                }
            }

            return base.CreateMemberValueProvider(member);
        }
    }
}
