using Sitecore.Diagnostics;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;
using System;

namespace Sitecore.SharedSource.Conditions
{
    public class UserProfileFieldsComparisonCondition<T> : StringOperatorCondition<T> where T : RuleContext
    {
        public string FieldName1 { get; set; }

        public string FieldName2 { get; set; }

        protected override bool Execute(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, "ruleContext");

            if (String.IsNullOrEmpty(FieldName1) || String.IsNullOrEmpty(FieldName2)) return false;

            var profileKeyValue1 = Common.GetUserProfile(FieldName1);
            if (String.IsNullOrEmpty(profileKeyValue1)) return false;

            var profileKeyValue2 = Common.GetUserProfile(FieldName2);
            if (String.IsNullOrEmpty(profileKeyValue2)) return false;

            return Compare(profileKeyValue1, profileKeyValue2);
        }
    }
}