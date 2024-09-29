using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller
{
    public CharacterPositions characterPositions;
    public float moveDuration;
    private DialogManager dialogManager;
    private CharacterAnimator characterAnimator;

    public override void InstallBindings()
    {
        Container.Bind<AssetLoader>().AsSingle();
        Container.Bind<DialogParser>().AsSingle();
        Container.Bind<LocalizationManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DialogPanel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DialogManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CharacterPositions>().FromInstance(characterPositions).AsSingle();
        Container.Bind<float>().FromInstance(moveDuration).AsSingle();
        Container.Bind<CharacterAnimator>()
            .AsSingle()
            .WithArguments(characterPositions, moveDuration);

    }

    public override void Start()
    {
        dialogManager = Container.Resolve<DialogManager>();
        characterAnimator = Container.Resolve<CharacterAnimator>();
        dialogManager.OnCharacterEnter += characterAnimator.OnCharacterEnterHandler;
    }

    private void OnDestroy()
    {
        if (dialogManager != null && characterAnimator != null)
        {
            dialogManager.OnCharacterEnter -= characterAnimator.OnCharacterEnterHandler;
        }
    }
}