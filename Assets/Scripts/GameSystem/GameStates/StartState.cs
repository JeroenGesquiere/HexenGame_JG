using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

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

