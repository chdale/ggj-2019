using System;
using Spine.Unity;

namespace Assets.Hatch.Scripts
{
    [Serializable]
    public class SpineEventKey
    {
        [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)]
        public string spineEvent;
    }
}