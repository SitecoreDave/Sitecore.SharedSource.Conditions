using Sitecore.Diagnostics;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;
using System;

namespace Sitecore.SharedSource.Conditions
{
    public class UserProfileDateOffsetCondition<T> : OperatorCondition<T> where T : RuleContext
    {
        public string FieldName { get; set; }

        public int Value { get; set; }

        protected override bool Execute(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, "ruleContext");

            if (String.IsNullOrEmpty(FieldName)) return false;

            var profileKeyValue = Common.GetUserProfile(FieldName);

            DateTime profileDate;

            if (!DateTime.TryParse(profileKeyValue, out profileDate)) return false;

            var compareResult = profileDate.AddDays(Value).CompareTo(DateTime.Now);

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