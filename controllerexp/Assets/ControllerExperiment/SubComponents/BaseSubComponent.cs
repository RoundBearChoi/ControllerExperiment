using UnityEngine;

namespace ControllerExperiment.SubComponents
{
    public abstract class BaseSubComponent : MonoBehaviour
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

        private SubComponentProcessor mProcessor = null;

        protected SubComponentProcessor subComponentProcessor
        {
            get
            {
                if (mProcessor == null)
                {
                    mProcessor = this.gameObject.GetComponentInParent<SubComponentProcessor>();
                }
                return mProcessor;
            }
        }
    }
}