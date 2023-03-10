using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameServiceBase
{
    private Dictionary<Type, IGameServices> _services;

    public GameServiceBase()
    {
        _services = new Dictionary<Type, IGameServices>();
    }

    public void AddService<T>(IGameServices service)
    {
        if (service is T)
        {
            _services.Add(typeof(T), service);
        }
        else
        {
            throw new Exception("Service " + service + " have not implemented interface ");
        }
    }

    public T GetService<T>()
    {
        try
        {
            return (T)_services[typeof(T)];
        }
        catch (KeyNotFoundException)
        {
            throw new Exception("Service " + typeof(T) + " is not registered.");
        }
    }

    public void InitServices()
    {
        foreach (var services in _services.Values)
            services.Init();
    }

    public void UpdateServices()
    {
        foreach (var services in _services.Values)
            services.Update();
    }

    public void FixedUpdateServices()
    {
        foreach (var services in _services.Values)
            services.FixedUpdate();
    }

    public void ReleaseServices()
    {
        foreach (var services in _services.Values)
            services.Release();
    }
}
