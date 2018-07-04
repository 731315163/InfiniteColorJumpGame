using System;
using System.Linq;
using GDGeek;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.HandShankAdapation.Messenger
{
    public class MessageManager : Singleton<MessageManager>
    {
        void Awake()
        {
            
        }
        public void RemoveListener(string eventType)
        {
            MessengerInternal.eventTable.Remove(eventType);
        }
        #region noneparametr
        public void AddListener(string eventType, Action handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

        public void AddListener<TReturn>(string eventType, Func<TReturn> handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

        public void RemoveListener(string eventType, Action handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

        public void RemoveListener<TReturn>(string eventType, Func<TReturn> handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

        public void Broadcast(string eventType)
        {
            Broadcast(eventType, mode: MessengerMode.DONT_REQUIRE_LISTENER);
        }

        public void Broadcast<TReturn>(string eventType, Action<TReturn> returnCall)
        {
            Broadcast(eventType, returnCall, MessengerInternal.DEFAULT_MODE);
        }

        public void Broadcast(string eventType, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Action>(eventType);

            if(invocationList == null && mode == MessengerMode.DONT_REQUIRE_LISTENER)return;
            foreach (var callback in invocationList)
                callback.Invoke();
        }

         public void Broadcast<TReturn>(string eventType, Action<TReturn> returnCall, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Func<TReturn>>(eventType);

            if(invocationList == null && mode == MessengerMode.DONT_REQUIRE_LISTENER)return;
            foreach (var result in invocationList.Select(del => del.Invoke()).Cast<TReturn>())
            {
                returnCall.Invoke(result);
            }
        }
        #endregion

        #region oneparameter
        public void AddListener<T>(string eventType, Action<T> handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

        public void AddListener<T,TReturn>(string eventType, Func<T, TReturn> handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

        public void RemoveListener<T>(string eventType, Action<T> handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

        public void RemoveListener<T, TReturn>(string eventType, Func<T, TReturn> handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

        public void Broadcast<T>(string eventType, T arg1)
        {
            Broadcast(eventType, arg1, mode: MessengerMode.DONT_REQUIRE_LISTENER);
        }

        public void Broadcast<T,TReturn>(string eventType, T arg1, Action<TReturn> returnCall)
        {
            Broadcast(eventType, arg1, returnCall, MessengerMode.DONT_REQUIRE_LISTENER);
        }

        public void Broadcast<T>(string eventType, T arg1, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Action<T>>(eventType);
          
            if(invocationList == null && mode == MessengerMode.DONT_REQUIRE_LISTENER)return;
            
            foreach (var callback in invocationList)
                callback.Invoke(arg1);
        }

        public void Broadcast<T,TReturn>(string eventType, T arg1, Action<TReturn> returnCall, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Func<T, TReturn>>(eventType);
            
            if(invocationList == null && mode == MessengerMode.DONT_REQUIRE_LISTENER)return;
            
            foreach (var result in invocationList.Select(del => del.Invoke(arg1)).Cast<TReturn>())
            {
                returnCall.Invoke(result);
            }
        }

        #endregion

        #region twoparameter
        
         public void AddListener<T, U>(string eventType, Action<T, U> handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

         public void AddListener<T, U,TReturn>(string eventType, Func<T, U, TReturn> handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

         public void RemoveListener<T, U>(string eventType, Action<T, U> handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

         public void RemoveListener<T, U,TReturn>(string eventType, Func<T, U, TReturn> handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

         public void Broadcast<T, U>(string eventType, T arg1, U arg2)
        {
            Broadcast(eventType, arg1, arg2, MessengerMode.DONT_REQUIRE_LISTENER);
        }

         public void Broadcast<T, U,TReturn>(string eventType, T arg1, U arg2, Action<TReturn> returnCall)
        {
            Broadcast(eventType, arg1, arg2, returnCall, MessengerMode.DONT_REQUIRE_LISTENER);
        }

         public void Broadcast<T, U>(string eventType, T arg1, U arg2, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Action<T, U>>(eventType);

            if(invocationList == null && mode == MessengerMode.DONT_REQUIRE_LISTENER)return;
            foreach (var callback in invocationList)
                callback.Invoke(arg1, arg2);
        }

         public void Broadcast<T, U,TReturn>(string eventType, T arg1, U arg2, Action<TReturn> returnCall, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Func<T, U, TReturn>>(eventType);

            if(invocationList == null && mode == MessengerMode.DONT_REQUIRE_LISTENER)return;
            foreach (var result in invocationList.Select(del => del.Invoke(arg1, arg2)).Cast<TReturn>())
            {
                returnCall.Invoke(result);
            }
        }
    
        

        #endregion

        #region threeregion
         
         public void AddListener<T, U, V>(string eventType, Action<T, U, V> handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

         public void AddListener<T, U, V,TReturn>(string eventType, Func<T, U, V, TReturn> handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

         public void RemoveListener<T, U, V>(string eventType, Action<T, U, V> handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

         public void RemoveListener<T, U, V,TReturn>(string eventType, Func<T, U, V, TReturn> handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

         public void Broadcast<T, U, V>(string eventType, T arg1, U arg2, V arg3)
        {
            Broadcast(eventType, arg1, arg2, arg3, MessengerMode.DONT_REQUIRE_LISTENER);
        }

         public void Broadcast<T, U, V,TReturn>(string eventType, T arg1, U arg2, V arg3, Action<TReturn> returnCall)
        {
            Broadcast(eventType, arg1, arg2, arg3, returnCall, MessengerMode.DONT_REQUIRE_LISTENER);
        }

         public void Broadcast<T, U, V>(string eventType, T arg1, U arg2, V arg3, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Action<T, U, V>>(eventType);

            if(invocationList == null && mode == MessengerMode.DONT_REQUIRE_LISTENER)return;
            foreach (var callback in invocationList)
                callback.Invoke(arg1, arg2, arg3);
        }

         public void Broadcast<T, U, V,TReturn>(string eventType, T arg1, U arg2, V arg3, Action<TReturn> returnCall, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Func<T, U, V, TReturn>>(eventType);

            if(invocationList == null && mode == MessengerMode.DONT_REQUIRE_LISTENER)return;
            foreach (var result in invocationList.Select(del => del.Invoke(arg1, arg2, arg3)).Cast<TReturn>())
            {
                returnCall.Invoke(result);
            }
        }

        #endregion

        public void Clear()
        {
            MessengerInternal.eventTable.Clear();
        }
        /// <summary>
        /// 转换场景注册的事件会被清除
        /// </summary>
        /// <param name="scence"></param>
        /// <param name="mod"></param>
        void OnSceneLoaded(Scene scence, LoadSceneMode mod)
        {
            Debug.Log("Clear scene");
            Clear();
        }

        public void OnDestroy()
        {
            Clear();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}