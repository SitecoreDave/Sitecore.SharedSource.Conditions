using Sitecore.Diagnostics;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;
using System;

namespace Sitecore.SharedSource.Conditions
{
    public class UserProfileDateFieldsCondition<T> : OperatorCondition<T> where T : RuleContext
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
            if (String.IsNullOrEmpty(profileKeyValue1)) return false;

            DateTime profileDate1;
            if (!DateTime.TryParse(profileKeyValue1, out profileDate1)) return false;

            DateTime profileDate2;
            if (!DateTime.TryParse(profileKeyValue2, out profileDate2)) return false;

            var compareResult = profileDate1.CompareTo(profileDate2);

            switch (GetOperator())
            {
                case ConditionOperator.Equal:
                    return compareResult == 0;
                case ConditionOperator.GreaterThanOrEqual:
                    return compareResult == 0 || compareResult > 0;
                case ConditionOperator.GreaterThan:
                    return compareResult > 0;
                case ConditionOperator.LessThanOrEqual:
                    return compareResult == 0 || compareResult < 0;
                case ConditionOperator.LessThan:
                    return compareResult < 0;
                case ConditionOperator.NotEqual:
                    return compareResult != 0;
                default:
                    return false;
            }
        }
    }
}