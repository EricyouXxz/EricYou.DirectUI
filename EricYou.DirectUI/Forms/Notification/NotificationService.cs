using System;
using System.Collections.Generic;
using System.Text;

namespace EricYou.DirectUI.Forms.Notification
{
    //通知处理委托类型
    public delegate void NotifyEventHandler(object message);

    /// <summary>
    /// DUI界面通知消息处理服务类
    /// </summary>
    public static class NotificationService
    {
        /// <summary>
        /// 通知处理器字典
        /// </summary>
        private static IDictionary<string, NotifyEventHandler> _notifyEventHandlerDict
            = new Dictionary<string, NotifyEventHandler>();

        /// <summary>
        /// 向界面通知消息处理服务注册通知消息处理器
        /// </summary>
        /// <param name="eventName">消息名</param>
        /// <param name="handler">消息处理器方法实例</param>
        public static void RegisterNotifyEventHandler(string eventName, NotifyEventHandler handler)
        {
            if(NotificationService._notifyEventHandlerDict.ContainsKey(eventName))
            {
                NotificationService._notifyEventHandlerDict[eventName] += handler;
            }
            else
            {
                NotificationService._notifyEventHandlerDict.Add(eventName, handler);
            }
        }

        /// <summary>
        /// 向界面通知消息处理服务注销通知消息处理器
        /// </summary>
        /// <param name="eventName">消息名</param>
        /// <param name="handler">要注销的消息处理器方法实例</param>
        public static void DeregisterNotifyEventHandler(string eventName, NotifyEventHandler handler)
        {
            if (NotificationService._notifyEventHandlerDict.ContainsKey(eventName))
            {
                NotificationService._notifyEventHandlerDict[eventName] -= handler;
            }
        }

        /// <summary>
        /// 消息通知方法
        /// </summary>
        /// <param name="eventName">消息名</param>
        /// <param name="message">消息数据</param>
        public static void Notify(string eventName, object message)
        {
            if (NotificationService._notifyEventHandlerDict.ContainsKey(eventName))
            {
                NotifyEventHandler handler = NotificationService._notifyEventHandlerDict[eventName];
                if (handler != null)
                {
                    handler(message);
                }
            }
        }
    }
}
