using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eyeManager : MonoBehaviour
{
    public Sprite eyeOpenImage;
    public Sprite eyeClosedImage;
    public GameObject image;

    public InputField passwordInput;
    public InputField confirmPasswordInput;

    private bool open;

    void Start()
    {
        open = false;
    }

    public void ChangeEyeImage()
    {
        if (open)
        {
            image.GetComponent<Image>().sprite = eyeClosedImage;
            passwordInput.contentType = InputField.ContentType.Password;

            if (confirmPasswordInput != null)
            {
                confirmPasswordInput.contentType = InputField.ContentType.Password;
            }
            open = false;
        }
        else
        {
            image.GetComponent<Image>().sprite = eyeOpenImage;
            passwordInput.contentType = InputField.ContentType.Standard;

            if (confirmPasswordInput != null)
            {
                confirmPasswordInput.contentType = InputField.ContentType.Standard;
            }

            open = true;
        }

        passwordInput.ForceLabelUpdate();

        if (confirmPasswordInput != null)
        {
            confirmPasswordInput.ForceLabelUpdate();
        }
    }
}
