using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace ExpressionBuilder
{
    [DataContract]
    public class EditableParameterExpression : EditableExpression
    {
        private static Dictionary<string, ParameterExpression> _usableParameters =
            new Dictionary<string, ParameterExpression>();
       
        protected string _name;

        public Type Type { get { return _type; } set { _type = value; } }
        [DataMember]
        public string Name { get { return _name; } set { _name = value; } }

        public EditableParameterExpression()
        {

        }       

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Parameter;
            }
            set
            {
                // throw new Exception("The method or operation is not implemented.");
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

        static public ParameterExpression CreateParameter(Type type, string name)
        {
            ParameterExpression parameter = null;
            string key = type.AssemblyQualifiedName + Environment.NewLine + name;
            if (_usableParameters.ContainsKey(key))
            {
                parameter = _usableParameters[key] as ParameterExpression;
            }
            else
            {
                parameter = Expression.Parameter(type, name);
                _usableParameters.Add(key, parameter);
            }
            return parameter;
        }

        public override Expression ToExpression()
        {
            return CreateParameter(_type, _name); 
        }
    }
}
