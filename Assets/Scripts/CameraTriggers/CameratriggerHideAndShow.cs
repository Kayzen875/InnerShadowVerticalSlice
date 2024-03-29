using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameratriggerHideAndShow : MonoBehaviour
{
    public GameObject[] elementsToHide;
    public GameObject[] elementsToShow;

    float timer;
    public float timeToSet;
    bool startCounting;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(startCounting)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                HideandShow();
                Destroy(transform.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            timer = timeToSet;
            startCounting = true;
        }
    }

    void HideandShow()
    {
        for (int i = 0; i < elementsToHide.Length; i++)
        {
            elementsToHide[i].SetActive(false);
        }

        for (int i = 0; i < elementsToShow.Length; i++)
        {
            elementsToShow[i].SetActive(true);
        }
    }
}
