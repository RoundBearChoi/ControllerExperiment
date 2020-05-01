using UnityEngine;

namespace ControllerExperiment.SubComponents
{
    public abstract class SubComponent : MonoBehaviour
    {
        [Header("SubComponent Debug")]
        public bool DoFixedUpdate;
        public bool DoUpdate;

        public virtual void OnUpdate()
        {
            throw new System.NotImplementedException();
        }
        public virtual void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        private SubComponentProcessor mSubComponentProcessor = null;

        protected SubComponentProcessor processor
        {
            get
            {
                if (mSubComponentProcessor == null)
                {
                    mSubComponentProcessor = this.gameObject.GetComponentInParent<SubComponentProcessor>();
                }
                return mSubComponentProcessor;
            }
        }
    }
}