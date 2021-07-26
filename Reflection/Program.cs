using System;
using System.Linq;
using System.Reflection;
using TestLibrary;

namespace Reflection
{
    class Program
    {
        static void Main()
        {
            var assembly = Assembly.Load("TestLibrary");
            var s = assembly.GetTypes();
            var type = assembly.GetType("TestLibrary.IWebClient");
            var types = assembly.GetTypes().Where(r => type.IsAssignableFrom(r) && type != r).ToList();

            foreach (var findedType in types)
            {
                Console.WriteLine(findedType.FullName);
            }

            var instance = (IWebClient)Activator.CreateInstance(types[0]);

            instance.StartDownload("", res => { Console.WriteLine(res.IsCancelled); });

            var attributeType = assembly.GetType("TestLibrary.TestAttribute");

            var methods = assembly.GetType("TestLibrary.TestClass").GetMethods()
                .Where(x => x.GetCustomAttributes(attributeType).FirstOrDefault() is not null);

            foreach (var method in methods)
            {
                var obj = Activator.CreateInstance(method.DeclaringType);
                method.Invoke(obj, null);
            }
        }
    }
}
