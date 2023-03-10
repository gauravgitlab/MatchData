using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainApp : MonoBehaviour
{
    public static MainApp Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        GameClient.Instance.InitServices();
    }

    private void Update()
    {
        GameClient.Instance.UpdateServices();
    }

    private void FixedUpdate()
    {
        GameClient.Instance.FixedUpdateServices();
    }

    private void OnDestroy()
    {
        GameClient.Instance.ReleaseServices();
    }
}
