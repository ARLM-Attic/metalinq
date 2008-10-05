using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionBuilder
{
    public class EditableMethodCallExpression : EditableExpression
    {
        protected EditableExpressionCollection _arguments;
        protected MethodInfo _method;
        protected EditableExpression _object;
        protected readonly ExpressionType _nodeType;

        public EditableExpressionCollection Arguments { get { return _arguments; } }
        public MethodInfo Method { get { return _method; } set { _method = value; } }
        public EditableExpression Object { get { return _object; } set { _object = value; } }

        public override ExpressionType NodeType
        {
            get
            {
                return _nodeType;
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
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
