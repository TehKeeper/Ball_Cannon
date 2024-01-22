using UnityEngine;

namespace utilities.tools.mono
{
    public abstract class MonoBehaviourObj : MonoBehaviourExt<Transform>
    {

        protected override dynamic DefineTransform() => GetComponent<Transform>();
    }
}