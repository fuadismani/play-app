/*==============================================================================
Copyright (c) 2019 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using UnityEngine.Events;
using Vuforia;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
///
/// Changes made to this file could be overwritten when upgrading the Vuforia version.
/// When implementing custom event handler behavior, consider inheriting from this class instead.
/// </summary>
public class DefaultTrackableEventHandlerNew : MonoBehaviour
{

    //------------Begin Sound----------
    public AudioSource soundTarget;
    public AudioClip clipTarget;
    private AudioSource[] allAudioSources;

    //function to stop all sounds
    void StopAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

    //function to play sound
    void playSound(string ss)
    {
        clipTarget = (AudioClip)Resources.Load(ss);
        soundTarget.clip = clipTarget;
        soundTarget.loop = false;
        soundTarget.playOnAwake = false;
        soundTarget.Play();
    }

    //-----------End Sound------------

    public enum TrackingStatusFilter
    {
        Tracked,
        Tracked_ExtendedTracked,
        Tracked_ExtendedTracked_Limited
    }

    /// <summary>
    /// A filter that can be set to either:
    /// - Only consider a target if it's in view (TRACKED)
    /// - Also consider the target if's outside of the view, but the environment is tracked (EXTENDED_TRACKED)
    /// - Even consider the target if tracking is in LIMITED mode, e.g. the environment is just 3dof tracked.
    /// </summary>
    public TrackingStatusFilter StatusFilter = TrackingStatusFilter.Tracked_ExtendedTracked_Limited;
    public UnityEvent OnTargetFound;
    public UnityEvent OnTargetLost;


    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;
    protected bool m_CallbackReceivedOnce = false;

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();

        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterOnTrackableStatusChanged(OnTrackableStatusChanged);
        }

        //Register / add the AudioSource as object
        soundTarget = (AudioSource)gameObject.AddComponent<AudioSource>();
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.UnregisterOnTrackableStatusChanged(OnTrackableStatusChanged);
        }
    }

    void OnTrackableStatusChanged(TrackableBehaviour.StatusChangeResult statusChangeResult)
    {
        m_PreviousStatus = statusChangeResult.PreviousStatus;
        m_NewStatus = statusChangeResult.NewStatus;

        Debug.LogFormat("Trackable {0} {1} -- {2}",
            mTrackableBehaviour.TrackableName,
            mTrackableBehaviour.CurrentStatus,
            mTrackableBehaviour.CurrentStatusInfo);

        HandleTrackableStatusChanged();
    }

    protected virtual void HandleTrackableStatusChanged()
    {
        if (!ShouldBeRendered(m_PreviousStatus) &&
            ShouldBeRendered(m_NewStatus))
        {
            OnTrackingFound();
        }
        else if (ShouldBeRendered(m_PreviousStatus) &&
                 !ShouldBeRendered(m_NewStatus))
        {
            OnTrackingLost();
        }
        else
        {
            if (!m_CallbackReceivedOnce && !ShouldBeRendered(m_NewStatus))
            {
                // This is the first time we are receiving this callback, and the target is not visible yet.
                // --> Hide the augmentation.
                OnTrackingLost();
            }
        }

        m_CallbackReceivedOnce = true;
    }

    protected bool ShouldBeRendered(TrackableBehaviour.Status status)
    {
        if (status == TrackableBehaviour.Status.DETECTED ||
            status == TrackableBehaviour.Status.TRACKED)
        {
            // always render the augmentation when status is DETECTED or TRACKED, regardless of filter
            return true;
        }

        if (StatusFilter == TrackingStatusFilter.Tracked_ExtendedTracked)
        {
            if (status == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                // also return true if the target is extended tracked
                return true;
            }
        }

        if (StatusFilter == TrackingStatusFilter.Tracked_ExtendedTracked_Limited)
        {
            if (status == TrackableBehaviour.Status.EXTENDED_TRACKED ||
                status == TrackableBehaviour.Status.LIMITED)
            {
                // in this mode, render the augmentation even if the target's tracking status is LIMITED.
                // this is mainly recommended for Anchors.
                return true;
            }
        }

        return false;
    }

    protected virtual void OnTrackingFound()
    {
        if (mTrackableBehaviour)
        {
            var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

            // Enable rendering:
            foreach (var component in rendererComponents)
                component.enabled = true;

            // Enable colliders:
            foreach (var component in colliderComponents)
                component.enabled = true;

            // Enable canvas':
            foreach (var component in canvasComponents)
                component.enabled = true;

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");

            //Play Sound, IF detect an target

            if (mTrackableBehaviour.TrackableName == "Target_A")
            {
                playSound("Audio/apple");
            }

            if (mTrackableBehaviour.TrackableName == "Target_B")
            {
                playSound("Audio/ball");
            }

            if (mTrackableBehaviour.TrackableName == "Target_C")
            {
                playSound("Audio/carrot");
            }

            if (mTrackableBehaviour.TrackableName == "Target_D")
            {
                playSound("Audio/duck");
            }

            if (mTrackableBehaviour.TrackableName == "Target_E")
            {
                playSound("Audio/elephant");
            }

            if (mTrackableBehaviour.TrackableName == "Target_F")
            {
                playSound("Audio/fish");
            }

            if (mTrackableBehaviour.TrackableName == "Target_G")
            {
                playSound("Audio/grapes");
            }

            if (mTrackableBehaviour.TrackableName == "Target_H")
            {
                playSound("Audio/hammer");
            }

            if (mTrackableBehaviour.TrackableName == "Target_I")
            {
                playSound("Audio/ice cream");
            }

            if (mTrackableBehaviour.TrackableName == "Target_J")
            {
                playSound("Audio/jug");
            }

            if (mTrackableBehaviour.TrackableName == "Target_K")
            {
                playSound("Audio/kite");
            }

            if (mTrackableBehaviour.TrackableName == "Target_L")
            {
                playSound("Audio/ladybug");
            }

            if (mTrackableBehaviour.TrackableName == "Target_M")
            {
                playSound("Audio/mango");
            }

            if (mTrackableBehaviour.TrackableName == "Target_N")
            {
                playSound("Audio/nest");
            }

            if (mTrackableBehaviour.TrackableName == "Target_O")
            {
                playSound("Audio/orange");
            }

            if (mTrackableBehaviour.TrackableName == "Target_P")
            {
                playSound("Audio/penguin");
            }

            if (mTrackableBehaviour.TrackableName == "Target_Q")
            {
                playSound("Audio/quill");
            }

            if (mTrackableBehaviour.TrackableName == "Target_R")
            {
                playSound("Audio/roses");
            }

            if (mTrackableBehaviour.TrackableName == "Target_S")
            {
                playSound("Audio/seal");
            }

            if (mTrackableBehaviour.TrackableName == "Target_T")
            {
                playSound("Audio/turtle");
            }

            if (mTrackableBehaviour.TrackableName == "Target_U")
            {
                playSound("Audio/umbrella");
            }

            if (mTrackableBehaviour.TrackableName == "Target_V")
            {
                playSound("Audio/van");
            }

            if (mTrackableBehaviour.TrackableName == "Target_W")
            {
                playSound("Audio/whale");
            }

            if (mTrackableBehaviour.TrackableName == "Target_X")
            {
                playSound("Audio/x-mas tree");
            }

            if (mTrackableBehaviour.TrackableName == "Target_Y")
            {
                playSound("Audio/yo-yo");
            }

            if (mTrackableBehaviour.TrackableName == "Target_Z")
            {
                playSound("Audio/zebra");
            }
            // Add sound here
        }

        if (OnTargetFound != null)
            OnTargetFound.Invoke();
    }

    protected virtual void OnTrackingLost()
    {
        if (mTrackableBehaviour)
        {
            var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

            // Disable rendering:
            foreach (var component in rendererComponents)
                component.enabled = false;

            // Disable colliders:
            foreach (var component in colliderComponents)
                component.enabled = false;

            // Disable canvas':
            foreach (var component in canvasComponents)
                component.enabled = false;

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");

            //Stop All Sounds if Target Lost
            StopAllAudio();

        }

        if (OnTargetLost != null)
            OnTargetLost.Invoke();
    }
}
