using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Following : Conditional
{
    /// <summary>
    /// checks if the animal is supposed to be following the player
    /// </summary>
    private AnimalBase animalBase;

    public override void OnStart()
    {
        animalBase = GetComponent<AnimalBase>();
    }
    public override TaskStatus OnUpdate()
	{
        if(animalBase.isFollowing)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
		
	}
}