using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using System.Linq.Expressions;

namespace ExpressionBuilder
{
    [DataContract]
    [KnownType(typeof(EditableMemberAssignment))]
    [KnownType(typeof(EditableMemberListBinding))]
    [KnownType(typeof(EditableMemberMemberBinding))]
    public abstract class EditableMemberBinding
    {
        protected MemberInfo _member;
       
        public abstract MemberBindingType BindingType { get; set; }       

        public MemberInfo Member { get { return _member; } set { _member = value; } }

        [DataMember]
        public string MemberName
        {
            get
            {
                return _member.ToSerializableForm();
            }
            set
            {
                _member = _member.FromSerializableForm(value);
            }
        }

        protected EditableMemberBinding()
        {
        }

        protected EditableMemberBinding(MemberBindingType type, MemberInfo member)
        {
            BindingType = type;
            _member = member;
        }

        public abstract MemberBinding ToMemberBinding();

        public static EditableMemberBinding CreateEditableMemberBinding(MemberBinding member)
        {
            if (member is MemberAssignment) return new EditableMemberAssignment(member as MemberAssignment);
            else if (member is MemberListBinding) return new EditableMemberListBinding(member as MemberListBinding);
            else if (member is MemberMemberBinding) return new EditableMemberMemberBinding(member as MemberMemberBinding);
            else return null;

        }
    }
}
