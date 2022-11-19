using UnityEngine;

public class Force 
{
    float baseStrength;
    Vector2 dir;
    float dec;

    float currentStr;
    public Force(Vector2 _dir, float _baseStrength = 15f, float _dec = .90f)
    {
        baseStrength = _baseStrength;
        dir = _dir;
        dec = _dec;

        currentStr = baseStrength;
    }

    public Vector2 getForce()
    {
        currentStr -= currentStr * dec * Time.deltaTime;
        if(currentStr <= baseStrength/15)
        {
            currentStr = 0;
            return Vector2.zero;
        }
        return dir.normalized * currentStr ;
    }
    
    public void PrintForce()
    {
        Debug.Log("Force : cstr = " + currentStr);
    }

}
