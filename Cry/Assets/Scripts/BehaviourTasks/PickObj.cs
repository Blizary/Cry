using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class PickObj : Action
{
    /// <summary>
     /// Checks if animal is at a certain distance from the target
    /// </summary>
    private AnimalBase animalBase;
    public SharedGameObject currentFecth;
    public float distance=0.5f;

    public override void OnStart()
    {
        animalBase = GetComponent<AnimalBase>();
    }
    public override TaskStatus OnUpdate()
    {
        if (Vector3.Distance(gameObject.transform.position,
            animalBase.fetchQuery.GetComponent<FetchQueryStore>().fetchObjs[0].transform.position) < distance)
        {
            currentFecth.Value = animalBase.fetchQuery.GetComponent<FetchQueryStore>().fetchObjs[0];
            currentFecth.Value.transform.position = animalBase.holdLocation.transform.position;
            currentFecth.Value.transform.parent = this.transform;
            currentFecth.Value.GetComponent<DisableColl>().PickedUp();
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }

    }
}