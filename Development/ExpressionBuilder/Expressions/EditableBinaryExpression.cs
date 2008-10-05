using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace ExpressionBuilder
{
    [DataContract]
    public class EditableBinaryExpression : EditableExpression
    {
        EditableExpression _left;
        EditableExpression _right;
        ExpressionType _nodeType;

        [DataMember]
        public EditableExpression Left { get { return _left; } set { _left = value; } }
        [DataMember]
        public EditableExpression Right { get { return _right; } set { _right = value; } }

        public EditableBinaryExpression()
        {

        }

        public EditableBinaryExpression(BinaryExpression binex)
        {
            _left = EditableExpression.CreateEditableExpression(binex.Left);
            _right = EditableExpression.CreateEditableExpression(binex.Right);
            _nodeType = binex.NodeType;
        }

        public override Expression ToExpression()
        {
            return Expression.MakeBinary(_nodeType, _left.ToExpression(), _right.ToExpression());
        }

        [DataMember]
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
    }
}
