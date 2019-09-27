using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CloseToTarget : Conditional
{
    /// <summary>
    /// Checks if animal is at a certain distance from the target
    /// </summary>
    private AnimalBase animalBase;
    public float distance = 1f;

    public override void OnStart()
    {
        animalBase = GetComponent<AnimalBase>();
    }
    public override TaskStatus OnUpdate()
    {
        if(Vector3.Distance(gameObject.transform.position,animalBase.moveToLocations[0])<distance)
        {
            animalBase.moveToLocations.Clear();
            animalBase.SetNavmeshMov(true);
            animalBase.SetAnimation("isRunning", false);
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }

    }
}