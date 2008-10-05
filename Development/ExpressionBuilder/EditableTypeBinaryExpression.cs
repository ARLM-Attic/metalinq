using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace ExpressionBuilder
{
    public class EditableTypeBinaryExpression : EditableExpression
    {
        protected EditableExpression _expression;
        protected Type _typeOperand;

        public EditableExpression Expression { get { return _expression; } set { _expression = value; } }
        public Type TypeOperand { get { return _typeOperand; } set { _typeOperand = value; } }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.TypeIs;
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public EditableTypeBinaryExpression(TypeBinaryExpression typeBinEx) :
            this( EditableExpression.CreateEditableExpression(typeBinEx.Expression),typeBinEx.TypeOperand)
        { }

        public EditableTypeBinaryExpression(EditableExpression expression, Type typeOperand)
        {
            _expression = expression;
            _typeOperand = typeOperand;
        }

        public override Expression ToExpression()
        {
            return System.Linq.Expressions.Expression.TypeIs(_expression.ToExpression(), _typeOperand);
        }
    }
}
