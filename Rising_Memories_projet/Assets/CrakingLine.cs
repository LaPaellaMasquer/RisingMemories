using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrakingLine : MonoBehaviour
{
    LineRenderer line;
    public float frequency;
    public float amplitude;
    public GameObject dst;
    public GameObject src;
    public float range;
    public int amountOfCrack;
    public EnemyStat _dstStat;
    Vector3[] pts;
    void Start()
    {
        this.line = transform.GetComponent<LineRenderer>();
        pts = new Vector3[amountOfCrack];
        line.positionCount = amountOfCrack;
        for (int i = 1; i < amountOfCrack; i++)
        {
            line.SetPosition(i,new Vector3( (float) i/amountOfCrack,0,0));
            
        }
        line.SetPosition(amountOfCrack - 1, new Vector3(1, 0, 0));
        StartCoroutine(Crack(1));

    }

    private void Update()
    {
        transform.right = dst.transform.position - transform.position;
        float distance = Vector3.Distance(dst.transform.position, src.transform.position);
        if(distance > range)
        {
            dst.GetComponent<EnemyStat>().isAlreadyTargeted = false;
            StopAllCoroutines();
            Destroy(gameObject);
        }
        transform.localScale = new Vector3(distance * 20, 10, 0);
    }

    public IEnumerator Crack(int factor)
    {
        yield return new WaitForSeconds(this.frequency);
        line.GetPositions(pts);
        for (int i = 1; i < amountOfCrack - 1; i+= 2)
        {
            line.SetPosition(i, new Vector3(pts[i].x, Random.Range(-amplitude * factor, 0), 0));
            line.SetPosition(i+1, new Vector3(pts[i+1].x, Random.Range(0, amplitude * factor), 0));
        }
        _dstStat.takeDamage(3);
        if (!_dstStat.isAlive)
        {
            Destroy(this.gameObject);
        }
        else
        {
            StartCoroutine(Crack(-factor));
        }
    }
}
