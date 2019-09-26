using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MoveTo : Action
{
    public Vector3 targetPosition;
    public SharedGameObject thisGO;

    private AnimalBase animalBase;
    public override void OnStart()
	{

        animalBase =GetComponent<AnimalBase>();
        //if there is a new location to go to
        if(animalBase.moveToLocations.Count!=0)
        {
            targetPosition = animalBase.moveToLocations[0];
            animalBase.SetDestination(targetPosition);
        }      
        animalBase.SetAnimation("isRunning", true);
    }

	public override TaskStatus OnUpdate()
	{

        if (Vector3.Distance(this.transform.position, targetPosition) <= animalBase.arrivingProximity)
        {
            animalBase.SetAnimation ("isRunning", false);
            animalBase.moveToLocations.Remove(targetPosition);
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Running;
        }
        
	}
}