using Zenject;

public class MainGameInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        Container.Bind<DataManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CameraManager>().FromComponentInHierarchy().AsSingle();

        /// <summary>
        /// Genelde projeler IoC yi bu sekilde kullanirim 
        /// örn reklam sdk sý  i degisecek oldugu durumda tek bir yerden yonetim yapiyorum
        /// A/B test yapilirkende buyuk kalaylýklar sagliyor
        /// </summary>
        /// 
        //Container.Bind<IAdsManager>().To<IronSouceAdsManager>().FromComponentInHierarchy().AsSingle();
        //Container.Bind<IAdsManager>().To<AdmobAdsManager>().FromComponentInHierarchy().AsSingle();
    }
}
