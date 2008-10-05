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
    public class EditableNewExpression : EditableExpression
    {
        protected ConstructorInfo _constructor;
        protected EditableExpressionCollection _arguments;
        protected Type _type;

        public ConstructorInfo Constructor { get { return _constructor; } set { _constructor = value; } }
        [DataMember]
        public EditableExpressionCollection Arguments { get { return _arguments; } set { _arguments = value; } }

        [DataMember]
        public EditableMemberInfoCollection Members { get; set; }

        [DataMember()]
        private string TypeName
        {
            get
            {
                return _type.ToSerializableForm();
            }
            set
            {
                _type = _type.FromSerializableForm(value);
            }
        }

        public Type Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        [DataMember()]
        private string ConstructorName
        {
            get
            {
                return _constructor.ToSerializableForm();
            }
            set
            {
                _constructor = _constructor.FromSerializableForm(value);
            }
        }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.New;
            }
            set
            {
                // throw new Exception("The method or operation is not implemented.");
            }
        }

        public EditableNewExpression()
        {

        }

        public EditableNewExpression(NewExpression newEx)
            : this(newEx.Constructor, new EditableExpressionCollection(newEx.Arguments), newEx.Members, newEx.Type)
        { }

        public EditableNewExpression(ConstructorInfo constructor, IEnumerable<EditableExpression> arguments, IEnumerable<MemberInfo> members, Type type)
            : this(constructor, new EditableExpressionCollection(arguments), members, type)
        { }

        public EditableNewExpression(ConstructorInfo constructor, EditableExpressionCollection arguments, IEnumerable<MemberInfo> members, Type type)
        {
            _arguments = arguments;
            _constructor = constructor;
            _type = type;
            Members = new EditableMemberInfoCollection(members);
        }

        public override Expression ToExpression()
        {
            if (_constructor != null)
                return Expression.New(_constructor, _arguments.GetExpressions());
            else
                return Expression.New(_type);
        }
    }
}
