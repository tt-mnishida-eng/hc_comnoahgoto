using System;
using UnityEngine;

namespace TT
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _mInstance;
        public static T I
        {
            get
            {
                if (_mInstance == null)
                {
                    Type t = typeof(T);
                    _mInstance = (T)FindObjectOfType(t);
                    if (_mInstance == null)
                    {
                        Debug.LogError(t + " をアタッチしているGameObjectはありません");
                        #if UNITY_EDITOR
                        GameObject go = new GameObject(typeof(T).ToString());
                        _mInstance = go.AddComponent<T>();
                        #endif
                    }
                }
                return _mInstance;
            }
        }

        virtual protected void Awake ()
        {
            if (this != I)
            {
                Destroy(this);
                Debug.LogError(
                    typeof(T) +
                    " は既に他のGameObjectにアタッチされているため、コンポーネントを破棄しました." +
                    " アタッチされているGameObjectは " + I.gameObject.name + " です.");
                return;
            }
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public class Singleton<T> where T : class, new()
    {
        private static readonly T _mInstance = new T();
        protected Singleton()
        {
            Debug.Assert( null == _mInstance );
        }
        public static T I {
            get {
                return _mInstance;
            }
        }
    }
}
