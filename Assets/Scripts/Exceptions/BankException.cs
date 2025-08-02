using System;
using System.Reflection;

namespace Exceptions
{
    public class BankException : Exception
    {
        private const string BankNullValueMessage = ": Some value in factory bank has null value";
        private const string BankNotUniqueKeyMessage = ": Bank key is not unique ";

        private BankException(MemberInfo type, string message) => throw new Exception(type.Name + message);

        public static BankException BankNullValueException(Type type) => new(type, BankNullValueMessage);

        public static BankException BankNonUniqueKeyException(Type type) => new(type, BankNotUniqueKeyMessage);
    }
}
