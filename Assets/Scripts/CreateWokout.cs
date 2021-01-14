using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateWokout : MonoBehaviour
{

    int numBlocs;

    static Workout workout;

    static public int numBloc;

    [SerializeField] InputField duracioInput;
    [SerializeField] Text duracioErrorText;
    [SerializeField] InputField potenciaInput;
    [SerializeField] Text potenciaErrorText;


    // Start is called before the first frame update
    void Start()
    {
        //Li hem de passar l'id que l'agafarem de la bbdd
        workout = new Workout(1);
        numBlocs = 0;

        duracioErrorText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        //Tornar a actualitzar la vista de la llista quan s'ha afegit un bloc
        if (workout.blocs.Count != numBlocs)
        {
            imprimirLLista();
        }
    }

    public void AddBlock()
    {
        //Crear bloc i afegir-lo a la llista de blocs del workout
        Bloc bloc = new Bloc(workout.blocs.Count + 1);

        bloc.temps = (int)UnityEngine.Random.Range(0.0f, 100.0f);
        bloc.pot = (int)UnityEngine.Random.value * 100;

        workout.AddBloc(bloc);
    }

    public void RemoveLastBlock()
    {
        workout.RemoveLastBloc();
    }

    public void DurationChanged()
    {
        string durStr = duracioInput.text;
        //Comprovar que el canvi es correcte
        bool format = durStr.Contains(":");
        //Separem string 
        string[] words = durStr.Split(':');

        int minutes = 0;
        int seconds = 0;

        if (format)
        {
            try
            {
                minutes = Int32.Parse(words[0]);
                seconds = Int32.Parse(words[1]);
            }
            catch (System.Exception)
            {
                format = false;
            }
        }

        if (format)
        {
            //multipliquem minuts per 60 per convertir-lo en segons
            minutes = minutes * 60;

            workout.blocs[numBloc].temps = minutes + seconds;

            //Quan tenim els canvis fets fem que imprimeixi la llista de nou perque es vegi el canvi en el bloc
            imprimirLLista();

        } else
        {
            //Text d'error
            duracioErrorText.text = "Format incorrecte";
        }



    }

    public void PotenciaChanged()
    {
        string potStr = potenciaInput.text;
        //Comprovar que el canvi es correcte

        int pot = 0;
       
        bool format = true;
        
        try
        {
            pot = Int32.Parse(potStr);
        }
        catch (System.Exception)
        {
            format = false;
        }
        

        if (format)
        {
            if (pot > 0 && pot < 2000)
            {
                workout.blocs[numBloc].pot = pot;

                //Quan tenim els canvis fets fem que imprimeixi la llista de nou perque es vegi el canvi en el bloc
                imprimirLLista();
            }
        }
        else
        {
            //Text d'error
            duracioErrorText.text = "Format incorrecte";
        }

        //workout.blocs[numBloc].temps = duracioInput.text;

    }


    private void imprimirLLista()
    {
        GameObject contentObj;
        CreateWorkoutList contentPanel;
        contentObj = GameObject.Find("Content");
        contentPanel = (CreateWorkoutList)contentObj.GetComponent(typeof(CreateWorkoutList));

        //Primer destruim la llista per després ensenyar-la de nou aj que sinó s'acomulen tots els blocs
        contentPanel.DestroyList();
        contentPanel.EnsenyarLListaBlocs(workout.blocs);
        numBlocs = workout.blocs.Count;
    }

}
