using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GoHome : Action
{
    /// <summary>
    /// makes AI go to their home location defined in the AI script
    /// </summary>
    /// 


    private AnimalBase animalBase;
	public override void OnStart()
	{
        animalBase = GetComponent<AnimalBase>();
        animalBase.moveToLocations.Add(animalBase.spawnLocation);

    }

	public override TaskStatus OnUpdate()
	{
        return TaskStatus.Success;
    }
}