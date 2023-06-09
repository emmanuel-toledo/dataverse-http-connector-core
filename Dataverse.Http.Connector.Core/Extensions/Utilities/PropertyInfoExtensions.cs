﻿using System.Reflection;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace Dataverse.Http.Connector.Core.Extensions.Utilities
{
    /// <summary>
    /// This class contains extensions methods fot "PropertyInfo" instance.
    /// </summary>
    internal static class PropertyInfoExtensions
    {
        /// <summary>
        /// Funtion to set a value of custom TEntity property from a Json object.
        /// </summary>
        /// <typeparam name="TEntity">TEntity class.</typeparam>
        /// <param name="property">Property infor of TEntity class.</param>
        /// <param name="entity">TEntity instance to set value.</param>
        /// <param name="fieldAttributes">Entity field attribute configurations.</param>
        /// <param name="jsonObject">Json object instance.</param>
        public static void SetTEntityPropertyValue<TEntity>
            (this PropertyInfo property, TEntity entity, Domains.Annotations.FieldAttributes fieldAttributes, JToken jsonObject)
            where TEntity : class, new()
        {
            var propType = property.PropertyType;
            var converter = TypeDescriptor.GetConverter(propType);
            property.SetValue(entity, converter.ConvertFrom(jsonObject.Value<string>(fieldAttributes.SchemaName!)!), null);
        }

        /// <summary>
        /// Function to get a specific property value from a TEntity.
        /// </summary>
        /// <typeparam name="TEntity">TEntity class.</typeparam>
        /// <param name="property">Property infor of TEntity class.</param>
        /// <param name="entity">TEntity instance to be used to get value.</param>
        /// <returns>Property value in string format.</returns>
        public static string? GetTEntityPropertyValue<TEntity>(this PropertyInfo property, TEntity entity) where TEntity : class, new()
        {
            var value = property.GetValue(entity);
            return value is null ? "" : value.ToString();
        }
    }
}