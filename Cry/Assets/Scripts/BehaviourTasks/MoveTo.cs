using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MoveTo : Action
{
    public SharedVector3 targetPosition;

    private AnimalBase animalBase;
    public override void OnStart()
	{
        animalBase = GetComponent<AnimalBase>();

        animalBase.SetDestination(targetPosition.Value);
        animalBase.SetAnimation("isRunning", true);
    }

	public override TaskStatus OnUpdate()
	{
      
        if (Vector3.Distance(this.transform.position, targetPosition.Value) <= animalBase.arrivingProximity)
        {
            animalBase.SetAnimation ("isRunning", false);
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
        
	}
}