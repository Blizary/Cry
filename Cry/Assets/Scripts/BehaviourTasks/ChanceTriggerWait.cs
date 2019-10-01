using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ChanceTriggerWait : Action
{
    /// <summary>
    /// random chance to trigger the animal to wait between wonder
    /// </summary>
    /// 

    public SharedFloat waitTimer;
    private AnimalBase animalBase;
    private int randInt;

    public override void OnStart()
    {
        animalBase = GetComponent<AnimalBase>();
    }
    public override TaskStatus OnUpdate()
    {
        randInt = Random.Range(0, 101);

        if(randInt> animalBase.waitingPercent)
        {
            animalBase.waiting = false;
            waitTimer.Value = 0;
        }
        else
        {
            animalBase.waiting = true;
            waitTimer.Value = Random.Range(10, 30);
        }

        return TaskStatus.Success;

    }
}