using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace MyRealProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            ICalculator calculator = Proxy<ICalculator>.Create();
            var result2 = calculator.Add(2, 2);
        }
    }

    public class Proxy<T> : RealProxy
    {
        private Proxy(): base(typeof(T))
        {
          
        }

        public static T Create()
        {
            var advice = new Proxy<T>();

            return (T)advice.GetTransparentProxy();
        }

        public override IMessage Invoke(IMessage msg)
        {
            var type = typeof(T);
            var methodCall = msg as IMethodCallMessage;
            var args = methodCall.Args;

            return new ReturnMessage((int)100, args, args.Length,  methodCall.LogicalCallContext, methodCall);
        }
    }

    public interface ICalculator
    {
        int Add(int a, int b);
        int Subtract(int a, int b);
    }
}
