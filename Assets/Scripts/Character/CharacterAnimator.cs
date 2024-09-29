using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterAnimator
{
    [Inject] private AssetLoader assetLoader;
    private Dictionary<string, CharacterData> characterDataDictionary = new Dictionary<string, CharacterData>();
    private GameObject currentCharacterObject;
    private CharacterFactory characterFactory;
    private AnimationHandler animationHandler;
    private CharacterPositions characterPositions;

    public void OnCharacterEnterHandler(string characterName, string direction)
    {
        PlayCharacterEnterAnimation(characterName, direction).Forget();
    }
    public CharacterAnimator(CharacterPositions positions,float moveDuration)
    {
        characterPositions = positions;
        characterFactory = new CharacterFactory(characterPositions.LeftStartPosition, characterPositions.RightStartPosition);
        animationHandler = new AnimationHandler(moveDuration);
    }

    private async UniTask PlayCharacterEnterAnimation(string characterName, string direction)
    {

        if (direction == "N") //на случай появления авторского текста в истории
        {
            if (!characterDataDictionary.ContainsKey(characterName))
            {
                var newCharacterData = new CharacterData(null, direction);
                characterDataDictionary[characterName] = newCharacterData;
            }
            return;
        }

        Sprite characterSprite = await assetLoader.LoadAsset<Sprite>(characterName);

        if (currentCharacterObject == null)
        {
            currentCharacterObject = characterFactory.CreateCharacter(characterName, characterSprite, direction);
            var characterData = new CharacterData(currentCharacterObject, direction);
            characterDataDictionary[characterName] = characterData;

            Vector2 targetPosition = characterPositions.GetTargetPosition(direction);
            await animationHandler.MoveCharacter(currentCharacterObject, targetPosition);
            return;
        }

        if (currentCharacterObject.name != characterName)
        {
            var previousDirection = characterDataDictionary[currentCharacterObject.name].Direction;
            Vector2 previousStartPosition = characterPositions.GetStartPosition(previousDirection);
            await animationHandler.MoveCharacter(currentCharacterObject, previousStartPosition);

            if (!characterDataDictionary.TryGetValue(characterName, out CharacterData newCharacterData))
            {
                GameObject newCharacterObject = characterFactory.CreateCharacter(characterName, characterSprite, direction);
                newCharacterData = new CharacterData(newCharacterObject, direction);
                characterDataDictionary[characterName] = newCharacterData;
            }

            Vector2 targetPosition = characterPositions.GetTargetPosition(direction);
            await animationHandler.MoveCharacter(newCharacterData.CharacterObject, targetPosition);

            currentCharacterObject = newCharacterData.CharacterObject;
            return;
        }

        Debug.Log($"Character {characterName} is already active.");
    }
}
