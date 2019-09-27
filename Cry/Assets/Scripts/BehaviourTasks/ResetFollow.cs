using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ResetFollow : Action
{
    /// <summary>
    /// resets follow
    /// </summary>
    private AnimalBase animalBase;


    public override void OnStart()
    {
        animalBase = GetComponent<AnimalBase>();
    }

    public override TaskStatus OnUpdate()
	{
        animalBase.isFollowing = false;
		return TaskStatus.Success;
	}
}