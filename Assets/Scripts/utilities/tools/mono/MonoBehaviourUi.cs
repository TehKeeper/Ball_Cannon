using UnityEngine;

namespace utilities.tools.mono
{
    public abstract class MonoBehaviourUi : MonoBehaviourExt<RectTransform>
    {
        protected override dynamic DefineTransform() => GetComponent<RectTransform>();
    }
}