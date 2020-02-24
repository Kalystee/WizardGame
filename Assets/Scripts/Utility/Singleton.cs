using UnityEngine;

namespace Project.Utility
{
    /// <summary>
    /// To create a Singleton do : class XXX : Singleton<XXX> and it's done.
    /// It's cool because it avoid duplicate code.
    /// Moreover a Singleton is always a MonoBehaviour (Managers, Player...)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DisallowMultipleComponent]
    public class Singleton<T> : MonoBehaviour where T:MonoBehaviour
    {
        public static T Instance { get; private set; }
        public virtual void Awake()
        {
            if(Instance != null)
            {
                DestroyImmediate(this);
                return;
            }

            Instance = this as T;
            DontDestroyOnLoad(this);
        }
    }
}

