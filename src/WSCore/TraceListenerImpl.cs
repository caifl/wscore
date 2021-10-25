using System;

namespace WSCore
{
    class TraceListenerImpl : System.Diagnostics.TraceListener
    {
        public override void Write(string message)
        {
            Console.Write(message);
        }

        public override void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
