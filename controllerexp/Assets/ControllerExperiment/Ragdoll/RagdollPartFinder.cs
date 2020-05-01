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

        public static Rigidbody GetHip(ControllerEntity entity)
        {
            ConfigurableJoint[] arr = entity.GetComponentsInChildren<ConfigurableJoint>();

            foreach (ConfigurableJoint j in arr)
            {
                if (j.name.Contains("hip") || j.name.Contains("Hip"))
                {
                    return j.GetComponent<Rigidbody>();
                }
            }

            return null;
        }
    }
}