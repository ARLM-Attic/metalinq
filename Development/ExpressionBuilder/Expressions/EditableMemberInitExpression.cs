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
    public class EditableMemberInitExpression : EditableExpression
    {
        List<MemberBinding> _memberBindings = new List<MemberBinding>();
        EditableNewExpression _new;

        [DataMember]
        public EditableNewExpression NewExpression { get { return _new; } set { _new = value; } }
        [DataMember]
        public List<MemberBinding> Bindings { get { return _memberBindings; } }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.MemberInit;
            }
            set
            {
                // throw new Exception("The method or operation is not implemented.");
            }
        }

        public EditableMemberInitExpression()
        {

        }

        public EditableMemberInitExpression(MemberInitExpression membInit)
            : this(EditableExpression.CreateEditableExpression(membInit.NewExpression) as EditableNewExpression,membInit.Bindings)
        {}

        public EditableMemberInitExpression(EditableNewExpression newEx, IEnumerable<MemberBinding> bindings)
        {
            _memberBindings.AddRange(bindings);
            _new = newEx;
        }

        public EditableMemberInitExpression(NewExpression newRawEx, IEnumerable<MemberBinding> bindings)
            : this(EditableExpression.CreateEditableExpression(newRawEx) as EditableNewExpression,bindings)
        {}

        public override Expression ToExpression()
        {
            return Expression.MemberInit(_new.ToExpression() as NewExpression,_memberBindings.ToArray());
        }
    }
}
