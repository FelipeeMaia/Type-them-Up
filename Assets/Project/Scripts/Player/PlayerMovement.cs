using UnityEngine;
using DG.Tweening;
using System;

namespace TypemUp.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Rows")]
        [SerializeField] float[] _rowsPositions;
        [SerializeField] int _activeRow = 2;

        [Header("Animation")]
        [SerializeField] float _duration;
        [SerializeField] float _jumpPower;

        public Action<int> OnRowChange;

        bool canMove = true;

        private void TryChangeRows(int direction)
        {
            if (!canMove) return;

            int lastRow = _rowsPositions.Length - 1;
            int imaginaryRow = _activeRow + direction;
            int newRow = Mathf.Clamp(imaginaryRow, 0, lastRow);

            if (newRow != _activeRow)
            {
                float yPos = transform.position.y;
                float xPos = _rowsPositions[newRow];

                Vector2 newPosition = new Vector2(xPos, yPos);

                canMove = false;
                transform.DOJump(newPosition, _jumpPower, 1, _duration)
                    .OnComplete(() => ChangeRow(newRow));
            }
        }

        private void ChangeRow(int newRow)
        {
            _activeRow = newRow;
            canMove = true;

            OnRowChange?.Invoke(_activeRow);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                TryChangeRows(-1);
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                TryChangeRows(1);
        }

        private void Start()
        {
            canMove = true;

            _activeRow = 2;

            float yPos = transform.position.y;
            float xPos = _rowsPositions[_activeRow];

            Vector2 newPosition = new Vector2(xPos, yPos);
            transform.position = newPosition;
        }
    }
} 