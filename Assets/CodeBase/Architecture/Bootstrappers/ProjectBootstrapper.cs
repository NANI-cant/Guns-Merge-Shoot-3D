using Architecture.Services.General.Impl;
using Architecture.Services.PersistentProgress.Impl;
using Gameplay.Economic;
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
		    
		    BindService<Bank>();
		    
		    BindService<PrefsSaveLoadService>();
		    BindService<PersistentProgressService>();
	    }

        private void BindService<TService>() 
			=> Container.BindInterfacesAndSelfTo<TService>().AsSingle().NonLazy();
    }
}