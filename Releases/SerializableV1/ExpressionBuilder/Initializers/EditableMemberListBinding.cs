using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Linq.Expressions;

namespace ExpressionBuilder
{
    [DataContract]
    public class EditableMemberListBinding : EditableMemberBinding
    {
        protected EditableElementInitCollection _initializers = new EditableElementInitCollection();

        [DataMember]
        public EditableElementInitCollection Initializers { get { return _initializers; } set { _initializers = value; } }

        public EditableMemberListBinding()
        {

        }

        public EditableMemberListBinding(MemberListBinding member) : base(member.BindingType, member.Member)
        {
            foreach (ElementInit e in member.Initializers)
            {
                _initializers.Add(new EditableElementInit(e));
            }
        }

        public override MemberBinding ToMemberBinding()
        {
            return Expression.ListBind(_member, _initializers.GetElementsInit());
        }

         public override MemberBindingType BindingType
        {
            get
            {
                return MemberBindingType.ListBinding;
            }
            set
            {
                
            }
        }
    }
}
