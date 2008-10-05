using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Linq.Expressions;

namespace ExpressionBuilder
{
    [DataContract]
    class EditableMemberMemberBinding : EditableMemberBinding
    {
        [DataMember]
        public EditableMemberBindingCollection Bindings { get; set; }

        public EditableMemberMemberBinding()
        {

        }

        public EditableMemberMemberBinding(MemberMemberBinding member)
            : base(member.BindingType, member.Member)
        {
            Bindings = new EditableMemberBindingCollection(member.Bindings);
        }

        public override MemberBinding ToMemberBinding()
        {
            return Expression.MemberBind(_member, Bindings.GetMemberBindings());
        }

        public override MemberBindingType BindingType
        {
            get
            {
                return MemberBindingType.MemberBinding;
            }
            set
            {
                
            }
        }
    }
}
