using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class DropObj : Action
{
    /// <summary>
    /// Checks if animal is at a certain distance from the target
    /// </summary>
    private AnimalBase animalBase;
    public SharedGameObject currentFecth;

    public override void OnStart()
    {
        animalBase = GetComponent<AnimalBase>();
    }
    public override TaskStatus OnUpdate()
    {
        currentFecth.Value.transform.parent = null;
        currentFecth.Value.GetComponent<DisableColl>().Dropped();
        currentFecth.Value = null;
        return TaskStatus.Success;

    }
}