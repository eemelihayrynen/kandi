using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using System.Threading;

public class physics : MonoBehaviour
{
    [SerializeField] private float treshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;
    private bool isPressed;
    private Vector3 startPos;
    private ConfigurableJoint joint;
    public UnityEvent onPressed,onReleased;
    public GameObject Filter;
    public float i=0;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
        Filter = GameObject.Find("CVDFilter");
        Filter.SetActive(false);
        
    }
 
    // Update is called once per frame
    void Update()
    {
        if (!isPressed && GetValue() + treshold >= 1){
            Pressed();
        }
        if (isPressed && GetValue() - treshold <= 0){
            Released();
        } 
    }
    private float GetValue(){
        var value  = Vector3.Distance(startPos, transform.localPosition)/joint.linearLimit.limit;
        if (Math.Abs(value)<deadZone){
            value = 0;
        }
        return Mathf.Clamp(value, -1f,1f);
    }
    private void Pressed(){
        isPressed =true;
        onPressed.Invoke();
        i++;
        if(i % 2 == 0){
            Filter.SetActive(false);
        }
        else{
            Filter.SetActive(true);
        }
        Debug.Log("Pressed");
    }
    private void Released(){
        isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released");
        
        
           
        }
    }

