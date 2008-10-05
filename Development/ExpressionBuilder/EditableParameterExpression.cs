using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace ExpressionBuilder
{
    class EditableParameterExpression : EditableExpression
    {
        protected Type _type;
        protected string _name;

        public Type Type { get { return _type; } set { _type = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        
        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Parameter;
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public EditableParameterExpression(ParameterExpression parmEx)
            : this(parmEx.Type,parmEx.Name)
        { }

        public EditableParameterExpression(Type type, string name)
        {
            _type = type;
            _name = name;
        }

        public override Expression ToExpression()
        {
            return Expression.Parameter(_type,_name);
        }
    }
}
