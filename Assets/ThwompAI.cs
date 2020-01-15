using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ThwompAI : MonoBehaviour
{
    public enum ThwompState { 
        IDLE,
        CHASING,
        ATTACKING
    }

    public GameObject Dan;
    public ThwompState CurrentState;
    public float MoveSpeed;
    public float ThwompSpeed;
    public float ActivationDistance;
    public float ActivationDelay;

    // Start is called before the first frame update
    void Start()
    {
        CurrentState = ThwompState.IDLE;
        StartCoroutine(ThwompFSM());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ThwompFSM() {
        while (true) {
            yield return StartCoroutine(CurrentState.ToString());
        }
    }

    IEnumerator IDLE() {
        Debug.Log("Enter Idle State");

        while (CurrentState == ThwompState.IDLE) {
            
            if (Vector3.Distance(Dan.transform.position, transform.position) < ActivationDistance) {
                CurrentState = ThwompState.CHASING;
            }
            
            yield return null;
        }

        Debug.Log("Exit Idle State");
    }

    IEnumerator CHASING() {
        Debug.Log("Enter Chasing State");
        yield return new WaitForSeconds(ActivationDelay);

        while (CurrentState == ThwompState.CHASING) {

            var newPosition = Vector3.MoveTowards(transform.position, Dan.transform.position, MoveSpeed * Time.deltaTime);
            transform.position = new Vector3(newPosition.x, transform.position.y, transform.position.z);

            if (Math.Abs(transform.position.x - Dan.transform.position.x) < 0.5) {
                CurrentState = ThwompState.ATTACKING;
            }

            yield return null;
        }
        Debug.Log("Exit Chasing State");
    }

    IEnumerator ATTACKING() {
        Debug.Log("Enter Attacking State");
        while (CurrentState == ThwompState.ATTACKING) {
            var oldY = transform.position.y;
            var newPosition = Vector3.MoveTowards(transform.position, Dan.transform.position, ThwompSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newPosition.y, transform.position.z);

            if (Math.Abs(transform.position.x - Dan.transform.position.x) > 0.5) {
                CurrentState = ThwompState.CHASING;
                transform.position = new Vector3(transform.position.x, oldY, transform.position.z);
            }

            yield return null;
        }
        Debug.Log("Exit Attacking State");
    }
}
