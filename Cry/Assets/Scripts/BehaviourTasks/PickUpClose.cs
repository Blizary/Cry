using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class PickUpClose : Conditional
{
    /// <summary>
    /// checks if there is a pick up object close to the animal
    /// </summary>
    private AnimalBase animalBase;

    public override void OnStart()
    {
        animalBase = GetComponent<AnimalBase>();
    }
    public override TaskStatus OnUpdate()
    {
        if (animalBase.isFollowing)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }

    }
}
