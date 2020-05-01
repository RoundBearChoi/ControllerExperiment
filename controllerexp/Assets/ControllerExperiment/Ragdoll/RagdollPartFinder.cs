using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public static class RagdollPartFinder
    {
        public static Rigidbody GetRootJoint(ControllerEntity entity)
        {
            Rigidbody[] arr = entity.gameObject.GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody r in arr)
            {
                ConfigurableJoint j = r.GetComponent<ConfigurableJoint>();

                if (j == null)
                {
                    if (r.gameObject != entity.gameObject)
                    {
                        return r;
                    }
                }
            }

            return null;
        }
    }
}