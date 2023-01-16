using System;
using UnityEngine;

namespace GameSystem.Views
{
    public class StartView : MonoBehaviour
    {
        public event EventHandler PlayClicked;

        public void Play()
        {
            OnPlayClicked(EventArgs.Empty);
        }

        public void OnPlayClicked(EventArgs eventArgs)
        {
            var handler = PlayClicked;
            handler?.Invoke(this, eventArgs);
        }
    }
}

