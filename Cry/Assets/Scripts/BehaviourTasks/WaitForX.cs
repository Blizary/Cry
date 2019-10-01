using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class WaitForX : Action
{

    /// <summary>
    /// timer to countdown the wait time
    /// </summary>
    public SharedFloat waitTimer;
	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
        if(waitTimer.Value>0)
        {
            waitTimer.Value -= Time.deltaTime;
            return TaskStatus.Failure;
        }
        else
        {
            GetComponent<AnimalBase>().waiting = false;
            return TaskStatus.Success;
           
        }
		
	}
}