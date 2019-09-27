using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class PredatorClose : Conditional
{
    /// <summary>
    ///Checks if there are any predatores close by
    /// </summary>
    /// 


    private AnimalBase animalBase;
    public override void OnStart()
    {
        animalBase = GetComponent<AnimalBase>();

       
    }

    public override TaskStatus OnUpdate()
    {
        if(animalBase.fetchQuery.predators.Count!=0)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
        
    }
}