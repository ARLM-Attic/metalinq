using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace ExpressionBuilder
{
    public class EditableUnaryExpression : EditableExpression
    {
        protected ExpressionType _nodeType;
        protected EditableExpression _operand;
        protected Type _type;

        public EditableExpression Operand { get { return _operand; } set { _operand = value; } }
        public Type Type { get { return _type; } set { _type = value; } }

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

        public EditableUnaryExpression(ExpressionType nodeType, EditableExpression operand, Type type)
        {
            _nodeType = nodeType;
            _operand = operand;
            _type = type;
        }

        public EditableUnaryExpression(UnaryExpression unEx) :
            this(unEx.NodeType,EditableExpression.CreateEditableExpression(unEx.Operand),unEx.Type)
        { }

        public override Expression ToExpression()
        {
            return Expression.MakeUnary(_nodeType, _operand.ToExpression(), _type);
        }
    }
}
