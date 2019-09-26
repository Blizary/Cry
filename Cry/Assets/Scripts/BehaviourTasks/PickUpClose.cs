using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class PickUpClose : Conditional
{
    /// <summary>
    /// checks if there is a pick up object close to the animal
    /// </summary>
    private AnimalBase animalBase;
    public SharedGameObject currentFecth;

    public override void OnStart()
    {
        animalBase = GetComponent<AnimalBase>();
    }
    public override TaskStatus OnUpdate()
    {

        if (animalBase.fetchQuery.GetComponent<FetchQueryStore>().fetchObjs.Count != 0)
        {
            if (animalBase.moveToLocations.Contains(animalBase.fetchQuery.GetComponent<FetchQueryStore>().fetchObjs[0].transform.position))
            {

            }
            else
            {
                animalBase.moveToLocations.Clear();
                currentFecth.Value = animalBase.fetchQuery.GetComponent<FetchQueryStore>().fetchObjs[0];
                animalBase.moveToLocations.Add(animalBase.fetchQuery.GetComponent<FetchQueryStore>().fetchObjs[0].transform.position);
            }
            
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }

    }
}
