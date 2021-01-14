using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CreateWorkoutList : MonoBehaviour
{
    static public bool afegit;
    public GameObject contentPanel;

    public GameObject buttPrefab;


    [SerializeField] Text numBlocText;
    [SerializeField] InputField duracioInput;
    [SerializeField] InputField potenciaInput;

    private List<Bloc> blocsPrivat;
    private int blocSelected;

    // Start is called before the first frame update
    public void EnsenyarLListaBlocs(List<Bloc> blocs)
    {
        blocsPrivat = blocs;

        float myWidth;
        //int listSize = RoutesManager.rutas.Count;
        int listSize = blocs.Count;
        if (listSize <= 10)
        {
            myWidth = 1365;
        }
        else
        {
            myWidth = listSize * buttPrefab.GetComponent<RectTransform>().sizeDelta.x + 10 * listSize;
        }
        contentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, myWidth);
        contentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 108);
        //GameObject buttonTemplate = transform.GetChild(0).gameObject;
        GameObject g;

        for (int i = 0; i < listSize; i++)
        {
            Bloc bloc = blocs[i];
            g = Instantiate(buttPrefab, transform);
            g.transform.GetChild(1).GetComponent<Text>().text = bloc.numBloc.ToString();
            g.transform.GetChild(4).GetComponent<Text>().text = bloc.pot.ToString() + " W";
            g.transform.GetChild(5).GetComponent<Text>().text = bloc.temps.ToString() + " seg";


            g.GetComponent<Button>().AddEventListener(i, ItemClicked);

        }

        //Destroy(buttonTemplate);
    }

    public void DestroyList()
    {
        for (int i = 1; i < contentPanel.transform.childCount; i++)
        {
            GameObject.Destroy(contentPanel.transform.GetChild(i).gameObject);
        }
    }

    private void ItemClicked(int i)
    {
        Bloc bloc = blocsPrivat[i];
        blocSelected = i;

        numBlocText.text = bloc.numBloc.ToString();
        duracioInput.text = bloc.temps.ToString();
        potenciaInput.text = bloc.pot.ToString();

        CreateWokout.numBloc = i;
    }

    
}
