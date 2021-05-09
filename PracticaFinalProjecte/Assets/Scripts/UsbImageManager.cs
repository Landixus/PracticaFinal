using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsbImageManager : MonoBehaviour
{

    public GameObject noUSBImg;
    public static bool haveUsb;

    // Start is called before the first frame update
    void Start()
    {
        checkImgUsb();
    }

    private void checkImgUsb()
    {
        //TODO
       //Debug.Log("Check Image:" + PaginaPrincipal.haveUSB);
        if (PaginaPrincipal.haveUSB)
        {
            noUSBImg.active = false;
        }
        else {
            noUSBImg.active = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkImgUsb();
    }
}
