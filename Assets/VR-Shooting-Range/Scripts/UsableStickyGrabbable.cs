using HTC.UnityPlugin.ColliderEvent;
using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UsableStickyGrabbable : MonoBehaviour
    , IColliderEventPressEnterHandler
    , IColliderEventPressExitHandler
{
    public UnityEvent OnUse;
    public ColliderButtonEventData.InputButton UseInputButton = ColliderButtonEventData.InputButton.Trigger;

    public ColliderButtonEventData.InputButton GrabInputButton = ColliderButtonEventData.InputButton.GripOrHandTrigger;
    Transform currentAttachedHand;
    public void OnColliderEventPressEnter(ColliderButtonEventData eventData)
    {
        if (currentAttachedHand == null)
        {
            if (eventData.button == GrabInputButton)
            {
                var hand = eventData.eventCaster.gameObject.GetComponentInParent<VivePoseTracker>();
                currentAttachedHand = hand.transform;
                this.transform.position = currentAttachedHand.position;
                this.transform.rotation = currentAttachedHand.rotation;
                this.transform.parent = currentAttachedHand;
            }
        }
        else
        {
            if (eventData.button == GrabInputButton)
            {
                this.transform.parent = currentAttachedHand = null;
            }
            else if(eventData.button == UseInputButton)
            {
                Use();
            }
        }
    }

    public void OnColliderEventPressExit(ColliderButtonEventData eventData)
    {
    }

    public virtual void Use()
    {
        OnUse?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        //if (currentAttachedHand) {
        //    this.transform.position = currentAttachedHand.position;
        //    this.transform.rotation = currentAttachedHand.rotation;
        //}
    }
}
