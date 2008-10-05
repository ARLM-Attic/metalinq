using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace ExpressionBuilder
{
    [DataContract]
    public class EditableMemberExpression : EditableExpression
    {
        EditableExpression _ex;
        MemberInfo _member;
       
        public MemberInfo Member { get { return _member; } set { _member = value; } }
        [DataMember]
        public EditableExpression Expression { get { return _ex; } set { _ex = value; } }

        public EditableMemberExpression()
        {

        }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.MemberAccess;
            }
            set
            {
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        public EditableMemberExpression(Expression rawEx, MemberInfo member)
            : this(EditableExpression.CreateEditableExpression(rawEx),member)
        {}

        public EditableMemberExpression(EditableExpression editEx, MemberInfo member)
        {
            _member = member;
            _ex = editEx;
        }

        public EditableMemberExpression(MemberExpression membEx)
            : this(EditableExpression.CreateEditableExpression(membEx),membEx.Member)
        {}

        public override Expression ToExpression()
        {
            return System.Linq.Expressions.Expression.MakeMemberAccess(_ex.ToExpression(), _member);
        }
    }
}
