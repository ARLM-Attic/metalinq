using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace ExpressionBuilder
{
    public class EditableBinaryExpression : EditableExpression
    {
        EditableExpression _left;
        EditableExpression _right;
        ExpressionType _nodeType;

        public EditableExpression Left { get { return _left; } set { _left = value; } }
        public EditableExpression Right { get { return _right; } set { _right = value; } }

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
