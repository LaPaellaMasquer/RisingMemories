using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    public GameObject[] objects;

    public static DontDestroyOnLoad instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'un instance de DontDestroy dans la scène");
            return;
        }

        instance = this;

        foreach (var element in objects)
        {
            DontDestroyOnLoad(element);
        }
    }

    public void setActiveObj(bool active)
    {
        foreach (var element in objects)
        {
            if (element.name != "GameManager")
            {
                element.SetActive(active);
            }
        }
    }

    public void RemoveFromDontDestroyOnLoad()
    {
        foreach (var element in objects) {
            SceneManager.MoveGameObjectToScene(element, SceneManager.GetActiveScene());
        }
    }

    public void RemoveFromDontDestroyOnLoad(GameObject obj) 
    {
        SceneManager.MoveGameObjectToScene(obj, SceneManager.GetActiveScene());
    }
}