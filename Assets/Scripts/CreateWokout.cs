using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class CreateWokout : MonoBehaviour
{

    int numBlocs;

    static Workout workout;

    static public int numBloc;

    [SerializeField] Text numBlocText;
    [SerializeField] InputField duracioInput;
    [SerializeField] Text duracioErrorText;
    [SerializeField] InputField potenciaInput;
    [SerializeField] Text potenciaErrorText;

    [SerializeField] Text tempsTotalText;
    [SerializeField] Text nameErrorText;
    [SerializeField] InputField nameInput;

    [SerializeField] Scrollbar scrollbar;

    [SerializeField] Text errorGeneralText;

    private bool nomCorrecte;
    private string nomWorkout;

    // Start is called before the first frame update
    void Start()
    {
        numBlocText.text = "";
        errorGeneralText.text= "";
        nomWorkout = "";
        //Li hem de passar l'id que l'agafarem de la bbdd
        workout = new Workout(1);
        numBlocs = 0;

        duracioErrorText.text = "";
        potenciaErrorText.text = "";

        duracioInput.interactable = false;
        potenciaInput.interactable = false;

        tempsTotalText.text = 0.ToString() + " segons";
        nameInput.characterLimit = 100;

        nameErrorText.text = "";
        nomCorrecte = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Num blocs" + workout.blocs.Count);
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

        bloc.temps = 0;
        bloc.pot = 0;

        workout.AddBloc(bloc);

        scrollbar.value = 1;
    }

    public void RemoveLastBlock()
    {
        workout.RemoveLastBloc();
        UpdateTempsTotal();
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
        duracioErrorText.text = "";
        if (format)
        {
            if (seconds >= 60 || seconds < 0)
            {
                duracioErrorText.text = "Segons incorrectes ";
                format = false;
            }
            if (minutes < 0 || minutes > 900)
            {
                duracioErrorText.text += "Minuts incorrectes";
                format = false;
            }

            if (format)
            {
                //multipliquem minuts per 60 per convertir-lo en segons
                minutes = minutes * 60;

                if (minutes == 0 && seconds == 0)
                {
                    duracioErrorText.text = "El temps no pot ser 0";
                }
                else {
                    workout.blocs[numBloc].temps = minutes + seconds;

                    //Quan tenim els canvis fets fem que imprimeixi la llista de nou perque es vegi el canvi en el bloc
                    imprimirLLista();
                }
            }
        } else
        {
            //Text d'error
            duracioErrorText.text = "Format incorrecte";
        }


        //Calcular nou temps total

        UpdateTempsTotal();
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
            else {
                //Text d'error
                potenciaErrorText.text = "La poténcia ha de ser un número entre 1 i 2000";
            }
        }
        else
        {
            //Text d'error
            potenciaErrorText.text = "La poténcia ha de ser un número entre 1 i 2000";
        }
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


    private void UpdateTempsTotal()
    {
        int totalTime = CalcTotalTime();

        workout.tempsTotal = totalTime;

        tempsTotalText.text = FromSecondsToMinutesString(totalTime) + " minuts";
    }
    private int CalcTotalTime()
    {
        int totalTime = 0;

        foreach (var bloc in workout.blocs)    
        {
            totalTime += bloc.temps;
        }

        return totalTime;
    }


    private string FromSecondsToMinutesString(int totalSeconds)
    {

        if (totalSeconds == 0)
        {
            return "00:00";
        }
        float total = (float) totalSeconds / 60;

        int minutes = (int)total;

        Debug.Log(total);

        var seconds  = (total - Math.Truncate(total)) * 30 / 0.5;

        seconds = (float)Math.Round(seconds, 3);

        Debug.Log(seconds);

        return minutes.ToString() + ":" + seconds.ToString();
    }


    public void ComporvarNom() 
    {
        Regex regexNom = new Regex(@"^[A-Za-z0-9 _-]+$");

        Match match = regexNom.Match(nameInput.text);
        if (!match.Success)
        {
            nomCorrecte = false;
            nameErrorText.text = "Error en el nom (Només s'accepta números, lletres, espais, _ i -)";
            Debug.Log("Nom incorrecte " + nameInput.text);
        }
        else {
            nomCorrecte = true;
            nameErrorText.text = "";
            nomWorkout = nameInput.text;
            Debug.Log("Nom correcte " + nameInput.text);
        }
    }

    public void Confirm()
    {
        if (nomCorrecte && workout.blocs.Count > 0)
        {
            bool correcte = true;

            //Comprovar que no hi ha cap paràmetre d'un bloc que sigui == 0
            foreach (var bloc in workout.blocs)
            {
                if (bloc.temps == 0)
                {
                    errorGeneralText.text += "Error en el temps del bloc " + bloc.numBloc + " ";
                    correcte = false;
                }

                if (bloc.pot == 0)
                {
                    errorGeneralText.text += "Error en la poténcia del bloc" + bloc.numBloc + " ";
                    correcte = false;
                }
            }

            if (correcte)
            {
                //Guardar workout en l'usuari
                //Ensneyar POP UP amb entrenament creat i crear nou Entrenament
                //(Es podria fer fent enable d'un panel que est'a desactivat per defecte)

                workout.nom = nomWorkout;
                //Aquesta funció ja ens assigna en temps total a la variable tempsTotal del workout
                UpdateTempsTotal();

                //PaginaPrincipal.user.AfegirWorkout(workout)
                
            }
        }
        else
        {
            if (!nomCorrecte)
            {
                errorGeneralText.text = "El nom de l'entrenament no es correcte. ";
            }

            if (workout.blocs.Count == 0)
            {
                errorGeneralText.text += "No s'han creat blocs";
            }

        }
    }
}
