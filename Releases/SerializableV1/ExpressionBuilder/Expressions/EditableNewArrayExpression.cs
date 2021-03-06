﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace ExpressionBuilder
{
    [DataContract]
    public class EditableNewArrayExpression : EditableExpression
    {
        protected EditableExpressionCollection _expressions = new EditableExpressionCollection();        
        protected ExpressionType _nodeType;

        [DataMember]
        public EditableExpressionCollection Expressions { get { return _expressions; } set { _expressions = value; } }

        [DataMember]
        public override ExpressionType NodeType
        {
            get
            {
                return _nodeType;
            }
            set
            {
                if (value == ExpressionType.NewArrayInit || value == ExpressionType.NewArrayBounds)
                    _nodeType = value;
                else
                    throw new InvalidOperationException("NodeType for NewArrayExpression must be ExpressionType.NewArrayInit or ExpressionType.NewArrayBounds");
            }
        }      

        public EditableNewArrayExpression()
        {

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
                return Expression.NewArrayBounds(_type.GetElementType(), _expressions.GetExpressions());
            else if (_nodeType == ExpressionType.NewArrayInit)
                return Expression.NewArrayInit(_type.GetElementType(), _expressions.GetExpressions());
            else
                throw new InvalidOperationException("NodeType for NewArrayExpression must be ExpressionType.NewArrayInit or ExpressionType.NewArrayBounds");
        }
    }
}
