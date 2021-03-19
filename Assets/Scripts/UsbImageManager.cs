using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsbImageManager : MonoBehaviour
{

    public Image usbImage;
    public static bool haveUsb;
    private bool checkUsb;

    // Start is called before the first frame update
    void Start()
    {
        checkImgUsb();
    }

    private void checkImgUsb()
    {
        //TODO
       /*Debug.Log("Check Image:" + haveUsb);
        if (haveUsb)
        {
            usbImage.sprite = (Sprite)Resources.Load("Img/usb");
        }
        else
        {
            usbImage.sprite = (Sprite)Resources.Load("Img/noUSB");
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (checkUsb != haveUsb)
        {
            checkImgUsb();
            checkUsb = haveUsb;
        }
    }
}
