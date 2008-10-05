using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace ExpressionBuilder
{
    public abstract class EditableExpression
    {
        public abstract ExpressionType NodeType { get; set; }
        public abstract Expression ToExpression();

        public EditableExpression() { } //allow for non parameterized creation for all expressions

        public static EditableExpression CreateEditableExpression<TResult>(Expression<Func<TResult>> ex)
        {
            LambdaExpression lambEx = Expression.Lambda<Func<TResult>>(ex.Body, ex.Parameters);
            return new EditableLambdaExpression(lambEx);
        }

        public static EditableExpression CreateEditableExpression<TArg0, TResult>(Expression<Func<TArg0, TResult>> ex)
        {
            LambdaExpression lambEx = Expression.Lambda<Func<TArg0, TResult>>(ex.Body, ex.Parameters);
            return new EditableLambdaExpression(lambEx);
        }


        public static EditableExpression CreateEditableExpression<TArg0, TArg1, TResult>(Expression<Func<TArg0, TArg1, TResult>> ex)
        {
            LambdaExpression lambEx = Expression.Lambda<Func<TArg0, TArg1, TResult>>(ex.Body, ex.Parameters);
            return new EditableLambdaExpression(lambEx);
        }


        public static EditableExpression CreateEditableExpression<TArg0, TArg1, TArg2, TResult>(Expression<Func<TArg0, TArg1, TArg2, TResult>> ex)
        {
            LambdaExpression lambEx = Expression.Lambda<Func<TArg0, TArg1, TArg2, TResult>>(ex.Body, ex.Parameters);
            return new EditableLambdaExpression(lambEx);
        }


        public static EditableExpression CreateEditableExpression<TArg0, TArg1, TArg2, TArg3, TResult>(Expression<Func<TArg0, TArg1, TArg2, TArg3, TResult>> ex)
        {
            LambdaExpression lambEx = Expression.Lambda<Func<TArg0, TArg1, TArg2, TArg3, TResult>>(ex.Body, ex.Parameters);
            return new EditableLambdaExpression(lambEx);
        }

        public static EditableExpression CreateEditableExpression(Expression ex)
        {
            if (ex is ConstantExpression) return new EditableConstantExpression(ex as ConstantExpression);
            else if (ex is BinaryExpression) return new EditableBinaryExpression(ex as BinaryExpression);
            else if (ex is ConditionalExpression) return new EditableConditionalExpression(ex as ConditionalExpression);
            else if (ex is InvocationExpression) return new EditableInvocationExpression(ex as InvocationExpression);
            else if (ex is LambdaExpression) return new EditableLambdaExpression(ex as LambdaExpression);
            else if (ex is ParameterExpression) return new EditableParameterExpression(ex as ParameterExpression);
            else if (ex is ListInitExpression) return new EditableListInitExpression(ex as ListInitExpression);
            else if (ex is MemberExpression) return new EditableMemberExpression(ex as MemberExpression);
            else if (ex is MemberInitExpression) return new EditableMemberInitExpression(ex as MemberInitExpression);
            else if (ex is MethodCallExpression) return new EditableMethodCallExpression(ex as MethodCallExpression);
            else if (ex is NewArrayExpression) return new EditableNewArrayExpression(ex as NewArrayExpression);
            else if (ex is NewExpression) return new EditableNewExpression(ex as NewExpression);
            else if (ex is TypeBinaryExpression) return new EditableTypeBinaryExpression(ex as TypeBinaryExpression);
            else if (ex is UnaryExpression) return new EditableUnaryExpression(ex as UnaryExpression);
            else return null;
        }
    }

    public static class EditableExpressionExtension
    {
        public static IEnumerable<EditableExpression> LinkNodes(this EditableExpression source)
        {
            //returns all the "paths" or "links" from a node in an expression tree
            //  each expression type that has "links" from it has different kinds of links
            if (source is EditableLambdaExpression)
            {
                yield return (source as EditableLambdaExpression).Body;
                foreach (EditableParameterExpression parm in (source as EditableLambdaExpression).Parameters)
                    yield return parm;
            }
            else if (source is EditableBinaryExpression)
            {
                yield return (source as EditableBinaryExpression).Left;
                yield return (source as EditableBinaryExpression).Right;
            }
            else if (source is EditableConditionalExpression)
            {
                yield return (source as EditableConditionalExpression).IfTrue;
                yield return (source as EditableConditionalExpression).IfFalse;
                yield return (source as EditableConditionalExpression).Test;
            }
            else if (source is EditableInvocationExpression)
                foreach (EditableExpression x in (source as EditableInvocationExpression).Arguments)
                    yield return x;
            else if (source is EditableListInitExpression)
            {
                yield return (source as EditableListInitExpression).NewExpression;
                foreach (EditableExpression x in (source as EditableListInitExpression).Expressions)
                    yield return x;
            }
            else if (source is EditableMemberExpression)
                yield return (source as EditableMemberExpression).Expression;
            else if (source is EditableMemberInitExpression)
                yield return (source as EditableMemberInitExpression).NewExpression;
            else if (source is EditableMethodCallExpression)
            {
                foreach (EditableExpression x in (source as EditableMethodCallExpression).Arguments)
                    yield return x;
                yield return (source as EditableMethodCallExpression).Object;
            }
            else if (source is EditableNewArrayExpression)
                foreach (EditableExpression x in (source as EditableNewArrayExpression).Expressions)
                    yield return x;
            else if (source is EditableNewExpression)
                foreach (EditableExpression x in (source as EditableNewExpression).Arguments)
                    yield return x;
            else if (source is EditableTypeBinaryExpression)
                yield return (source as EditableTypeBinaryExpression).Expression;
            else if (source is EditableUnaryExpression)
                yield return (source as EditableUnaryExpression).Operand;
        }

        //return all the nodes in a given expression tree
        public static IEnumerable<EditableExpression> Nodes(this EditableExpression source)
        {
            //i.e. left, right, body, etc.
            foreach (EditableExpression linkNode in source.LinkNodes())
                //recursive call to get the nodes from the tree, until you hit terminals
                foreach (EditableExpression subNode in linkNode.Nodes())
                    yield return subNode;
            yield return source; //return the root of this most recent call
        }
    }

    public static class ExpressionExtension
    {
        public static IEnumerable<Expression> LinkNodes(this Expression source)
        {
            //returns all the "paths" or "links" from a node in an expression tree
            //  each expression type that has "links" from it has different kinds of links
            if (source is LambdaExpression)
            {
                yield return (source as LambdaExpression).Body;
                foreach (ParameterExpression parm in (source as LambdaExpression).Parameters)
                    yield return parm;
            }
            else if (source is BinaryExpression)
            {
                yield return (source as BinaryExpression).Left;
                yield return (source as BinaryExpression).Right;
            }
            else if (source is ConditionalExpression)
            {
                yield return (source as ConditionalExpression).IfTrue;
                yield return (source as ConditionalExpression).IfFalse;
                yield return (source as ConditionalExpression).Test;
            }
            else if (source is InvocationExpression)
                foreach (Expression x in (source as InvocationExpression).Arguments)
                    yield return x;
            else if (source is ListInitExpression)
            {
                yield return (source as ListInitExpression).NewExpression;
            }
            else if (source is MemberExpression)
                yield return (source as MemberExpression).Expression;
            else if (source is MemberInitExpression)
                yield return (source as MemberInitExpression).NewExpression;
            else if (source is MethodCallExpression)
            {
                foreach (Expression x in (source as MethodCallExpression).Arguments)
                    yield return x;
                yield return (source as MethodCallExpression).Object;
            }
            else if (source is NewArrayExpression)
                foreach (Expression x in (source as NewArrayExpression).Expressions)
                    yield return x;
            else if (source is NewExpression)
                foreach (Expression x in (source as NewExpression).Arguments)
                    yield return x;
            else if (source is TypeBinaryExpression)
                yield return (source as TypeBinaryExpression).Expression;
            else if (source is UnaryExpression)
                yield return (source as UnaryExpression).Operand;
        }

        //return all the nodes in a given expression tree
        public static IEnumerable<Expression> Nodes(this Expression source)
        {
            //i.e. left, right, body, etc.
            foreach (Expression linkNode in source.LinkNodes())
                //recursive call to get the nodes from the tree, until you hit terminals
                foreach (Expression subNode in linkNode.Nodes())
                    yield return subNode;
            yield return source; //return the root of this most recent call
        }
    }
}
