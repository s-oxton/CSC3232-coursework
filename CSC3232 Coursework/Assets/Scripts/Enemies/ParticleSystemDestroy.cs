using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //destroys the particle effect after it's finished.
        Destroy(gameObject, 2f);
    }

}
