using System;
using UI.Extensions;
using UnityEngine;

namespace UI.Inventory.Merging {
    [RequireComponent(typeof(RectTransform))]
    public class MergeGrid : MonoBehaviour {
        public RectTransform Transform { get; private set; }
        public MergeCell[] MergeCells { get; private set; }

        private void Awake() {
            Transform = GetComponent<RectTransform>();
            MergeCells = GetComponentsInChildren<MergeCell>();
        }

        public void HandleCellsSelection(Vector2 point) {
            UnSelectAllCells();

            if (!TryFindOverlappingCell(point, out MergeCell targetCell)) return;
            
            targetCell.Select();
        }

        public MergeCell FindByPosition(Vector2 anchoredPosition) {
            foreach (var cell in MergeCells) {
                if (cell.Transform.anchoredPosition == anchoredPosition) {
                    return cell;
                }
            }

            throw new ArgumentException($"There is no cell is position {anchoredPosition}");
        }

        public bool TryFindOverlappingCell(Vector2 point, out MergeCell targetCell) {
            targetCell = null;
            foreach (var cell in MergeCells) {
                if (cell.Transform.IsPointOut(point)) continue;
                    
                targetCell = cell;
                return true;
            }

            return false;
        }

        public void UnSelectAllCells() {
            foreach (var cell in MergeCells) {
                cell.UnSelect();
            }
        }
    }
}