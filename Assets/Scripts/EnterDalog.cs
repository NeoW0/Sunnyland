using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDalog : MonoBehaviour
{

    public GameObject EnterDialog;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EnterDialog.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EnterDialog.SetActive(false);
        }
    }
}
