﻿using System;
using System.Reflection;

namespace Serenity.ComponentModel
{
    /// <summary>
    /// Indicates that this method / type should generate a remote data
    /// script. Data contained by remote data scripts can be accessed
    /// client side using Q.getRemoteData("Key") function.
    /// </summary>
    /// <seealso cref="Serenity.ComponentModel.DynamicScriptAttribute" />
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple=false)]
    public class DataScriptAttribute : Attribute
    {
        public DataScriptAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataScriptAttribute"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public DataScriptAttribute(string key)
        {
            Check.NotNull(key, "key");
            this.Key = key;
        }

        public string Key { get; private set; }

        /// <summary>
        /// Permission key required to access this data script.
        /// Use special value "?" for all logged-in users.
        /// Use special value "*" for anyone including not logged-in users.
        /// </summary>
        public string Permission { get; set; }

        /// <summary>
        /// Cache duration in seconds
        /// </summary>
        public int CacheDuration { get; set; }

        /// <summary>
        /// Gets or sets the cache group key. 
        /// Group keys are used to invalidate a group of items.
        /// </summary>
        /// <value>
        /// The cache group key.
        /// </value>
        public string CacheGroupKey { get; set; }

        public static string AutoKeyFor(Type type)
        {
            string module;
            var moduleAttr = type.GetCustomAttribute<ModuleAttribute>(true);
            if (moduleAttr != null)
                module = moduleAttr.Value;
            else
            {
                module = type.Namespace ?? "";

                if (module.EndsWith(".Lookups") || module.EndsWith(".Scripts"))
                    module = module.Substring(0, module.Length - 8);

                var idx = module.IndexOf(".");
                if (idx >= 0)
                    module = module.Substring(idx + 1);
            }

            var name = type.Name;

            return string.IsNullOrEmpty(module) ? name :
                module + "." + name;
        }
    }
}