using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsNight : Conditional
{
    private DayNightCycle timeOfTheday;
   

    public override void OnStart()
    {
        timeOfTheday = GameObject.FindGameObjectWithTag("DayNight").GetComponent<DayNightCycle>();

    }

    public override TaskStatus OnUpdate()
	{
        if (timeOfTheday.timeOfDay <= 0.3f || timeOfTheday.timeOfDay >= 0.65f)
        {
            return TaskStatus.Success;
           

        }
        else
        {
            return TaskStatus.Failure;
        }
       
	}
}