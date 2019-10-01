using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ResetWait : Action
{
    private AnimalBase animalBase;
    public override void OnStart()
    {
        animalBase = GetComponent<AnimalBase>();
    }

    public override TaskStatus OnUpdate()
	{
        animalBase.waiting = false;
        return TaskStatus.Success;
	}
}