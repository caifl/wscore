using System.Xml;

namespace System.Diagnostics
{
    class SourceElement
    {
        public virtual string Name
        {
            get;
            set;
        }

        public virtual string SwitchName
        {
            get;
            set;
        }

        public virtual string SwitchType
        {
            get;
            set;
        }

        public virtual string SwitchValue
        {
            get;
            set;
        }

        public virtual SourceListenerSettings Listeners
        {
            get;
            set;
        }

        //        Name A read/write string value that specifies the name of the trace source.
        //SwitchName  A read/write string value that specifies the display name of a trace switch instance in the application.
        //SwitchType A read/write string value that specifies the type of the trace switch. The type must be a valid class name and cannot be an empty string.
        //SwitchValue A read/write string value that specifies a trace source-specific attribute.
    }
}
