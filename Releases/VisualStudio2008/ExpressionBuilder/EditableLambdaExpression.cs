﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace ExpressionBuilder
{
    public class EditableLambdaExpression : EditableExpression 
    {
        protected EditableExpression _body;
        protected EditableExpressionCollection _parameters = new EditableExpressionCollection();
        protected Type _type;

        public EditableExpression Body { get { return _body; } set { _body = value; } }
        public EditableExpressionCollection Parameters { get { return _parameters; } }
        
        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Lambda;
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
        public EditableLambdaExpression(LambdaExpression lambEx)
        {
            _body = EditableExpression.CreateEditableExpression(lambEx.Body);
            _type = lambEx.Type;
            foreach (ParameterExpression param in lambEx.Parameters)
                _parameters.Add(EditableExpression.CreateEditableExpression(param));
        }
        public override Expression ToExpression()
        {
            Expression body = _body.ToExpression();
            List<ParameterExpression> parameters = new List<ParameterExpression>(_parameters.GetParameterExpressions());

            var bodyParameters = from edX in body.Nodes()
                             where edX is ParameterExpression
                             select edX;
            for (int i = 0; i < parameters.Count; i++)
            {
                var matchingParm = from parm in bodyParameters
                                   where (parm as ParameterExpression).Name == parameters[i].Name
                                      && (parm as ParameterExpression).Type == parameters[i].Type
                                   select parm as ParameterExpression;
                if (matchingParm.Count<ParameterExpression>() == 1) 
                    parameters[i] = matchingParm.First<ParameterExpression>() as ParameterExpression;
            }
            
            
            return Expression.Lambda(_type, body, parameters);

        }
    }
}
