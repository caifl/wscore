using System.Collections.Generic;
using System.Xml;

namespace System.Diagnostics
{
    class TraceSettings
    {
        public bool AutoFlush
        {
            get;
            set;
        }

        public int IndentSize
        {
            get;
            set;
        }

        public TraceListenerSettings Listeners
        {
            get;
            set;
        }

        public bool UseGlobalLock
        {
            get;
            set;
        }

        public void Load(XmlElement element)
        {
            var value = element.GetAttribute("autoFlush");
            if (!string.IsNullOrEmpty(value))
                this.AutoFlush = bool.Parse(value);

            value = element.GetAttribute("indentSize");
            if (!string.IsNullOrEmpty(value))
                this.IndentSize = int.Parse(value);

            value = element.GetAttribute("useGlobalLock");
            if (!string.IsNullOrEmpty(value))
                this.UseGlobalLock = bool.Parse(value);

            var list = new List<ListenerElement>();

            var children = element.ChildNodes;
            foreach (XmlElement source in children)
            {
                if (!source.LocalName.Equals("listeners"))
                    continue;

                this.Listeners = new TraceListenerSettings();
                this.Listeners.Load(source);
                break;
            }
        }

        /*
        AutoFlush A read/write boolean value. true if the trace listeners automatically flush the output buffer after every write operation; otherwise, false. The default is false. Note: When the AutoFlush property is set to true, the trace listener writes to the file regardless of whether the System.Diagnostics.Trace.Flush method is called.
IndentSize A read/write sint32 value that specifies the number of spaces to indent when the System.Diagnostics.Trace.Indent method is called.The default is 4. Note: A System.Diagnostics.TextWriterTraceListener interprets this number as spaces.An EventLogTraceListener ignores this value.This property is stored on per-thread, per-request basis.
Listeners A TraceListenerSettings value that contains listeners that monitor and format trace output.
UseGlobalLock A read/write boolean value. true if the global lock will be used; otherwise, false. The default is true. Note: The global lock is always used if the trace listener is not thread safe, regardless of the value of UseGlobalLock.The System.Diagnostics.TraceListener.IsThreadSafe property determines whether the listener is thread safe. The global lock is not used only if the value of UseGlobalLock is false and the value of IsThreadSafe is true. The default behavior is to use the global lock.
    */
    }
}
