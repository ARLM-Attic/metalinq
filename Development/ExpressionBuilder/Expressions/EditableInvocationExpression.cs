using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace ExpressionBuilder
{
    [DataContract]
    public class EditableInvocationExpression : EditableExpression
    {
        protected EditableExpression _expression;
        protected EditableExpressionCollection _arguments = new EditableExpressionCollection();

        [DataMember]
        public EditableExpression Expression
        {
            get { return _expression; }
            set { _expression = value; }
        }

        [DataMember]
        public EditableExpressionCollection Arguments
        {
            get { return _arguments; }
            set { _arguments = value; }
        }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Invoke;
            }
            set
            {
                //throw new Exception("The method or operation is not implemented.");
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
