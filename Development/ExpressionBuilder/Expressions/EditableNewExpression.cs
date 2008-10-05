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

        public ConstructorInfo Constructor { get { return _constructor; } set { _constructor = value; } }
        [DataMember]
        public EditableExpressionCollection Arguments { get { return _arguments; } }

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
            : this(newEx.Constructor, new EditableExpressionCollection(newEx.Arguments))
        { }

        public EditableNewExpression(ConstructorInfo constructor, IEnumerable<EditableExpression> arguments)
            : this(constructor, new EditableExpressionCollection(arguments))
        { }

        public EditableNewExpression(ConstructorInfo constructor, EditableExpressionCollection arguments)
        {
            _arguments = arguments;
            _constructor = constructor;
        }

        public override Expression ToExpression()
        {
            return Expression.New(_constructor, _arguments.GetExpressions());
        }
    }
}
