using Prism.AppModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVPConf.CheckIn
{
    static class ApplicationStoreExtensions
    {
        public static bool IsDataLoaded(this IApplicationStore applicationStore)
        {
            if (applicationStore.Properties.TryGetValue(ApplicationStoreKeys.LOADED_DATA, out object result))
            {
                return Convert.ToBoolean(result);
            }
            return false;
        }

        public static void MarkDataAsLoaded(this IApplicationStore applicationStore)
        {
            applicationStore.Properties[ApplicationStoreKeys.LOADED_DATA] = true;
        }

        public static void MarkDataAsNotLoaded(this IApplicationStore applicationStore)
        {
            applicationStore.Properties[ApplicationStoreKeys.LOADED_DATA] = false;
        }
    }
}
