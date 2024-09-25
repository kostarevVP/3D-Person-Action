using UnityEngine;
using WKosArch.Extentions;

public class LogUnityLifeCycle : MonoBehaviour
{
    [SerializeField]
    private bool _isLogUpdateMethods;

    void Awake() => 
        Log.PrintYellow($"Awake {this.name}");

    void OnEnable() => 
        Log.PrintYellow($"OnEnable {this.name}");

    void Start() => 
        Log.PrintYellow($"Start {this.name}");

    void FixedUpdate()
    {
        if (_isLogUpdateMethods)
            Log.PrintYellow($"FixedUpdate {this.name}");
    }

    void Update()
    {
        if (_isLogUpdateMethods)
            Log.PrintYellow($"Update {this.name}");
    }

    void LateUpdate()
    {
        if (_isLogUpdateMethods)
            Log.PrintYellow($"LateUpdate {this.name}");
    }

    void OnDisable() => 
        Log.PrintYellow($"OnDisable {this.name}");

    void OnDestroy() => 
        Log.PrintYellow($"OnDestroy {this.name}");
}
