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

        private SubComponentProcessor m_processor = null;

        public SubComponentProcessor processor
        {
            get
            {
                if (m_processor == null)
                {
                    m_processor = this.gameObject.GetComponentInParent<SubComponentProcessor>();
                }
                return m_processor;
            }
        }
    }
}