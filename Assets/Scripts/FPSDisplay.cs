using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BSports
{
    public class FPSDisplay : MonoBehaviour
    {
        private TextMeshProUGUI _fpsText;

        private void Awake()
        {
            _fpsText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            var msec = Time.deltaTime * 1000.0f;
            var fps = 1.0f / Time.deltaTime;
            var text = string.Format("{0:0.0} ms, {1:0.} fps", msec, fps);
            _fpsText.text = text;
        }
    }
}

