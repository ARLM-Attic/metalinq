﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace ExpressionBuilder
{
    [DataContract]
    public class EditableConditionalExpression : EditableExpression
    {
        protected ExpressionType _nodeType;
        protected EditableExpression _test;
        protected EditableExpression _ifTrue;
        protected EditableExpression _ifFalse;

        public EditableConditionalExpression()
        {

        }

        public EditableConditionalExpression(ConditionalExpression condEx)
        {
            _nodeType = condEx.NodeType;
            _test = EditableExpression.CreateEditableExpression(condEx.Test);
            _ifTrue = EditableExpression.CreateEditableExpression(condEx.IfTrue);
            _ifFalse = EditableExpression.CreateEditableExpression(condEx.IfFalse);
        }

        public EditableConditionalExpression(ExpressionType nodeType, EditableExpression test, EditableExpression ifTrue, EditableExpression ifFalse)
        {
            _nodeType = nodeType;
            _test = test;
            _ifTrue = ifTrue;
            _ifFalse = ifFalse;
        }

        [DataMember]
        public EditableExpression Test { get { return _test; } set { _test = value; } }
        [DataMember]
        public EditableExpression IfTrue { get { return _ifTrue; } set { _ifTrue = value; } }
        [DataMember]
        public EditableExpression IfFalse { get { return _ifFalse; } set { _ifFalse = value; } }
        [DataMember]
        public override ExpressionType NodeType { get { return _nodeType; } set { _nodeType = value; } }

        public override Expression ToExpression()
        {
            return Expression.Condition(_test.ToExpression(),_ifTrue.ToExpression(),_ifFalse.ToExpression());
        }
    }
}
