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
    }

    public void AddBlock()
    {
        //Crear bloc i afegir-lo a la llista de blocs del workout
    }
}
