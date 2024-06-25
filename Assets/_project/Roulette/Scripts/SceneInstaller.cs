using Zenject;

namespace ValeryPopov.RoulettePrototype
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Notification>().FromComponentInHierarchy().AsSingle();
            Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
        }
    }
}