using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MoveBoost
{
    private int currentSpeed = 0;
    [SerializeField]
    private int accelerationFactor = 3;
    private int maxSpeed = 500;
    bool acceleratorIsStarted = false;
    bool deceleratorIsStarted = false;

    public override void onUse()
    {

    }
    public override void onUpdate()
    {
        if(PlayerMovements.instance.isWalking)
        {
            if(!acceleratorIsStarted)
                StartCoroutine(Accelerate());
        }
        else
        {
            if (!deceleratorIsStarted && currentSpeed != 0)
                StopBonus();
        }
    }

    public void StopBonus()
    {
        PlayerMovements.instance.speedBonus -= currentSpeed;
        currentSpeed = 0;
        acceleratorIsStarted = false;
        deceleratorIsStarted = false;
    }

    public override void equipEffect()
    {
        currentSpeed = 0;
        PlayerMovements.instance.speedBonus += currentSpeed;
    }
    public override void unEquipEffect()
    {
        StopBonus();
    }

    public IEnumerator Accelerate()
    {
        acceleratorIsStarted = true;
        yield return new WaitForSeconds(.05f);
        if (PlayerMovements.instance.isWalking)
        {
            if (currentSpeed < maxSpeed)
            {
                PlayerMovements.instance.speedBonus += accelerationFactor ;
                currentSpeed += accelerationFactor;
                StartCoroutine(Accelerate());
            }
            else
            {
                acceleratorIsStarted = false;
            }
        }
        else
        {
            acceleratorIsStarted = false;
            StopBonus();
        }
        
    }

    
}
