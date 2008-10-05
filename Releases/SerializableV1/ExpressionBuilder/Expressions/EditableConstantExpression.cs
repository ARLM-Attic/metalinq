using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace ExpressionBuilder
{
    [DataContract]
    public class EditableConstantExpression : EditableExpression
    {
        protected object _value;
        protected ExpressionType _nodeType;

        [DataMember]
        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public EditableConstantExpression()
        {

        }

        public EditableConstantExpression(object value)
        {
            _value = value;
        }

        public EditableConstantExpression(ConstantExpression startConstEx)
        {
            _value = startConstEx.Value;
        }

        public override Expression ToExpression()
        {
            return Expression.Constant(_value) as Expression;
        }
        
        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Constant;
            }
            set
            {
                //throw new Exception("The method or operation is not implemented.");
            }
        }
    }
}
