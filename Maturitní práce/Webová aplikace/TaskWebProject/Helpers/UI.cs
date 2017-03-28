using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskWebProject.Helpers
{
    public static class UI
    {
        //public static MvcHtmlString DrawLabel(LabelSet set, string text)
        //{
        //    string html = String.Format("<div class='label label-{0}'>{1}</div>",set.GetLabelType(text),text);
        //    return new MvcHtmlString(html);
        //}

        //public static MvcHtmlString DrawLabel<EnumType>(object value)
        //{
        //    return DrawLabel(typeof(EnumType), value);
        //}

        public static MvcHtmlString DrawLabel(Type EnumType, object value, object htmlAttributes = null)
        {
            TaskManager.UI.VisualMeaning attribute = (TaskManager.UI.VisualMeaning)EnumType.GetMember(value.ToString()).FirstOrDefault().GetCustomAttributes(typeof(TaskManager.UI.VisualMeaning), false).FirstOrDefault();

            TagBuilder builder = new TagBuilder("div");
            if (htmlAttributes != null)
            {
                builder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }
            builder.AddCssClass(String.Format("label label-{0}", attribute.Meaning));
            builder.SetInnerText(value.ToString());
            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }
    }

    //public static class EnumConverter
    //{
    //    public static string ItemToString<EnumType>(object item)
    //    {
    //        return Enum.GetName(typeof(EnumType), item);
    //    }
    //}

    //public static class LabelSets
    //{
    //    public static LabelSet TaskState = new LabelSet("New=warning;Assigned=primary;Done=success;Closed=default");
    //}

    //public class LabelSet
    //{
    //    private Dictionary<string,string> sets = new Dictionary<string,string>();
        
    //    public LabelSet()
    //    {

    //    }

    //    public LabelSet(string definitions)
    //    {
    //        foreach(string definition in definitions.Split(';'))
    //        {
    //            string[] defines = definition.Split('=');
    //            sets.Add(defines[0], defines[1]);
    //        }
    //    }

    //    public string GetLabelType(string text)
    //    {
    //        string type;
    //        if(sets.TryGetValue(text,out type))
    //        {
    //            return type;
    //        }
    //        else 
    //            return "default";
    //    }
    //}
}