using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Common
{
    public class Session
    {
        private static Session session = new Session();

        private Dictionary<string, object> kvs = new Dictionary<string, object>();
        private Session()
        {

        }

        public object this[string key]
        {
            get
            {
                return kvs[key];
            }
            set
            {
                kvs.Add(key,value);
            }
        }

        public int Count
        {
            get
            {
                return kvs.Count;
            }
        }

        public Dictionary<string, object>.KeyCollection Keys
        {
            get
            {
                return kvs.Keys;
            }
        }

        public Dictionary<string, object>.ValueCollection Values
        {
            get
            {
                return kvs.Values;
            }
        }

        public object this[Keys key]
        {
            get
            {
                return this[key.ToString()];
            }
            set
            {
                this[key.ToString()] = value;
            }
        }

        public T GetValue<T>(string key)
        {
            object value = this[key];
            if (value != null)
            {
                return (T)value;
            }
            return default(T);
        }

        public T GetValue<T>(Keys key)
        {
            return GetValue<T>(key.ToString());
        }

        public object GetValue(string key)
        {
            return GetValue<object>(key);
        }

        public object GetValue(Keys key)
        {
            return GetValue(key.ToString());
        }

        public void AddValue<T>(string key, T t)
        {
            this[key] = t;
        }

        public void AddValue<T>(Keys key, T t)
        {
            AddValue<T>(key.ToString(), t);
        }

        public void Remove(string key)
        {
            kvs.Remove(key);
        }

        public void Remove(Keys key)
        {
            kvs.Remove(key.ToString());
        }

        public bool ContainsKey(string key)
        {
            return kvs.ContainsKey(key);
        }

        public bool ContainsValue(string value)
        {
            return kvs.ContainsValue(value);
        }

        public static Session GetInstance()
        {
            return session;
        }
    }
}
