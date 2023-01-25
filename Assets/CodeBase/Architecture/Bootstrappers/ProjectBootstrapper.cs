using Architecture.Services.Factories.Impl;
using Architecture.Services.General;
using Architecture.Services.General.Impl;
using Architecture.Services.PersistentProgress.Impl;
using Gameplay.Economic;
using Zenject;

namespace Architecture.Bootstrappers {
    public class ProjectBootstrapper : MonoInstaller, ICoroutineRunner{
	    public override void InstallBindings() {
		    Container.BindInstance(0).WhenInjectedInto<RandomService>();

		    Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle().NonLazy();
		    
		    BindService<RandomService>();
		    BindService<UnityDestroyProvider>();
		    BindService<UnityInstantiateProvider>();
		    BindService<UnitySceneLoadService>();
		    BindService<UnityTimeProvider>();
		    
		    BindService<Bank>();
		    
		    BindService<PrefsSaveLoadService>();
		    BindService<PersistentProgressService>();
		    BindService<AutoProgressSubscriber>();
		    
		    BindService<SchedulerFactory>();
	    }

        private void BindService<TService>() 
			=> Container.BindInterfacesAndSelfTo<TService>().AsSingle().NonLazy();
    }
}