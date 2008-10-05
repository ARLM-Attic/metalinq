using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace ExpressionBuilder
{
    public class EditableNewArrayExpression : EditableExpression
    {
        protected EditableExpressionCollection _expressions = new EditableExpressionCollection();
        protected Type _type;
        protected ExpressionType _nodeType;

        public EditableExpressionCollection Expressions { get { return _expressions; } }
        public Type Type { get { return _type; } set { _type = value; } }

        public override ExpressionType NodeType
        {
            get
            {
                return _nodeType;
            }
            set
            {
                if (_nodeType == ExpressionType.NewArrayInit || _nodeType == ExpressionType.NewArrayBounds)
                    _nodeType = value;
                else
                    throw new InvalidOperationException("NodeType for NewArrayExpression must be ExpressionType.NewArrayInit or ExpressionType.NewArrayBounds");
            }
        }

        public EditableNewArrayExpression(NewArrayExpression newEx) :
            this(new EditableExpressionCollection(newEx.Expressions),newEx.NodeType,newEx.Type)
        { }

        public EditableNewArrayExpression(IEnumerable<EditableExpression> expressions, ExpressionType nodeType, Type type) :
            this(new EditableExpressionCollection(expressions), nodeType, type)
        { }

        public EditableNewArrayExpression(EditableExpressionCollection expressions, ExpressionType nodeType, Type type)
        {
            _expressions = expressions;
            _nodeType = nodeType;
            _type = type;
        }

        public override Expression ToExpression()
        {
            if (_nodeType == ExpressionType.NewArrayBounds)
                return Expression.NewArrayBounds(_type, _expressions.GetExpressions());
            else if (_nodeType == ExpressionType.NewArrayInit)
                return Expression.NewArrayInit(_type, _expressions.GetExpressions());
            else
                throw new InvalidOperationException("NodeType for NewArrayExpression must be ExpressionType.NewArrayInit or ExpressionType.NewArrayBounds");
        }
    }
}
