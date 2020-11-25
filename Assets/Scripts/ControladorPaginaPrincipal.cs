using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPaginaPrincipal : MonoBehaviour
{

    public static User user;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(user.toString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
