using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Wander : Action
{

    private AnimalBase animalBase;
    public override void OnStart()
    {
        animalBase = GetComponent<AnimalBase>();
    }

	public override TaskStatus OnUpdate()
	{
        
        //there are no other paths to move to
        if(animalBase.moveToLocations.Count==0)
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * animalBase.wanderRadius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                animalBase.moveToLocations.Add(randomPoint);
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
        else
        {
            return TaskStatus.Success;
        }
        
       
	}
}