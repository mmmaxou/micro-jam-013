using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
{

    /// <summary> Instance of the singleton </summary>
    private static T instance = null;

    /// <summary> If the singleton instance no longer in the game </summary>
    private static bool isDestroyed = false;

    /// <summary> Lock object for more faster multithreaded systems </summary>
    private static readonly object lockObj = new object();


    /// ========================================
    /// PUBLIC METHODS
    /// ========================================

    /// <summary> The singleton instance </summary>
    /// <exception cref="System.Exception"> None or more than one singleton were found on the scene </exception>
    public static T Instance
    {
        get
        {
            // If singleton instance already destroyed
            if (isDestroyed)
            {
                instance = null;
                isDestroyed = false;
            }
            // lock for multithreaded
            lock (lockObj)
            {
                // if the singleton is not set
                if (instance == null)
                {
                    // Find singleton gameobject instances
                    var instances = FindObjectsOfType<T>();

                    // Throw error if none or more than one singleton were found on the scene 
                    if (instances.Length == 0)
                    {
                        throw new System.Exception("[Singleton] The scene doesn't contain any object with the " + typeof(T).Name + " script");
                    }
                    else if (instances.Length > 1)
                    {
                        throw new System.Exception("[Singleton] The scene must have a single object with the " + typeof(T).Name + " script." + instances.Length + "different " + typeof(T).Name + " script was found in the scene.");
                    }

                    // set the instance 
                    instance = instances[0];
                }
                return instance;
            }
        }
    }


    /// ========================================
    /// UNITY METHODS
    /// ========================================

    /// <summary> Awake is called when the script instance is being loaded : set the singleton </summary>
    /// <exception cref="System.Exception"> None or more than one singleton were found on the scene </exception>
    virtual protected void Awake()
    {
        T getInstance = Instance;
    }

    /// <summary> Function sent to all game objects before the application quits </summary>
    protected void OnApplicationQuit()
    {
        isDestroyed = true;
    }

    /// <summary> Function sent to the game objects before destroying it </summary>
    protected void OnDestroy()
    {
        isDestroyed = true;
    }
}