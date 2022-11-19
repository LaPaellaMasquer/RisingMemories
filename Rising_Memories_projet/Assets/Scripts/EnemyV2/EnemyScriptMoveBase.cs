using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScriptMoveBase : MonoBehaviour
{
    protected GameObject player;
    protected Transform centerCircle;
    protected float circleRadius;
    [SerializeField]
    protected LayerMask layerPlayer=9;
    [SerializeField]
    protected LayerMask layerGround=8;
    protected bool isPlayerEnter;

    private void FixedUpdate()
    {
        isPlayerEnter = Physics2D.OverlapCircle(centerCircle.position, circleRadius, layerPlayer);
    }
    
    public void setCenterCircle(Transform pos)
    {
        this.centerCircle = pos;
    }

    protected void setRadiusCircle(float pos)
    {
        circleRadius = pos;
    }
    protected void setPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
