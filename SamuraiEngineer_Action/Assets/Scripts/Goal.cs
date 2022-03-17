using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public static bool goal;

    // Start is called before the first frame update
    void Start()
    {
        goal = false;
    }

    void Update()
    {
        Debug.Log(goal);
        if (goal)
        {
            SceneManager.LoadScene("Result");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            goal = true;
        }
    }
}
