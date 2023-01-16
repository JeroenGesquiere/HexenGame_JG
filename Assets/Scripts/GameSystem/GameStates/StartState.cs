using GameSystem.Views;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem.GameStates
{
    public class StartState : State
    {
        private StartView _startView;
        public override void OnEnter()
        {
            var asyncOperation = SceneManager.LoadSceneAsync("Start", LoadSceneMode.Additive);
            asyncOperation.completed += InitializeScene;
        }

        private void InitializeScene(UnityEngine.AsyncOperation obj)
        {
            _startView = GameObject.FindObjectOfType<StartView>();
            if (_startView != null)
                _startView.PlayClicked += OnPlayClicked;
        }

        private void OnPlayClicked(object sender, EventArgs e)
        {
            StateMachine.MoveTo(States.Play);
        }

        public override void OnExit()
        {
            if (_startView != null)
                _startView.PlayClicked -= OnPlayClicked;

            SceneManager.UnloadSceneAsync("Start");
        }
    }
}


