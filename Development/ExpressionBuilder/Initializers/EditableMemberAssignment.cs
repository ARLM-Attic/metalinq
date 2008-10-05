using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Linq.Expressions;

namespace ExpressionBuilder
{
    [DataContract]
    public class EditableMemberAssignment : EditableMemberBinding
    {
        protected EditableExpression _expression;

        [DataMember]
        public EditableExpression Expression { get {return _expression;} set {_expression = value;}}

        public EditableMemberAssignment ()
	    {

	    }

        public EditableMemberAssignment (MemberAssignment member) : base(member.BindingType, member.Member)
	    {
            _expression = EditableExpression.CreateEditableExpression(member.Expression);
	    }

        public override MemberBinding ToMemberBinding()
        {
            return System.Linq.Expressions.Expression.Bind(_member, _expression.ToExpression());
        }

         public override MemberBindingType BindingType
        {
            get
            {
                return MemberBindingType.Assignment;
            }
            set
            {
                
            }
        }
    }
}
