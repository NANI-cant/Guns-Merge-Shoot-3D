using Architecture.Services.General.Impl;
using Zenject;

namespace Architecture.Bootstrappers {
    public class ProjectBootstrapper : MonoInstaller{
	    public override void InstallBindings() {
		    Container.BindInstance(0).WhenInjectedInto<RandomService>();
		    
		    BindService<RandomService>();
		    BindService<UnityDestroyProvider>();
		    BindService<UnityInstantiateProvider>();
		    BindService<UnitySceneLoadService>();
		    BindService<UnityTimeProvider>();
	    }

        private void BindService<TService>() 
			=> Container.BindInterfacesAndSelfTo<TService>().AsSingle().NonLazy();
    }
}