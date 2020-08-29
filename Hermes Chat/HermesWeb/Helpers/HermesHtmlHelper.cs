using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HermesChat.Helpers
{
    public static class HermesHtmlHelper
    {
        public static IHtmlContent HermesTextBoxFor<TModel, TProperty>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string type = null, object htmlAttributes = null)
        {
            if (type == "Password")
            {
                return helper.PasswordFor(expression, htmlAttributes);
            }
            else if (type == "Hidden")
            {
                return helper.HiddenFor(expression, htmlAttributes);
            }
            else
            {
                return helper.TextBoxFor(expression, htmlAttributes);
            }
        }

        public static IHtmlContent HermesTextAreaFor<TModel, TProperty>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string type = null, object htmlAttributes = null)
        {
            return helper.TextAreaFor(expression, htmlAttributes: MergeHtmlAttributesWithCustomAttributes(htmlAttributes, new { @class = "input" }));
        }

        public static IHtmlContent HermesChatTextAreaFor<TModel, TProperty>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string type = null, object htmlAttributes = null)
        {
            return helper.TextAreaFor(expression, htmlAttributes: htmlAttributes);
        }

        public static IHtmlContent HermesSubmitButton(this IHtmlHelper helper, string text, string id = null, string className = null, string prefill = null)
        {
            if (className == null)
            {
                className = "button-classic-mid";
            }

            return new HtmlContentBuilder().AppendHtml
                ($"<button id='{id}' placeholder='{prefill}' type='submit' class='{className}'>{text}</button>");
        }

        public static IHtmlContent HermesInputButton(this IHtmlHelper helper, string text, string id = null, string className = null)
        {
            if (className == null)
            {
                className = "button-classic-mid";
            }

            return new HtmlContentBuilder().AppendHtml
                ($"<input type='button' id='{id}' class='{className}' value='{text}'></input>");
        }

        public static IHtmlContent HermesLabelFor<TModel, TProperty>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return helper.LabelFor(expression, htmlAttributes);
        }

        public static IHtmlContent HermesActionLink(this IHtmlHelper helper, string linkText, string controllerName, string actionName = null, object htmlAttributes = null)
        {
            if (string.IsNullOrEmpty(actionName))
            {
                actionName = "Index";
            }

            return helper.ActionLink(linkText, actionName, controllerName: controllerName, null, htmlAttributes: MergeHtmlAttributesWithCustomAttributes(htmlAttributes, new { @class = "link" }));
        }

        public static IHtmlContent HermesValidationMessageFor<TModel, TProperty>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return helper.ValidationMessageFor(expression, null, htmlAttributes: new { @class = "error-msg" });
        }

        private static IDictionary<string, object> MergeHtmlAttributesWithCustomAttributes(object defaultHtmlAttributes, object customHtmlAttributes)
        {
            if (defaultHtmlAttributes == null)
            {
                return new RouteValueDictionary(AnonymousObjectToHtmlAttributes(customHtmlAttributes));
            }

            RouteValueDictionary defaultAttributes = new RouteValueDictionary(AnonymousObjectToHtmlAttributes(defaultHtmlAttributes));
            RouteValueDictionary customAttributes = new RouteValueDictionary(AnonymousObjectToHtmlAttributes(customHtmlAttributes));

            foreach (var customAttribute in customAttributes)
            {
                if (defaultAttributes.ContainsKey(customAttribute.Key))
                {
                    defaultAttributes.TryGetValue(customAttribute.Key, out object defaultValue);
                    defaultAttributes[customAttribute.Key] = string.Format("{0} {1}", defaultValue, customAttribute.Value);
                }
                else
                {
                    defaultAttributes[customAttribute.Key] = customAttribute.Value;
                }
            }

            return defaultAttributes;
        }

        private static IDictionary<string, object> AnonymousObjectToHtmlAttributes(object htmlAttributes)
        {
            var dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            var stringAttributes = htmlAttributes.ToString().Replace("{", "").Replace("}", "").Replace("@", "").Replace("=", "").Replace("  ", " ");
            string[] stringAttributesArray = stringAttributes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var attribute in stringAttributesArray)
            {
                string[] attribKeyValue = attribute.Trim().Split(' ');
                string key = attribKeyValue.First();
                string value = attribute.Substring(attribute.IndexOf(key) + key.Length).Trim(); // Getting string after first word (which always must be an attribute).
                dictionary[key] = value;
            }

            return dictionary;
        }
    }
}