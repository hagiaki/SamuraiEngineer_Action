using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Event : MonoBehaviour
{
    public int EventID = 0;
    List<string[]> csvDatas = null; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            csvDatas = EventManager.instance.subroutine(EventID);
            Launch();
        }
    }

    void Launch()
    {
        if (csvDatas == null)
        {
            return;
        }
        if (csvDatas[0][0].Equals("CLEAR"))
        {
            SceneManager.LoadScene("Result");
        }
    }
}