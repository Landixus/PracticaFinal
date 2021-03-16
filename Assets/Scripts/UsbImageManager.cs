using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsbImageManager : MonoBehaviour
{

    public static Image usbImage;
    private GameObject imgObject;

    // Start is called before the first frame update
    void Start()
    {
        imgObject = GameObject.Find("usbImg");
        if (imgObject != null)
        {
            usbImage = (Image)imgObject.GetComponent(typeof(Image));
            usbImage.sprite = (Sprite)Resources.Load("Img/usb");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
