using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Toorah.Files;
using System.IO;

[RequireComponent(typeof(RectTransform))]
public class DragFilesOntoMe : MonoBehaviour {

	[SerializeField] FileHopper m_fileHopper;
	[SerializeField] Text m_text;

	// Use this for initialization
	void Start () { 
		m_fileHopper.OnFilesDropped.AddListener(DroppedFilesOnMe);
	}

	void DroppedFilesOnMe(List<string> files, Vector2 pos)
	{
		//See if mouse is inside of Rect of this RectTransform while dropping files
		if(RectTransformUtility.RectangleContainsScreenPoint(transform as RectTransform, pos))
		{
			m_text.text = "Dropped on me: " + name + ", file count: " + files.Count.ToString()
				+ "\n First File: " + files[0];

            if (files.Count > 0)
            {
                Debug.Log(files[0]);
                m_text.text = files[0];

                //Comprovar que no es un directori (Encara qeu no es possible seleccionar un)
                if (!Directory.Exists(files[0]))
                {
                    //Comprovar que fitxer es .gpx  
                    string[] words = files[0].Split('.');
                    Debug.Log("Fitxer seleccionat No es un directori");
                    Debug.Log(words[words.Length - 1]);
                    string fileType = words[words.Length - 1];

                    if (fileType != "gpx")
                    {
                        //ERROR
                    }
                    else
                    {
                        GameObject select = GameObject.Find("SeleccionRuta");
                        SeleccionRuta ruta = select.GetComponent<SeleccionRuta>();
                        //ruta.routePath = files[0];
                        ruta.GetTrack(files[0]);
                        //Set path de fitxer al script d'obrir fitxer i dibuixar perfil
                    }
                }
            }
            StopListener();
		}
	}

	public void StopListener() {
		m_fileHopper.OnFilesDropped.RemoveListener(DroppedFilesOnMe);
	}
}
