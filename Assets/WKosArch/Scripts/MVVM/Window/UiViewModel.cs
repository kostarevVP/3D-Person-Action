using WKosArch.MVVM;
using System.Reactive.Linq;
using System;
using WKosArch.Services.UIService.Common;
using WKosArch.Services.UIService.UI;
using WKosArch.DependencyInjection;

namespace WKosArch
{
    public abstract class UiViewModel : IViewModel
    {
        public UILayer TargetLayer { get; set; }
        public IObservable<bool> Opened { get; }
        public IObservable<bool> Closed { get; }
        public IObservable<bool> Hided { get; }

        public bool IsHide { get; private set; }

        public IDiContainer DiContainer => _dIContainer;
        public IUserInterface UI => _userInterface;

        private IDiContainer _dIContainer;
        private IUserInterface _userInterface;

        private event Action<bool> _opened;
        private event Action<bool> _closed;
        private event Action<bool> _hided;


        protected UiViewModel()
        {
            Opened = Observable.FromEvent<bool>(a => _opened += a, a => _opened -= a);
            Closed = Observable.FromEvent<bool>(a => _closed += a, a => _closed -= a);
            Hided = Observable.FromEvent<bool>(a => _hided += a, a => _hided -= a);
        }

        public void Inject(IDiContainer dIContainer, IUserInterface userInterface)
        {
            _dIContainer = dIContainer;
            _userInterface = userInterface;
        }

        public virtual void Subscribe() { }
        public virtual void Unsubscribe() { }

        public void Open(bool forced = false)
        {
            Subscribe();
            _opened?.Invoke(forced);
            IsHide = false;
        }

        public void Close(bool forced = false)
        {
            Unsubscribe();
            _closed?.Invoke(forced);
        }

        public void Hide(bool forced = false)
        {
            _hided?.Invoke(forced);
            IsHide = true;
        }
    }
}
