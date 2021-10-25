using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Web.Util
{
    public delegate void WorkItemCallback();

    static class WorkItem
    {
        static WaitCallback waitCallback = new WaitCallback(OnWorkItemCompletion);

        static void OnWorkItemCompletion(object state)
        {
            var callback = state as WorkItemCallback;
            if (callback != null)
                callback();
        }

        public static void Post(WorkItemCallback workItem)
        {
            ThreadPool.QueueUserWorkItem(waitCallback, workItem);
        }
    }
}
