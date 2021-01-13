using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWokout : MonoBehaviour
{

    int numBlocs;

    static Workout workout;
    // Start is called before the first frame update
    void Start()
    {
        //Li hem de passar l'id que l'agafarem de la bbdd
        workout = new Workout(1);
        numBlocs = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Tornar a actualitzar la vista de la llista quan s'ha afegit un bloc
        if (workout.blocs.Count != numBlocs)
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

    public void AddBlock()
    {
        Debug.Log("S'ha apretat afegir bloc");
        //Crear bloc i afegir-lo a la llista de blocs del workout
        Bloc bloc = new Bloc(workout.blocs.Count + 1);

        workout.AddBloc(bloc);
    }
}
