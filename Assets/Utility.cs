using System;
using System.Collections.Generic;
using System.Linq;

 
    public static class Utility
    {
        /// <summary>
        /// Get Sub Classes of a class
        /// </summary>
        /// <param name="parent"></param> 
        /// <returns></returns>
        public static IEnumerable<System.Type> GetAllSubclassOf(System.Type parent)
        {
            var type = parent;
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));
            return types;
        }
    } 