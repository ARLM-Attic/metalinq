using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace ExpressionBuilder
{
    [DataContract]
    [KnownType(typeof(EditableMemberExpression))]
    [KnownType(typeof(EditableListInitExpression))]
    [KnownType(typeof(EditableNewExpression))]
    [KnownType(typeof(EditableNewArrayExpression))]
    [KnownType(typeof(EditableTypeBinaryExpression))]
    [KnownType(typeof(EditableMemberInitExpression))]
    [KnownType(typeof(EditableInvocationExpression))]
    [KnownType(typeof(EditableBinaryExpression))]
    [KnownType(typeof(EditableParameterExpression))]
    [KnownType(typeof(EditableExpressionCollection))]
    [KnownType(typeof(EditableConstantExpression))]
    [KnownType(typeof(EditableConditionalExpression))]
    [KnownType(typeof(EditableUnaryExpression))]
    [KnownType(typeof(EditableMethodCallExpression))]
    public abstract class EditableExpression
    {
        [DataMember]
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
}
