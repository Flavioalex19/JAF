using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIStates
{
    None,
    Patrol,
    Pursuing,
    Attacking
}
public class AI : MonoBehaviour
{

    public AIStates MyStates;
    public Transform Target;

    float _distance;

    //Components
    NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (MyStates)
        {
            case AIStates.None:
                break;
            case AIStates.Patrol:
                break;
            case AIStates.Pursuing:
                MoveTo();
                break;
            case AIStates.Attacking:
                break;
            default: break;

        }

        if(Target != null)
        {
            _distance = Vector3.Distance(transform.position, Target.position);
            _agent.SetDestination(Target.position);
        }
    }

    

    void MoveTo()
    {
        
        _agent.SetDestination(Target.position);

        if (_agent.remainingDistance <= _agent.stoppingDistance + .3f)
        {
            print("Time to change");
            _agent.isStopped = true;
            MyStates = AIStates.Attacking;
        }
        else if (_agent.remainingDistance > _agent.stoppingDistance + .3f)
        {
            _agent.isStopped = false;
        }
    }
}
