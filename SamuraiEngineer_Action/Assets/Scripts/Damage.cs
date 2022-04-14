using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Damage : MonoBehaviour
{
    public int HP;
    public Text Vitality;

    // Start is called before the first frame update
    void Start()
    {
        Vitality.text = "HP:" + HP;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            HP -= 1;
            Vitality.text = "HP:" + HP;

            if (HP <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}
