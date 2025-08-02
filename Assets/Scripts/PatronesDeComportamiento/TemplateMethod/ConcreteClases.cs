using System;

namespace PatronesDeComportamiento.TemplateMethod
{
    public class ConcreteClassA : AbstractTemplate
    {
        protected override void RequiredOperations1() => Console.WriteLine("ConcreteClass1 says: Implemented Operation1");

        protected override void RequiredOperation2() => Console.WriteLine("ConcreteClass1 says: Implemented Operation2");
    }
    
    public class ConcreteClassB : AbstractTemplate
    {
        protected override void RequiredOperations1() => Console.WriteLine("ConcreteClass2 says: Implemented Operation1");

        protected override void RequiredOperation2() => Console.WriteLine("ConcreteClass2 says: Implemented Operation2");

        protected override void Hook2() => Console.WriteLine("ConcreteClass2 says: Overridden Hook1");
    }
}