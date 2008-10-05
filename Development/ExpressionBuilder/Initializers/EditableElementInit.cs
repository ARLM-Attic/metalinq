using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using System.Linq.Expressions;

namespace ExpressionBuilder
{
    [DataContract]
    public class EditableElementInit
    {
        protected MethodInfo _method;
        protected EditableExpressionCollection _arguments = new EditableExpressionCollection();

        [DataMember]
        public EditableExpressionCollection Arguments { get { return _arguments; } set { _arguments = value; } }

        public MethodInfo AddMethod
        {
            get 
            {
                return _method;
            }
            set
            {
                _method = value;
            }
        }

        [DataMember]
        public string AddMethodName
        {
            get
            {
                return _method.ToSerializableForm();
            }
            set
            {
                _method = _method.FromSerializableForm(value);
            }
        }

        public EditableElementInit()
        {

        }

        public EditableElementInit(ElementInit elmInit)
        {
            _method = elmInit.AddMethod;
            foreach (Expression ex in elmInit.Arguments)
            {
                _arguments.Add(EditableExpression.CreateEditableExpression(ex));
            }
        }

        public ElementInit ToElementInit()
        {
            return Expression.ElementInit(_method, _arguments.GetExpressions());
        }
    }
}
