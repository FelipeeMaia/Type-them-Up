using UnityEngine;
using TypemUp.Enemy;
using TypemUp.Enemy.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Random = UnityEngine.Random;

namespace TypemUp.Management
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] EnemyTarget _enemyPrefab;
        [SerializeField] EnemyUI _uiPrefab;

        [Header("Configs")]
        [SerializeField] float _ySpawn;
        [SerializeField] int _timeBetweenSpawns;
        [SerializeField] int _minLetters;
        [SerializeField] int _maxLetters;

        [Header("Containers")]
        [SerializeField] Transform _enemyContainer;
        [SerializeField] Transform _uiContainer;

        private List<float> _rowsPositions;
        private bool _isSpawning;

        public Action<EnemyTarget, int> OnSpawn;

        public void StartSpawning(List<float> rowsPositions)
        {
            if (_isSpawning) return;

            _isSpawning = true;
            _rowsPositions = rowsPositions;

            Spawn();
        }

        public void StopSpawning()
        {
            _isSpawning = false;
        }

        private async void Spawn()
        {
            while(_isSpawning)
            {
                // Get wich row the enemy will be spawned
                int row = Random.Range(0, _rowsPositions.Count);
                var position = new Vector2(_rowsPositions[row], _ySpawn);

                // Spawns enemy
                var enemy = Instantiate(_enemyPrefab, position, Quaternion.identity, _enemyContainer)
                    .GetComponent<EnemyTarget>();

                // Spawns it's UI and set it up
                var ui = Instantiate(_uiPrefab, _uiContainer).GetComponent<EnemyUI>();
                ui.SetUp(enemy);

                // Randomizes how many letter the enemy will have and set it up
                int lettersQuantity = Random.Range(_minLetters, _maxLetters + 1);
                enemy.SetUp(lettersQuantity);

                OnSpawn?.Invoke(enemy, row);
                await Task.Delay(_timeBetweenSpawns);
            }
        }
    }
}