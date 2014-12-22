using Sitecore.Diagnostics;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;
using System;
using System.Web;

namespace Sitecore.SharedSource.Conditions
{
    /// <summary>
    /// can use firefox plugin - RefControl to test
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpReferrerCondition<T> : StringOperatorCondition<T> where T:RuleContext
    {
        public string Value { get; set; }

        protected override bool Execute(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, "ruleContext");

            if (String.IsNullOrEmpty(Value) || HttpContext.Current.Request.UrlReferrer == null) return false;

            var urlReferrer = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
            return !String.IsNullOrEmpty(urlReferrer) && Compare(urlReferrer, Value);
        }
    }
}
