using WKosArch.Common.Utils.Async;
using WKosArch.Domain.Contexts;
using UnityEngine;

namespace WKosArch.Game
{
    [RequireComponent(typeof(ProjectContext))]
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private ProjectContext _projectContext;

        private void Start()
        {
            Game.StartGameAsync(_projectContext).RunAsync();
        }

#if UNITY_EDITOR
        private void Reset()
        {
            if (_projectContext == null)
            {
                _projectContext = GetComponent<ProjectContext>();
            }
        } 
#endif
    }


}