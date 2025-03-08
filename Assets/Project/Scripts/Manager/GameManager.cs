using TypemUp.Enemy;
using TypemUp.Player;
using UnityEngine;

namespace TypemUp.Management
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] PlayerTyping _playerTyping;
        [SerializeField] PlayerMovement _playerMovement;

        [SerializeField] EnemyTarget[] _targets;
        [SerializeField] int activeRow;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _playerTyping.OnType += AttackActiveTarget;
            _playerMovement.OnRowChange += ChangeRows;
        }

        void AttackActiveTarget(char letter)
        {
            if (activeRow >= _targets.Length) return;

            _targets[activeRow].CheckLetter(letter);
        }

        void ChangeRows(int newRow)
        {
            activeRow = newRow;
        }
    }
}