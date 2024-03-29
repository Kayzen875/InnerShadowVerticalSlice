using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerTrigger : MonoBehaviour
{
    public int sceneToLoad;
    public bool UIScene;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            if (!UIScene)
            {
                LevelManager.Instance.LoadAsyncScene(sceneToLoad);
                collider.gameObject.GetComponent<PaxFSMController>().FrozePax();
            }
            else
            {
                LevelManager.Instance.EndGameScene();
            }
                
        }
    }
}
