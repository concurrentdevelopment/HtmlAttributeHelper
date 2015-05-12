namespace HtmlAttributeHelper
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Web.Mvc;

	public static class ViewDataDictionaryExtensions
	{
		public static IDictionary<string, object> GetHtmlAttributes<T>(this ViewDataDictionary<T> viewData, string additionalClass = null)
		{
			var attributes = viewData
				.ModelMetadata
				.AdditionalValues;

			// merge the classes from attributes, viewdata and passed in value
			attributes["class"] = String.Join(" ", new[]
			{
				attributes.ContainsKey("class") ? attributes["class"] : null,
				viewData["class"],
				additionalClass
			}.Where(x => x != null));

			// TODO - expand this list
			AddAdditionalValues<T>(attributes, viewData, "placeholder", "readonly", "disabled");

			AddDataValues<T>(attributes, viewData);

			return attributes;
		}

		private static void AddAdditionalValues<T>(IDictionary<string, object> attributes, ViewDataDictionary<T> viewData, params string[] keys)
		{
			foreach (var key in keys)
			{
				var value = viewData[key];

				if (value != null)
				{
					attributes[key] = value;
				}
			}
		}

		private static void AddDataValues<T>(IDictionary<string, object> attributes, ViewDataDictionary<T> viewData)
		{
			var items = viewData
				.Where(x => x.Key.StartsWith("data_"))
				.Select(x => new
				{
					Key = x.Key.Replace("_", "-"),
					Value = x.Value
				});

			foreach (var item in items)
			{
				attributes[item.Key] = item.Value;
			}
		}
	}
}
