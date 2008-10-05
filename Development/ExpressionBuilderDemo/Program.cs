using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using ExpressionBuilder;
using System.Linq.Expressions;

namespace ExpressionBuilderDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //make a lambda
            Expression<Func<int, int>> lambda = x => x + 1;
            //create an editable version of the lambda
            EditableExpression mutableLambda = EditableExpression.CreateEditableExpression(lambda);
            //make the adder from a version of the editable lambda in it's virgin state
            LambdaExpression adder = mutableLambda.ToExpression() as LambdaExpression;
            //Do Linq over Expressions :) - find all the binary expressions
            var editableBinaryExpressions = from x in mutableLambda.Nodes()
                                            where x is EditableBinaryExpression
                                            select x;
            //Change all the binary expressions to do subtraction
            foreach (EditableBinaryExpression x in editableBinaryExpressions)
                x.NodeType = ExpressionType.Subtract;
            //Now, make a subtractor, since we have changed the expression
            LambdaExpression subtractor = mutableLambda.ToExpression() as LambdaExpression;

            //Ok, now the test
            Console.WriteLine("Pick a number, any number:");
            string stringInput = Console.ReadLine();
            int input;
            if (int.TryParse(stringInput,out input))
            {
                //get and display the subtraction result
                int subResult = (int) subtractor.Compile().DynamicInvoke(input);
                Console.WriteLine("Subtractor result : "  + subResult);
                //get and display the add result
                int addResult = (int) adder.Compile().DynamicInvoke(input);
                Console.WriteLine("Adder result : " + addResult);
            }
            //keep console open so tester can validate results
            Console.WriteLine("Demonstration complete.  Press any key to continue.");
            Console.ReadKey();

        }
    }
}
