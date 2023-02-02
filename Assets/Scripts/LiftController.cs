using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftController : MonoBehaviour
{
    [SerializeField] private SliderJoint2D SliderPlatform;

    public void ToggleLiftDirection()
    {
        JointMotor2D motor = SliderPlatform.motor;
        JointMotor2D newMotor = new JointMotor2D
        {
            motorSpeed = -1 * motor.motorSpeed,
            maxMotorTorque = motor.maxMotorTorque
        };
        SliderPlatform.motor = newMotor;
    }
}
