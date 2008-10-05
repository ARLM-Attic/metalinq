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
        protected EditableElementInitCollection _initializers = new EditableElementInitCollection();
        protected EditableExpression _new;

        [DataMember]
        public EditableExpression NewExpression { get { return _new; } set { _new = value; } }
        [DataMember]
        public EditableElementInitCollection Initializers { get { return _initializers; } set { _initializers = value; } }
        
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
            foreach (ElementInit e in listInit.Initializers)
            {
                _initializers.Add(new EditableElementInit(e));
            }
        }

        public EditableListInitExpression(EditableExpression newEx, IEnumerable<EditableElementInit> initializers)
        {
            _new = newEx;
            foreach (EditableElementInit ex in initializers)
                _initializers.Add(ex);
        }

        public override Expression ToExpression()
        {
            return Expression.ListInit(_new.ToExpression() as NewExpression, _initializers.GetElementsInit().ToArray<ElementInit>());
        }
    }
}
