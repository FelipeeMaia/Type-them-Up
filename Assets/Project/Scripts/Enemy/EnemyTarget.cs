using System;
using System.Collections.Generic;
using UnityEngine;

namespace TypemUp.Enemy
{
    public class EnemyTarget : MonoBehaviour
    {
        private List<char> _letters;
        private int _listLength;

        public Action<List<char>> OnLettersChange;

        public void SetUp(int letterQuantity)
        {
            _listLength = letterQuantity;
            SetLetters();
        }

        public void CheckLetter(char typed)
        {
            if(typed == _letters[0])
            {
                _letters.RemoveAt(0);

                if(_letters.Count > 0)
                {
                    OnLettersChange?.Invoke(_letters);
                }
                else
                {
                    Debug.Log("Alvo derrotado");
                    Destroy(gameObject);
                }
            }
        }

        void SetLetters()
        {
            _letters = new List<char>();

            for (int i = 0; i < _listLength; i++)
            {
                char randomLetter = GetRandomLetter();
                _letters.Add(randomLetter);
            }

            OnLettersChange?.Invoke(_letters);
        }

        char GetRandomLetter()
        {
            // Generate a random number between 0 and 25 (inclusive)
            int randomIndex = UnityEngine.Random.Range(0, 26);

            // Convert the random index to a letter (A-Z)
            char randomLetter = (char)('A' + randomIndex);

            return randomLetter;
        }
    }
}