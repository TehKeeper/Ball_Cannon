using UnityEngine;

namespace utilities.tools.mono
{
    public abstract class MonoBehaviourExt<T> : MonoBehaviour where T : Transform
    {
        protected T tf;

        private void Awake()
        {
            tf = DefineTransform();
            MakeInit();
        }

        protected abstract dynamic DefineTransform();

        protected abstract void MakeInit();
    }
}