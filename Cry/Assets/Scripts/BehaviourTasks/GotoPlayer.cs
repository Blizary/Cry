using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GotoPlayer : Action
{
    /// <summary>
    /// makes the animal go to the player
    /// </summary>
    private AnimalBase animalBase;
    private GameObject player;
    public override void OnStart()
    {
        animalBase = GetComponent<AnimalBase>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (animalBase.moveToLocations.Contains(player.transform.position))
        {
        }
        else
        {          
          
            animalBase.moveToLocations.Clear();
            animalBase.moveToLocations.Add(player.transform.position);
        }
        

    }

    public override TaskStatus OnUpdate()
	{

		return TaskStatus.Success;
	}
}