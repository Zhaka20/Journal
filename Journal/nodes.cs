using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Journal
{
    public class nodes
    {
        static void Main(string[] args)
        {
            var man = new Person
            {
                Age = 33,
                FirstName = "Nick",
                Pet = new Animal { Name = "Dog", Height = 13 }
            };

            var man2 = new Person
            {
                Age = 22,
                FirstName = "Bravo",
                Pet = new Animal { Name = "Cat", Height = 13 }
            };

            Expression<Func<Person, object>> expr1 = arg => arg.Pet;
            Expression<Func<Person, object>> expr2 = arg => arg.Pet.Name;
            var compareResult = ExpressionComparer.IsSameMember(expr1, expr2);
            Console.WriteLine(compareResult);
            Console.ReadLine();
        }
    }

    public static class ExpressionComparer
    {
        public static MemberExpression GetMemberExpression<T>(Expression<Func<T, object>> expr)
        {
            var member = expr.Body as MemberExpression;
            var unary = expr.Body as UnaryExpression;
            return member ?? (unary != null ? unary.Operand as MemberExpression : null);
        }

        public static bool IsSameMember<T>(this Expression<Func<T, object>> expr1, Expression<Func<T, object>> expr2)
        {
            var result1 = GetMemberExpression(expr1);
            var result2 = GetMemberExpression(expr2);

            if (result1 == null || result2 == null)
                return false;

            return result1.Member.Name == result2.Member.Name;
        }

        public static bool IsSameProperty<TSourceA, TSourceB, TPropertyA, TPropertyB>(
                                                                                       Expression<Func<TSourceA, TPropertyA>> expA,
                                                                                       Expression<Func<TSourceB, TPropertyB>> expB)
        {
            MemberExpression memExpA = expA.Body as MemberExpression;
            MemberExpression memExpB = expB.Body as MemberExpression;

            if (memExpA == null || memExpB == null)
                return false;

            PropertyInfo propA = memExpA.Member as PropertyInfo;
            PropertyInfo propB = memExpB.Member as PropertyInfo;

            if (propA == null || propB == null)
                return false;

            return propA.Equals(propB);
        }
    }



    public class Person
    {
        public string FirstName { get; set; }
        public int Age { get; set; }
        public Animal Pet { get; set; }
    }

    public class Animal
    {
        public string Name { get; set; }
        public int Height { get; set; }
    }
}
}