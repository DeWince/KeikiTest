using UnityEngine;
using System.Collections;

public class Activator : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    // Use this for initialization
    void Awake()
    {
        obj.SetActive(true);
    }

}
