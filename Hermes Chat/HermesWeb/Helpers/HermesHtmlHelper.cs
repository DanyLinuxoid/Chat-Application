using HermesLogic.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HermesWeb.Helpers
{
    /// <summary>
    /// Html helper for drawing basic html elements.
    /// </summary>
    public static class HermesHtmlHelper
    {
        /// <summary>
        /// Creates textbox based on type.
        /// </summary>
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
                // Simple/Generic textbox.
                return helper.TextBoxFor(expression, htmlAttributes);
            }
        }

        /// <summary>
        /// Generic textarea.
        /// </summary>
        public static IHtmlContent HermesTextAreaFor<TModel, TProperty>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null) // chat text area is without "input" class
        {
            return helper.TextAreaFor(expression, htmlAttributes: MergeHtmlAttributesWithCustomAttributes(htmlAttributes, new { @class = "input" }));
        }

        /// <summary>
        /// Generic submit button with specified type.
        /// </summary>
        public static IHtmlContent HermesInputButton(this IHtmlHelper helper, string text, string id = null, string type = null, string className = null, string prefill = null)
        {
            if (className == null)
            {
                className = "btn-classic-mid";
            }

            return type == "submit"
                ? new HtmlContentBuilder().AppendHtml
                    ($"<button id='{id}' placeholder='{prefill}' type='submit' class='{className}'>{text}</button>")
                : new HtmlContentBuilder().AppendHtml
                    ($"<input type='button' id='{id}' class='{className}' value='{text}'></input>");
        }

        /// <summary>
        /// Simple html label.
        /// </summary>
        public static IHtmlContent HermesLabelFor<TModel, TProperty>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return helper.LabelFor(expression, htmlAttributes);
        }

        /// <summary>
        /// Modified link which redirects to specified IActionResult (R4MVC)
        /// </summary>
        public static IHtmlContent HermesActionLink(this IHtmlHelper helper, string linkText, IActionResult actionResult, object htmlAttributes = null)
        {
                return helper.ActionLink(linkText, actionResult, htmlAttributes: MergeHtmlAttributesWithCustomAttributes(htmlAttributes, new { @class = "link" }));
        }

        /// <summary>
        /// Displays custom validation/error message for forms.
        /// </summary>
        public static IHtmlContent HermesValidationMessageFor<TModel, TProperty>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return helper.ValidationMessageFor(expression, null, htmlAttributes: MergeHtmlAttributesWithCustomAttributes(htmlAttributes, new { @class = "error-msg" }));
        }

        /// <summary>
        /// Image holder.
        /// </summary>
        public static IHtmlContent HermesImage(this IHtmlHelper helper, byte[] imageData, string className = null)
        {
            return new HtmlContentBuilder().SetHtmlContent
                 ($"<img class='{className}' src='data:image;base64,{ imageData?.GetBase64String() }' />");
        }

        /// <summary>
        /// Method to merge both passed in html attributes (class names, etc) and default attributes that are defined withing html extension method.
        /// </summary>
        /// <param name="defaultHtmlAttributes">Default html attributes.</param>
        /// <param name="customHtmlAttributes">Some custom html attributes.</param>
        /// <returns>Dictionary of merged attributes, that can be used for html helper extension, as it is accepting dictionary of attributes.</returns>
        private static IDictionary<string, object> MergeHtmlAttributesWithCustomAttributes(object defaultHtmlAttributes, object customHtmlAttributes)
        {
            if (defaultHtmlAttributes == null)
            {
                return new RouteValueDictionary(AnonymousObjectToHtmlAttributes(customHtmlAttributes));
            }

            RouteValueDictionary defaultAttributes = new RouteValueDictionary(AnonymousObjectToHtmlAttributes(defaultHtmlAttributes));
            RouteValueDictionary customAttributes = new RouteValueDictionary(AnonymousObjectToHtmlAttributes(customHtmlAttributes));

            // Attribute merge:
            // For each custom attribute check if it exists in default attributes
            // If exists - add values by fomatting
            // If not - insert value without formatting/appending.
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

        /// <summary>
        ///     Replaces anonymous object with Dictionary representation of html attributes
        ///     new { @class = "test" } gets mapped 
        /// </summary>
        /// <param name="htmlAttributes">Html attributes to turn into dictionary of attributes.</param>
        /// <returns>
        ///     Dictionary representation of attributes from object.
        ///     { @class = "row col-md-6"} Becomes dictionary with key value:
        ///     <class, row col-md-6>
        /// </returns>
        private static IDictionary<string, object> AnonymousObjectToHtmlAttributes(object htmlAttributes)
        {
            var dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            // Getting rid of object representation for attributes.
            // Attributes become something like 
            var stringAttributes = htmlAttributes.ToString().Replace("{", "").Replace("}", "").Replace("@", "").Replace("=", "").Replace("  ", " ");

            // Splitting attributes into array, result is like
            // class row classname2
            string[] stringAttributesArray = stringAttributes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var attribute in stringAttributesArray)
            {
                string[] attribKeyValue = attribute.Trim().Split(' ');

                // First element is ALWAYS attribute e.g class, display...
                string key = attribKeyValue.First();

                // "class row classname2" from string becomes dictionary key-value pair with "class" as key and "row classname2" as value.
                string value = attribute.Substring(attribute.IndexOf(key) + key.Length).Trim(); // Getting string after first word (which always must be an attribute).
                dictionary[key] = value;
            }

            return dictionary;
        }
    }
}