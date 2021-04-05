using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserHistorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<Session> hist = PaginaPrincipal.user.historial;

        Debug.Log(hist.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
