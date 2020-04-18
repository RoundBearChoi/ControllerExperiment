using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class CharacterControllerTest : MonoBehaviour
    {
        public CharacterController con;
        public float MoveSpeed;

        private void Update()
        {
            if (Input.GetKey(KeyCode.D))
            {
                con.Move(this.transform.forward * MoveSpeed * Time.deltaTime);
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                con.Move(-this.transform.forward * MoveSpeed * Time.deltaTime);
            }
        }
    }
}