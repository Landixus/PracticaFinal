using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordInfo : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject canva;

    void Start()
    {
        canva.SetActive(false);
    }

    public void OnMouseOver()
    {
        Debug.Log("Mouse Over");
        canva.SetActive(true);
    }

    public void OnMouseExit()
    {
        Debug.Log("Mouse Exit");
        canva.SetActive(false);
    }
}
