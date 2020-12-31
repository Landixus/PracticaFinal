using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;
using System.IO;

[RequireComponent(typeof(Button))]
public class CanvasSampleOpenFileText : MonoBehaviour, IPointerDownHandler {
    public Text output;


#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

    public void OnPointerDown(PointerEventData eventData) {
        UploadFile(gameObject.name, "OnFileUpload", ".txt", false);
    }

    // Called from browser
    public void OnFileUpload(string url) {
        StartCoroutine(OutputRoutine(url));
    }
#else
    //
    // Standalone platforms & editor
    //
    public void OnPointerDown(PointerEventData eventData) { }

    void Start() {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick() {

        var extensions = new[] {
            new ExtensionFilter("GPS Exchange Format Files", "gpx"),
            new ExtensionFilter("All Files", "*" ),
        };


        var paths = StandaloneFileBrowser.OpenFilePanel("Title", "", extensions, false);
       
        if (paths.Length > 0) {
            Debug.Log(paths[0]);
            output.text = paths[0];

            //Comprovar que no es un directori (Encara qeu no es possible seleccionar un)
            if (!Directory.Exists(paths[0]))
            {

                //Comprovar que fitxer es .gpx  
                string[] words = paths[0].Split('.');
                Debug.Log("Fitxer seleccionat No es un directori");
                Debug.Log(words[words.Length - 1]);
                string fileType = words[words.Length - 1];

                if (fileType != "gpx")
                {
                    Debug.LogError("El fitxer seleccionat no es un GPX");
                    //ERROR
                } else
                {
                    GameObject select = GameObject.Find("SeleccionRuta");
                    SeleccionRuta ruta = select.GetComponent<SeleccionRuta>();
                   
                    Ruta objecteRuta = ruta.GetTrack(paths[0]);

                    SelectedRoute.ruta = objecteRuta;
                    SelectedRoute.originalPath = paths[0];
                    //Set path de fitxer al script d'obrir fitxer i dibuixar perfil
                }
            }
        }
    }
#endif

   
}