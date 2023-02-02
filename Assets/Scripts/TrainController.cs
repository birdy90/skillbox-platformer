using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class TrainController : MonoBehaviour
{
    [SerializeField] private List<WheelJoint2D> Wheels;
    [SerializeField] private Light2D Light;
    [SerializeField] private float TrainSpeed;
    [SerializeField] private float TimeTillStartMoving = 3f;

    public void StartTrain()
    {
        StartCoroutine(nameof(LightBlink));
    }

    private IEnumerator LightBlink()
    {
        float startTime = Time.time;

        while (Time.time - startTime < TimeTillStartMoving)
        {
            Light.gameObject.SetActive(Random.Range(0f, 1f) > 0.5f);
            yield return new WaitForSeconds(0.2f);
        }
        
        Light.gameObject.SetActive(true);
        
        StartCoroutine(nameof(EngineRpmUp));
    }

    private IEnumerator EngineRpmUp()
    {
        if (Wheels.Count == 0) yield break;
        
        Wheels.ForEach(wheel => wheel.useMotor = true);

        float currentSpeed = 0;
        while (TrainSpeed - currentSpeed > 0.01f)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, TrainSpeed, Time.deltaTime / 2f);
            Wheels.ForEach(wheel => {
                JointMotor2D motor = new JointMotor2D
                {
                    motorSpeed = currentSpeed,
                    maxMotorTorque = Wheels[0].motor.maxMotorTorque
                };
                wheel.motor = motor;
            });
            
            yield return null;
        }
    }
}
