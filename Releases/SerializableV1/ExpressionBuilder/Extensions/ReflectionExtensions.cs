﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ExpressionBuilder
{
    public static class ReflectionExtensions
    {
        public static string ToSerializableForm(this Type type)
        {
            return type.AssemblyQualifiedName;
        }

        public static Type FromSerializableForm(this Type type, string serializedValue)
        {
            return Type.GetType(serializedValue);
        }

        public static string ToSerializableForm(this MethodInfo method)
        {
            return method.DeclaringType.AssemblyQualifiedName + Environment.NewLine + method.ToString();
        }

        public static MethodInfo FromSerializableForm(this MethodInfo methodInfo, string serializedValue)
        {
            string[] fullName = serializedValue.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string name = fullName[1];
            MethodInfo method = (from m in Type.GetType(fullName[0]).GetMethods()
                                 where m.ToString() == name
                                 select m).First();
            return method;

        }

        public static string ToSerializableForm(this MemberInfo member)
        {
            return member.DeclaringType.AssemblyQualifiedName + Environment.NewLine + member.ToString();
        }

        public static MemberInfo FromSerializableForm(this MemberInfo memberInfo, string serializedValue)
        {
            string[] fullName = serializedValue.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string name = fullName[1];
            MemberInfo member = (from m in Type.GetType(fullName[0]).GetMembers()
                                     where m.ToString() == name
                                     select m).First();
            return member;

        }

        public static string ToSerializableForm(this ConstructorInfo obj)
        {
            if (obj == null)
                return null;
            else
                return obj.DeclaringType.AssemblyQualifiedName + Environment.NewLine + obj.ToString();
        }

        public static ConstructorInfo FromSerializableForm(this ConstructorInfo obj, string serializedValue)
        {
            if (serializedValue == null)
                return null;
            else
            {
                string[] fullName = serializedValue.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                string name = fullName[1];
                ConstructorInfo newObj = (from m in Type.GetType(fullName[0]).GetConstructors()
                                          where m.ToString() == name
                                          select m).First();
                return newObj;
            }
        }
    }
}
