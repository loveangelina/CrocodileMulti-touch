using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class nav : MonoBehaviour
{
    public Transform[] target;
    NavMeshAgent agent;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartCoroutine("Move");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Move()
    {
        for (int i = 0; i < target.Length; i++)
        {
           
            agent.destination = target[Random.Range(0,target.Length)].transform.position;
            animator.SetBool("Sprint", true);
            Debug.Log(i);
            if (i ==target.Length-1)
            {
                i = 0;
            }
            
            yield return new WaitForSecondsRealtime(2f);

            animator.SetBool("Sprint", false);
            Debug.Log("²¨Áü");
        }
        yield return null;
        
    }
}
