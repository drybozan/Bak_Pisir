using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace BakPisir.CORE.Helper
{
    public static class HtmlHelper
    {
        public static MvcHtmlString TextBoxForControlHorizontal<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, int labelsize = 12)
        {
            var isRequired = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).IsRequired;
            var me = (expression.Body as MemberExpression);
            if (me == null) return null;

            object classAttributes = new { @class = "form-control" };
            IDictionary<string, object> inputAttributes = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(classAttributes);
            object inputRequiredAttr = new { required = "required" };
            if (isRequired)
            {
                inputAttributes = AddAttr(inputAttributes, inputRequiredAttr);
            }
            var maxLength = me.Member.GetCustomAttributes(typeof(MaxLengthAttribute), false).FirstOrDefault() as MaxLengthAttribute;
            if (maxLength != null)
            {
                var maxLengthAttribute = new { maxlength = maxLength.Length };
                inputAttributes = AddAttr(inputAttributes, maxLengthAttribute);
            }
            var attributes = AddAttr(inputAttributes, htmlAttributes);
            object labelAttributes = new { @class = "col-form-label" + (isRequired ? " required" : "") };
            object validationAttributes = new { @class = "text-danger" };

            var editorFor = htmlHelper.TextBoxFor(expression, attributes).ToHtmlString();
            var labelFor = htmlHelper.LabelFor(expression, labelAttributes).ToHtmlString();
            var validationFor = isRequired ? htmlHelper.ValidationMessageFor(expression, "", validationAttributes).ToHtmlString() : "";

            return new MvcHtmlString(BuildHorizontalTagOnly(editorFor, labelFor, validationFor, labelsize).ToString());
            //return MvcHtmlString.Create(htmlMerger(editorFor, labelFor, validationFor));
        }


        public static MvcHtmlString TextBoxForControl<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var isRequired = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).IsRequired;

            var me = (expression.Body as MemberExpression);
            if (me == null) return null;

            object classAttributes = new { @class = "form-control" };
            IDictionary<string, object> inputAttributes = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(classAttributes);
            object inputRequiredAttr = new { required = "required" };
            if (isRequired)
            {
                inputAttributes = AddAttr(inputAttributes, inputRequiredAttr);
            }
            var maxLength = me.Member.GetCustomAttributes(typeof(MaxLengthAttribute), false).FirstOrDefault() as MaxLengthAttribute;
            if (maxLength != null)
            {
                var maxLengthAttribute = new { maxlength = maxLength.Length };
                inputAttributes = AddAttr(inputAttributes, maxLengthAttribute);
            }
            var attributes = AddAttr(inputAttributes, htmlAttributes);
            object labelAttributes = new { @class = "col-form-label" + (isRequired ? " required" : "") };
            object validationAttributes = new { @class = "text-danger" };

            var editorFor = htmlHelper.TextBoxFor(expression, attributes).ToHtmlString();
            var labelFor = htmlHelper.LabelFor(expression, labelAttributes).ToHtmlString();
            var validationFor = isRequired ? htmlHelper.ValidationMessageFor(expression, "", validationAttributes).ToHtmlString() : "";

            return new MvcHtmlString(BuildTagOnly(editorFor, labelFor, validationFor).ToString());
        }



        public static MvcHtmlString TextboxControlNoLabel<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var isRequired = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).IsRequired;
            var me = (expression.Body as MemberExpression);
            if (me == null) return null;

            object classAttributes = new { @class = "form-control" };
            IDictionary<string, object> inputAttributes = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(classAttributes);
            object inputRequiredAttr = new { required = "required" };

            if (isRequired)
            {

                inputAttributes = AddAttr(inputAttributes, inputRequiredAttr);
            }



            var attributes = AddAttr(inputAttributes, htmlAttributes);
            object validationAttributes = new { @class = "text-danger" };

            var editorFor = htmlHelper.TextBoxFor(expression, attributes).ToHtmlString();
            var validationFor = isRequired ? htmlHelper.ValidationMessageFor(expression, "", validationAttributes).ToHtmlString() : "";

            return new MvcHtmlString(BuildTagOnlyWithoutLabel(editorFor, validationFor).ToString());

        }



        public static MvcHtmlString DropDownControl<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes = null, string firstItem = "")
        {
            var isRequired = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).IsRequired;
            selectList = selectList ?? new List<SelectListItem>();

            object classAttributes = new { @class = "form-control selectpicker" };
            IDictionary<string, object> inputAttributes = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(classAttributes);
            object labelAttributes = new { @class = "control-label" + (isRequired ? " required" : "") };
            object validationAttributes = new { @class = "text-danger" };

            object inputRequiredAttr = new { required = "required" };

            if (isRequired)
            {
                inputAttributes = AddAttr(inputAttributes, inputRequiredAttr);
            }

            var attributes = AddAttr(inputAttributes, htmlAttributes);

            string editorFor;
            if (string.IsNullOrEmpty(firstItem))
            {
                editorFor = htmlHelper.DropDownListFor(expression, selectList, attributes).ToHtmlString();
            }
            else
            {
                editorFor = htmlHelper.DropDownListFor(expression, selectList, firstItem, attributes).ToHtmlString();
            }

            var labelFor = htmlHelper.LabelFor(expression, labelAttributes).ToHtmlString();
            var validationFor = isRequired ? htmlHelper.ValidationMessageFor(expression, "", validationAttributes).ToHtmlString() : "";

            return new MvcHtmlString(BuildTagOnly(editorFor, labelFor, validationFor).ToString());

        }

        public static MvcHtmlString DropDownControlNoLabel<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes = null, string firstItem = "")
        {
            var isRequired = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).IsRequired;

            object classAttributes = new { @class = "form-control" };
            IDictionary<string, object> inputAttributes = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(classAttributes);
            object validationAttributes = new { @class = "text-danger" };

            object inputRequiredAttr = new { required = "required" };

            if (isRequired)
            {
                inputAttributes = AddAttr(inputAttributes, inputRequiredAttr);
            }


            var attributes = AddAttr(inputAttributes, htmlAttributes);
            string editorFor;
            if (string.IsNullOrEmpty(firstItem))
            {
                editorFor = htmlHelper.DropDownListFor(expression, selectList, attributes).ToHtmlString();
            }
            else
            {
                editorFor = htmlHelper.DropDownListFor(expression, selectList, firstItem, attributes).ToHtmlString();
            }

            var validationFor = isRequired ? htmlHelper.ValidationMessageFor(expression, "", validationAttributes).ToHtmlString() : "";

            return new MvcHtmlString(BuildTagOnlyWithoutLabel(editorFor, validationFor).ToString());

        }


        public static MvcHtmlString PhoneControl<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string extraAttr = "")
        {
            var isRequired = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).IsRequired;

            object classAttributes = new { data_mask = "(999) 999-9999", @class = "form-control" + (extraAttr == "" ? "" : " " + extraAttr) };
            object labelAttributes = new { @class = "control-label" + (isRequired ? " required" : "") };
            object validationAttributes = new { @class = "text-danger" };

            var editorFor = htmlHelper.TextBoxFor(expression, classAttributes).ToHtmlString();
            var labelFor = htmlHelper.LabelFor(expression, labelAttributes).ToHtmlString();
            var validationFor = isRequired ? htmlHelper.ValidationMessageFor(expression, "", validationAttributes).ToHtmlString() : "";
            return new MvcHtmlString(BuildTagOnly(editorFor, labelFor, validationFor).ToString());
        }

        public static MvcHtmlString TextAreaForControl<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var isRequired = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).IsRequired;
            var me = (expression.Body as MemberExpression);
            if (me == null) return null;

            object classAttributes = new { @class = "form-control" };
            IDictionary<string, object> inputAttributes = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(classAttributes);
            object inputRequiredAttr = new { required = "required" };
            if (isRequired)
            {
                inputAttributes = AddAttr(inputAttributes, inputRequiredAttr);
            }
            var maxLength = me.Member.GetCustomAttributes(typeof(MaxLengthAttribute), false).FirstOrDefault() as MaxLengthAttribute;
            if (maxLength != null)
            {
                var maxLengthAttribute = new { maxlength = maxLength.Length };
                inputAttributes = AddAttr(inputAttributes, maxLengthAttribute);
            }

            var attributes = AddAttr(inputAttributes, htmlAttributes);
            object labelAttributes = new { @class = "col-form-label" + (isRequired ? " required" : "") };
            object validationAttributes = new { @class = "text-danger" };

            var editorFor = htmlHelper.TextAreaFor(expression, attributes).ToHtmlString();
            var labelFor = htmlHelper.LabelFor(expression, labelAttributes).ToHtmlString();
            var validationFor = isRequired ? htmlHelper.ValidationMessageFor(expression, "", validationAttributes).ToHtmlString() : "";

            return new MvcHtmlString(BuildTagOnly(editorFor, labelFor, validationFor).ToString());
        }

        public static MvcHtmlString EMailControl<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var isRequired = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).IsRequired;
            var me = (expression.Body as MemberExpression);
            if (me == null) return null;


            object classAttributes = new { @class = "form-control", type = "email", size = "30", placeholder = "ornek@ornek.com" };
            IDictionary<string, object> inputAttributes = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(classAttributes);
            object inputRequiredAttr = new { required = "required" };

            if (isRequired)
            {
                inputAttributes = AddAttr(inputAttributes, inputRequiredAttr);
            }

            var attributes = AddAttr(inputAttributes, htmlAttributes);

            object labelAttributes = new { @class = "control-label" + (isRequired ? " required" : "") };
            object validationAttributes = new { @class = "text-danger" };

            var editorFor = htmlHelper.TextBoxFor(expression, attributes).ToHtmlString();
            var labelFor = htmlHelper.LabelFor(expression, labelAttributes).ToHtmlString();
            var validationFor = isRequired ? htmlHelper.ValidationMessageFor(expression, "", validationAttributes).ToHtmlString() : "";

            return new MvcHtmlString(BuildTagOnly(editorFor, labelFor, validationFor).ToString());

        }

        public static MvcHtmlString PasswordControl<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {

            var isRequired = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).IsRequired;

            object classAttributes = new { @class = "form-control", type = "password", autocomplete = "new-password" };
            IDictionary<string, object> inputAttributes = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(classAttributes);
            object labelAttributes = new { @class = "control-label" + (isRequired ? " required" : "") };
            object validationAttributes = new { @class = "text-danger" };

            object inputRequiredAttr = new { required = "required" };

            if (isRequired)
            {
                inputAttributes = AddAttr(inputAttributes, inputRequiredAttr);
            }


            var attributes = AddAttr(inputAttributes, htmlAttributes);

            var editorFor = htmlHelper.TextBoxFor(expression, attributes).ToHtmlString();
            var labelFor = htmlHelper.LabelFor(expression, labelAttributes).ToHtmlString();
            var validationFor = isRequired ? htmlHelper.ValidationMessageFor(expression, "", validationAttributes).ToHtmlString() : "";

            return new MvcHtmlString(BuildTagOnly(editorFor, labelFor, validationFor).ToString());
        }

        public static MvcHtmlString PasswordControlClassic<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {

            var isRequired = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).IsRequired;

            object classAttributes = new { @class = "form-control", type = "password" };
            IDictionary<string, object> inputAttributes = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(classAttributes);
            object validationAttributes = new { @class = "text-danger" };

            object inputRequiredAttr = new { required = "required" };

            if (isRequired)
            {
                inputAttributes = AddAttr(inputAttributes, inputRequiredAttr);
            }

            var attributes = AddAttr(inputAttributes, htmlAttributes);

            var editorFor = htmlHelper.TextBoxFor(expression, attributes).ToHtmlString();
            var labelFor = "";//htmlHelper.LabelFor(expression, labelAttributes);
            var validationFor = isRequired ? htmlHelper.ValidationMessageFor(expression, "", validationAttributes).ToHtmlString() : "";

            return new MvcHtmlString(BuildTagOnly(editorFor, labelFor, validationFor).ToString());
        }

        public static MvcHtmlString CheckBoxControl<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, object htmlAttributes = null)
        {
            var expressionId = ExpressionHelper.GetExpressionText(expression).Replace(".", "_");
            var switchId = (string)htmlAttributes?.GetType().GetProperty("id")?.GetValue(htmlAttributes, null) ?? expressionId;

            object classAttributes = new { @class = "custom-control-input" };
            IDictionary<string, object> inputAttributes = System.Web.Mvc.HtmlHelper.AnonymousObjectToHtmlAttributes(classAttributes);
            var attributes = AddAttr(inputAttributes, htmlAttributes);

            object labelAttributes = new { @class = "custom-control-label ", @for = switchId };

            //object validationAttributes = new { @class = "text-danger" };

            var editorFor = htmlHelper.CheckBoxFor(expression, attributes).ToHtmlString();
            var labelFor = htmlHelper.LabelFor(expression, labelAttributes).ToHtmlString();

            var validationFor = "";//htmlHelper.ValidationMessageFor(expression, "", validationAttributes).ToHtmlString();

            return new MvcHtmlString(BuildTagForCheckbox(editorFor, labelFor, validationFor).ToString());

        }

        //static IDictionary<string, object> Combine(object obj1, object obj2)
        //{
        //    var dict1 = new RouteValueDictionary(obj1);
        //    var dict2 = new RouteValueDictionary(obj2);
        //    IDictionary<string, object> result = new Dictionary<string, object>();
        //    foreach (var item in dict1)
        //    {
        //        Debug.WriteLine(item);
        //        Debug.WriteLine(item.Value);
        //        Debug.WriteLine(item.Key);
        //    }
        //    foreach (var pair in dict1.Concat(dict2))
        //    {

        //        result.Add(pair);
        //    }
        //    return result;
        //}

        static IDictionary<string, object> AddAttr(IDictionary<string, object> obj1, object obj2)
        {
            var dict = new RouteValueDictionary(obj2);
            var result = obj1.Concat(dict).ToDictionary(x => x.Key,
                x => x.Value);
            return result;
        }

        //static string htmlMerger(string editorFor, string labelFor, string validationFor)
        //{
        //    var space = "  ";
        //    StringBuilder htmlBuilder = new StringBuilder();

        //    htmlBuilder.Append(labelFor);
        //    htmlBuilder.Append(space);
        //    htmlBuilder.Append(editorFor);
        //    htmlBuilder.Append(space);
        //    htmlBuilder.Append(validationFor);
        //    return htmlBuilder.ToString();
        //}
        private static TagBuilder BuildTagOnly(string editorFor, string labelFor, string validationFor)
        {

            var div = new TagBuilder("div");
            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("input-group");
            innerDiv.AddCssClass("input-group-single");
            div.AddCssClass("input-group input-group-outline mb-3");

            div.InnerHtml += labelFor;

            innerDiv.AddCssClass("clearfix");
            innerDiv.InnerHtml += editorFor;
            innerDiv.InnerHtml += validationFor;

            div.InnerHtml += innerDiv;

            return div;
        }

        private static TagBuilder BuildTagForCheckbox(string editorFor, string labelFor, string validationFor)
        {
            var div = new TagBuilder("div");
            var innerDiv = new TagBuilder("div");
            div.AddCssClass("form-group");

            innerDiv.AddCssClass("custom-control custom-checkbox");
            innerDiv.InnerHtml += editorFor;

            innerDiv.InnerHtml += labelFor;

            innerDiv.InnerHtml += validationFor;

            div.InnerHtml += innerDiv;

            return div;
        }

        private static TagBuilder BuildTagOnlyWithoutLabel(string editorFor, string validationFor)
        {

            var div = new TagBuilder("div");
            var innerDiv = new TagBuilder("div");
            innerDiv.AddCssClass("input-group");
            innerDiv.AddCssClass("input-group-single");
            div.AddCssClass("form-group");

            innerDiv.AddCssClass("");
            innerDiv.InnerHtml += editorFor;
            innerDiv.InnerHtml += validationFor;

            div.InnerHtml += innerDiv;

            return div;
        }

        private static TagBuilder BuildHorizontalTagOnly(string editorFor, string labelFor, string validationFor, int labelSize)
        {

            var div = new TagBuilder("div");
            var innerDiv = new TagBuilder("div");
            div.AddCssClass("form-group row");

            div.InnerHtml += labelFor;

            innerDiv.AddCssClass("input-group input-group-outline mb-3 col-sm-" + (12 - labelSize));
            innerDiv.InnerHtml += editorFor;
            innerDiv.InnerHtml += validationFor;

            div.InnerHtml += innerDiv;

            return div;
        }
    }
}
