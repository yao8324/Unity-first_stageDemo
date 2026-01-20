using UnityEngine;

public class Player_Combat : Entity_Combat
{
    [Header("·´»÷²ÎÊý")]
    [SerializeField] private float counterRecovery;

    public bool CounterAttackPerformed()
    {
        bool hasPerformedCounter = false;

        foreach(var target in GetDetectedColliders())
        {

            ICounterable counterable = target.GetComponent<ICounterable>();

            if (counterable == null)
                continue;

            if(counterable.CanBeCountered)
            {
                counterable.HandleCounter();
                hasPerformedCounter = true;
            }
        }

        return hasPerformedCounter;
    }

    public float GetCounterRecoveryDuration() => counterRecovery;
}
