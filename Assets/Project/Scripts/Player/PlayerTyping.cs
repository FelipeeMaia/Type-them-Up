using System;
using UnityEngine;

namespace TypemUp.Player
{
    public class PlayerTyping : MonoBehaviour
    {
        public Action<Char> OnType;

        void Update()
        {
            // Check if any key was pressed
            if (Input.anyKeyDown)
            {
                // Get the input string
                if(CheckForLetter(out char character))
                {
                    OnType?.Invoke(character);
                    Debug.Log("Player typed a letter: " + character);
                }
            }
        }

        private bool CheckForLetter(out char Char)
        {
            string input = Input.inputString;

            // Check if the input is a letter
            if (!string.IsNullOrEmpty(input))
            {
                char character = input.ToUpper()[0];
                if (char.IsLetter(character))
                {
                    Char = character;
                    return true;
                }
            }

            Char = ' ';
            return false;
        }
    }
}