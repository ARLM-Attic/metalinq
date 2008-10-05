using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace ExpressionBuilder
{
    class EditableInvocationExpression : EditableExpression
    {
        protected EditableExpression _expression;
        protected EditableExpressionCollection _arguments = new EditableExpressionCollection();

        public EditableExpression Expression
        {
            get { return _expression; }
            set { _expression = value; }
        }

        public EditableExpressionCollection Arguments
        {
            get { return _arguments; }
        }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Invoke;
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public EditableInvocationExpression(InvocationExpression invocEx)
        {
            _expression = EditableExpression.CreateEditableExpression(invocEx.Expression);
            foreach (Expression ex in invocEx.Arguments)
                _arguments.Add(EditableExpression.CreateEditableExpression(ex));
        }

        public override Expression ToExpression()
        {
            return System.Linq.Expressions.Expression.Invoke(_expression.ToExpression(), _arguments.GetExpressions());
        }
    }
}
