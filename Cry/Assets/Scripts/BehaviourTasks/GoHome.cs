using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GoHome : Action
{
    /// <summary>
    /// makes AI go to their home location defined in the AI script
    /// </summary>
    /// 

    public SharedVector3 targetPosition;

    private AnimalBase animalBase;
	public override void OnStart()
	{
        animalBase = GetComponent<AnimalBase>();
        targetPosition = animalBase.spawnLocation;

    }

	public override TaskStatus OnUpdate()
	{
        return TaskStatus.Success;
    }
}