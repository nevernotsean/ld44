using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPosition : MonoBehaviour
{
    NavMeshAgent nma;
    public Vector3Variable Position;
    public FloatVariable CurrentSpeed;
    public BoolVariable IsMoving;
    
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
    }

    void Update(){
        if (CurrentSpeed)
            CurrentSpeed.SetValue(nma.velocity.magnitude);

        if (IsMoving)
            IsMoving.SetValue(nma.velocity.magnitude > 1);
    }

    public void NavigateToPosition () {
        // print(nma.navMeshOwner);
        // if (!nma.navMeshOwner) {
        //     nma.Warp(Position.Value);
        // } else {
            nma.enabled = true;
            nma.SetDestination(Position.Value);
        // }
    }
}
