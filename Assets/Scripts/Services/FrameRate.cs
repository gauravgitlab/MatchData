using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface IFrameRate
{
    void SetTargetFrameRate(int targetFrameRate);
}

public class FrameRate : IFrameRate, IGameServices
{
    public void Init() 
    {
        SetTargetFrameRate(1);        
    }

    public void SetTargetFrameRate(int targetFrameRate)
    {
        Application.targetFrameRate = targetFrameRate;
    }

    public void Update() { }
    public void FixedUpdate() { }
    public void Release() { }
}
