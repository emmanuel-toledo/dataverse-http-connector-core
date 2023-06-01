using Dynamics.Crm.Http.Connector.Core.Domains.Annotations;
using Dynamics.Crm.Http.Connector.Core.Domains.Enums;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Dynamics.Crm.Http.Connector.Core.Utilities
{
    internal static class Parse
    {
        private static T? ChangeType<T, V>(V value, TypeCode code)
            => (T?)Convert.ChangeType(value, code);

        public static string ParseValue<T>(T value)
        {
            string? parsedValue = string.Empty;
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.String:
                    parsedValue = ChangeType<string, T>(value, TypeCode.String) ?? "";
                    break;
                case TypeCode.Int32:
                    parsedValue = ChangeType<int, T>(value, TypeCode.String).ToString() ?? "";
                    break;
                case TypeCode.Double:
                    parsedValue = ChangeType<double, T>(value, TypeCode.String).ToString() ?? "";
                    break;
                case TypeCode.Decimal:
                    parsedValue = ChangeType<decimal, T>(value, TypeCode.String).ToString() ?? "";
                    break;
                case TypeCode.Boolean:
                    parsedValue = ChangeType<bool, T>(value, TypeCode.String).ToString() ?? "";
                    break;
                case TypeCode.DateTime:
                    parsedValue = ChangeType<DateTime, T>(value, TypeCode.String).ToString("yyyy-MM-ddTHH:mm:ssZ") ?? "";
                    break;
                case TypeCode.Object:
                    if (typeof(T) == typeof(Guid))
                        parsedValue = value!.ToString();

                    else
                        throw new ArgumentNullException(nameof(value));
                    break;
                default:
                    throw new ArgumentNullException(nameof(value));
            }
            return parsedValue!;
        }

        public static string ParseCondition(ConditionTypes conditionType)
        {
            string conditionString = string.Empty;
            switch (conditionType)
            {
                case ConditionTypes.Equal:
                    conditionString = "eq";
                    break;
                case ConditionTypes.NotEqual:
                    conditionString = "ne";
                    break;
                case ConditionTypes.Null:
                    conditionString = "null";
                    break;
                case ConditionTypes.NotNull:
                    conditionString = "not-null";
                    break;
                case ConditionTypes.BeginsWith:
                    conditionString = "begins-with";
                    break;
                case ConditionTypes.DoesNotBeginWith:
                    conditionString = "not-begin-with";
                    break;
                case ConditionTypes.EndsWith:
                    conditionString = "ends-with";
                    break;
                case ConditionTypes.DoesNotEndsWith:
                    conditionString = "not-end-with";
                    break;
                case ConditionTypes.Like:
                    conditionString = "like";
                    break;
                case ConditionTypes.NotLike:
                    conditionString = "not-like";
                    break;
                default:
                    throw new ArgumentNullException(nameof(conditionType));
            }
            return conditionString;
        }

        public static string ParseFilter(FilterTypes filterType)
        {
            return filterType switch
            {
                FilterTypes.Or => "or",
                _ => "and"
            };
        }

		public static TEntity? ParseTEntityToModel<TEntity>(JToken? jsonObject) where TEntity : class, new()
		{
			try
			{
                if (jsonObject is null) 
                    return null;
				var entity = new TEntity();
				var entityType = entity.GetType();
				foreach (var property in entityType.GetProperties())
				{
					var fieldAttributes = property.GetFieldAttribute();
					if (fieldAttributes is null)
						continue;

					//var propType = property.PropertyType;
					//var converter = TypeDescriptor.GetConverter(propType);
					// //var convertedObject = converter.ConvertFromString(entity);
					//property.SetValue(entity, converter.ConvertFrom(jsonObject.Value<string>(fieldAttributes.LogicalName!)!), null);

					// Set default value if property of JSON does not exists.
					var propertyValue = jsonObject.Value<string>(fieldAttributes.SchemaName!);
					if (string.IsNullOrEmpty(propertyValue))
                    {
						property.SetValue(entity, default, null);
                        continue;
					}

					// Check field type to parse information.
					switch (fieldAttributes.FieldType)
					{
						case FieldTypes.UniqueIdentifier:
						case FieldTypes.Lookup:
							property.SetValue(entity, Guid.Parse(propertyValue!), null);
							break;
						case FieldTypes.Text:
							property.SetValue(entity, Convert.ToString(propertyValue), null);
							break;
						case FieldTypes.DateTime:
							property.SetValue(entity, jsonObject.Value<DateTime>(fieldAttributes.LogicalName!), null);
							break;
					}
				}
				return entity;
			}
			catch (Exception ex)
			{
				throw new NotSupportedException("An error has ocurred during the convertion to the custom class.", ex);
			}
		}

		public static ICollection<TEntity> ParseTEntityToCollection<TEntity>(JArray jsonArray) where TEntity : class, new()
        {
            try
            {
				var collection = new List<TEntity>();
				foreach (var jsonObject in jsonArray)
				{
                    var entity = ParseTEntityToModel<TEntity>(jsonObject);
                    if(entity != null)
                        collection.Add(entity);
				}
				return collection;
			}
            catch(Exception ex)
            {
                throw new NotSupportedException("An error has ocurred during the convertion to the custom class.", ex);
            }
        }
    }
}
