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
        Debug.Log("time : " + timeOfTheday.timeOfDay);
        if (timeOfTheday.timeOfDay <= 0.3f || timeOfTheday.timeOfDay >= 0.65f)
        {
            return TaskStatus.Failure;
           

        }
        else
        {
            return TaskStatus.Success;
        }
       
	}
}