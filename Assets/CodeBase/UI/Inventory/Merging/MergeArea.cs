using System.Collections.Generic;
using Architecture.Services.AssetProviding;
using Architecture.Services.Factories;
using Architecture.Services.Gameplay;
using Metric.Weapons;
using PersistentProgress;
using UI.Extensions;
using UnityEngine;

namespace UI.Inventory.Merging {
    [RequireComponent(typeof(RectTransform))]
    public class MergeArea : MonoBehaviour, IProgressReader, IProgressWriter {
        private readonly List<MergeWeapon> _mergeWeapons = new();
        
        private MergeGrid _grid;
        private IUIFactory _uiFactory;
        private IMetricProvider _metricProvider;
        private PlayerPointer _playerPointer;

        public RectTransform Transform { get; private set; }

        private void Awake() {
            Transform = GetComponent<RectTransform>();
        }

        public void Construct(
            MergeGrid mergeGrid,
            IUIFactory uiFactory,
            IMetricProvider metricProvider,
            PlayerPointer playerPointer
        ) {
            _grid = mergeGrid;
            _uiFactory = uiFactory;
            _metricProvider = metricProvider;
            _playerPointer = playerPointer;
        }

        public void Initialize(int[] lvls, Vector2[] positions) {
            for (int i = 0; i < lvls.Length; i++) {
                var mergeWeapon = _uiFactory
                    .CreateMergeWeapon(lvls[i], _grid.FindByPosition(positions[i]), Transform, this)
                    .GetComponent<MergeWeapon>();
                _mergeWeapons.Add(mergeWeapon);
            }
        }
        
        public void Read(IReadOnlyPlayerProgress playerProgress) {
            foreach (var weapon in _mergeWeapons) {
                Destroy(weapon.gameObject);
            }
            _mergeWeapons.Clear();
            
            Initialize(playerProgress.InventoryWeapons, playerProgress.InventoryPositions);
        }

        public void Write(PlayerProgress playerProgress) {
            int[] weaponLvls = new int[_mergeWeapons.Count];
            Vector2[] positions = new Vector2[_mergeWeapons.Count];
            for (int i = 0; i < weaponLvls.Length; i++) {
                weaponLvls[i] = _mergeWeapons[i].WeaponData.Level;
                positions[i] = _mergeWeapons[i].Transform.anchoredPosition;
            }

            playerProgress.InventoryWeapons = weaponLvls;
            playerProgress.InventoryPositions = positions;
        }

        public void AddWeapon(WeaponData weaponData) {
            if(_mergeWeapons.Count == _grid.MergeCells.Length) return;

            foreach (var cell in _grid.MergeCells) {
                if (!cell.IsEmpty) continue;
                
                var weapon = _uiFactory
                    .CreateMergeWeapon(weaponData.Level, cell, Transform, this)
                    .GetComponent<MergeWeapon>();
                _mergeWeapons.Add(weapon);
                break;
            }
        }
        
        public void OnWeaponDragged(MergeWeapon weapon, Vector2 position) {
            _grid.HandleCellsSelection(position);
        }
        
        public void OnWeaponDropped(MergeWeapon weapon, Vector2 position) {
            MergeResult mergeResult = new MergeResult(MergeType.Translation, weapon, null);
            
            foreach (var other in _mergeWeapons) {
                if (other == weapon) continue;
                if (other.Transform.IsPointOut(position)) continue;

                mergeResult = HandleMerge(weapon, other);
                break;
            }
            
            ReactToMergeTrying(position, mergeResult);

            _grid.UnSelectAllCells();
        }

        private void ReactToMergeTrying(Vector2 position, MergeResult mergeResult) {
            switch (mergeResult.MergeType) {
                case MergeType.Success: {
                    mergeResult.Active.Detach();
                    _mergeWeapons.Remove(mergeResult.Active);
                    Destroy(mergeResult.Active.gameObject);
                    break;
                }
                case MergeType.Translation: {
                    if (_grid.TryFindOverlappingCell(position, out MergeCell targetCell)) {
                        mergeResult.Active.Attach(targetCell);
                    }
                    mergeResult.Active.ReturnToGrid();
                    break;
                }
                case MergeType.Swap: {
                    mergeResult.Active.Swap(mergeResult.Passive);
                    break;
                }
            }
        }

        private MergeResult HandleMerge(MergeWeapon weapon, MergeWeapon other) {
            if (other.WeaponData.Level != weapon.WeaponData.Level) return new MergeResult(MergeType.Swap, weapon, other);
            
            return TryUpdateWeapon(other) 
                ? new MergeResult(MergeType.Success, weapon, other) 
                : new MergeResult(MergeType.Swap, weapon, other);
        }

        private bool TryUpdateWeapon(MergeWeapon other) {
            if (other.WeaponData.Level + 1 >= _metricProvider.WeaponData.Length) return false;

            WeaponData nextWeapon = _metricProvider.WeaponData[other.WeaponData.Level + 1];
            other.Upgrade(nextWeapon);
            _playerPointer.Player.WeaponHolder.SetWeapon(nextWeapon);
            
            return true;
        }
    }
}
