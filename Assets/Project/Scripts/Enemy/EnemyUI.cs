using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TypemUp.Enemy.UI
{
    public class EnemyUI : MonoBehaviour
    {
        [SerializeField] TMP_Text _lettersDisplay;
        [SerializeField] EnemyTarget _target;
        [SerializeField] Vector3 _offset = new Vector3(0, 2, 0);

        Transform _targetTransform;
        private RectTransform _uiElement;
        private Camera _camera;    

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public void SetUp(EnemyTarget target)
        {
            this._target = target;

            _target.OnLettersChange += UpdateDisplay;
            _targetTransform = _target.transform;

            _uiElement = GetComponent<RectTransform>();
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_targetTransform != null)
            {
                // Convert world position to screen point
                Vector3 worldPosition = _targetTransform.position + _offset;
                Vector3 screenPosition = _camera.WorldToScreenPoint(worldPosition);

                // Check if object is behind the camera
                if (screenPosition.z < 0)
                    return;

                // Convert screen position to UI canvas position
                RectTransform canvasRect = _uiElement.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
                Vector2 viewportPosition = _camera.WorldToViewportPoint(worldPosition);

                // Use viewport position to ensure proper alignment
                Vector2 uiPosition = new Vector2(
                    (viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
                    (viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)
                );

                // Set the UI element's position
                _uiElement.anchoredPosition = uiPosition;
            }
        }

        // Update is called once per frame
        void UpdateDisplay(List<char> letters)
        {
            string newText = "";
            int count = letters.Count;

            for (int i = 0; i < letters.Count; i++)
            {
                newText += letters[i];

                if (i < count - 1)
                    newText += ", ";
            }

            _lettersDisplay.text = newText;
        }
    }
}