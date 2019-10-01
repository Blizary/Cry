using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsWaiting : Conditional
{
    /// <summary>
    /// checks if the animal is supposed to be waiting, normaly happens when the animal is wondering around
    /// </summary>
    private AnimalBase animalBase;
    private Rigidbody rigidbody;

    public override void OnStart()
    {
        animalBase = GetComponent<AnimalBase>();
        rigidbody = GetComponent<Rigidbody>();
    }
    public override TaskStatus OnUpdate()
    {
        if (animalBase.waiting)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }

    }
}