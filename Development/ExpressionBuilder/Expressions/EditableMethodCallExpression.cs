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
    public class EditableMethodCallExpression : EditableExpression
    {
        protected EditableExpressionCollection _arguments;
        protected MethodInfo _method;
        protected EditableExpression _object;
        protected ExpressionType _nodeType;

        [DataMember]
        public EditableExpressionCollection Arguments { get { return _arguments; } set { _arguments = value; } }
        public MethodInfo Method { get { return _method; } set { _method = value; } }

        [DataMember]
        public string MethodName
        {
            get
            {
                return _method.ToSerializableForm();
            }
            set
            {
                _method = _method.FromSerializableForm(value);
            }
        }

        [DataMember]
        public EditableExpression Object { get { return _object; } set { _object = value; } }

        public override ExpressionType NodeType
        {
            get
            {
                return _nodeType;
            }
            set
            {
                _nodeType = value;
            }
        }

        public EditableMethodCallExpression()
        {

        }

        public EditableMethodCallExpression(EditableExpressionCollection arguments, MethodInfo method, EditableExpression theObject, ExpressionType nodeType)
        {
            _arguments = arguments;
            _method = method;
            _object = theObject;
            _nodeType = nodeType;
        }

        public EditableMethodCallExpression(IEnumerable<EditableExpression> arguments, MethodInfo method, Expression theObject, ExpressionType nodeType) :
            this(new EditableExpressionCollection(arguments), method, EditableExpression.CreateEditableExpression(theObject), nodeType)
        { }
        
        public EditableMethodCallExpression(MethodCallExpression callEx) :
            this(new EditableExpressionCollection(callEx.Arguments),callEx.Method,EditableExpression.CreateEditableExpression(callEx.Object),callEx.NodeType)
        { }

        public override Expression ToExpression()
        {
            return Expression.Call(_object.ToExpression(), _method, _arguments.GetExpressions().ToArray<Expression>());
        }
    }
}
