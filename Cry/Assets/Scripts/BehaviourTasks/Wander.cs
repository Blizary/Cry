using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Wander : Action
{
    public SharedVector3 targetPosition;

    private AnimalBase animalBase;
    public override void OnStart()
    {
        animalBase = GetComponent<AnimalBase>();
    }

	public override TaskStatus OnUpdate()
	{
        
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * animalBase.wanderRadius;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            targetPosition = hit.position;
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
       
	}
}