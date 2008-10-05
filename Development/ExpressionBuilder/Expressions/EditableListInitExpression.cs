using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace ExpressionBuilder
{
    [DataContract]
    public class EditableListInitExpression : EditableExpression
    {
        protected EditableExpressionCollection _initializers = new EditableExpressionCollection();
        protected EditableExpression _new;

        [DataMember]
        public EditableExpression NewExpression { get { return _new; } set { _new = value; } }
        [DataMember]
        public EditableExpressionCollection Expressions { get { return _initializers; } set { _initializers = value; } }
        
        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.ListInit;
            }
            set
            {
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        public EditableListInitExpression()
        {

        }

        public EditableListInitExpression(ListInitExpression listInit)
        {
            _new = EditableExpression.CreateEditableExpression(listInit.NewExpression);
        }

        public EditableListInitExpression(EditableExpression newEx, IEnumerable<EditableExpression> initializers)
        {
            _new = newEx;
            foreach (EditableExpression ex in initializers)
                _initializers.Add(ex);
        }

        public override Expression ToExpression()
        {
            return Expression.ListInit(_new.ToExpression() as NewExpression,_initializers.GetExpressions().ToArray<Expression>());
        }
    }
}
