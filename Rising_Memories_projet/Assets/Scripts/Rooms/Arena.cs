using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Arena : MonoBehaviour
{
    private const float WAITTIME = 5f;

    public RoomPrefab room;
    public GameObject[] waves;
    public GameObject[] wavestext;
    private GameObject curr_wave;
    private GameObject curr_wavetext;
    private GameObject[] enemies;
    private int nwaves;
    private int count = 0;
    private bool iswaiting;

    private void nexthWave()
    {

        curr_wave.SetActive(false);
        count++;
        if (count < nwaves)
        {
            StartCoroutine(waittext());
        }
    }

    public void startWave()
    {
        curr_wave = waves[count];
        curr_wave.SetActive(true);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    IEnumerator waittext()
    {
        curr_wavetext = wavestext[count];
        curr_wavetext.SetActive(true);
        iswaiting = true;
        yield return new WaitForSeconds(WAITTIME);
        curr_wavetext.SetActive(false);
        startWave();
        iswaiting = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        nwaves = waves.Length;
        StartCoroutine(waittext());
    }

    // Update is called once per frame
    void Update()
    {
        if (count < nwaves)
        {
            if (!iswaiting)
            {
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
                if (enemies.Length == 0)
                {
                    nexthWave();
                }
            }
        }
        else
        {
            wavestext[count].SetActive(true);
            room.exit.SetActive(true);
        }

    }
}
