using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeCharacter : MonoBehaviour
{
    [SerializeField] private List<Character> characters;

    [HideInInspector] public UnityAction <Character> characterChanged;

    public int CharactersCount { get; private set; }

    private void Start()
    {
        characters.AddRange(GameObject.FindObjectsOfType<Character>());
        for(int i = 1; i<=characters.Count-1; i++)
        {
            characters[i].GetComponent<Character>().enabled = false;
        }
        characterChanged.Invoke(characters[0]);
        CharactersCount = characters.Count;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Change();
        }
    }

    private void Change()
    {
        characters[0].GetComponent<Character>().enabled = false;
        characters[1].GetComponent<Character>().enabled = true;

        Character lastCharacter = characters[0];

        for (int i = 1; i<=characters.Count-1; i++)
        {
            characters[i - 1] = characters[i];
        }
        characters[characters.Count - 1] = lastCharacter;
        characterChanged?.Invoke(characters[0]);
    }

}
