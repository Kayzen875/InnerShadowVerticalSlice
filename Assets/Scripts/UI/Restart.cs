using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public float timer;
    public Transform[] show;
    public Transform[] hide;
    bool restarted;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && !restarted)
        {
            for (int i = 0; i < hide.Length; i++)
            {
                hide[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < show.Length; i++)
            {
                show[i].gameObject.SetActive(true);
            }
            restarted = true;
        }
    }
}
