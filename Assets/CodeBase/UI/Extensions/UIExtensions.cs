using System.Linq;
using UnityEngine;

namespace UI.Extensions {
    public static class UIExtensions {
        public static bool IsOverlap(this RectTransform rect, RectTransform other) {
            bool result = false;
            
            Vector3[] otherCorners = new Vector3[4];
            other.GetWorldCorners(otherCorners);

            foreach (var corner in otherCorners) {
                bool isCornerInRect = rect.IsPointIn(corner);
                if (isCornerInRect) return true;
            }

            return result;
        }

        public static bool IsPointIn(this RectTransform rect, Vector2 point, float accuracy = 0.001f) {
            Vector3[] rectCorners = new Vector3[4];
            rect.GetWorldCorners(rectCorners);
            
            float pointArea = QuadArea(rectCorners, point);
            float rectArea = QuadArea(rectCorners, MiddlePoint(rectCorners));

            return Mathf.Abs(pointArea - rectArea) < accuracy;
        }
        
        public static bool IsPointOut(this RectTransform rect, Vector2 point, float accuracy = 0.001f) => !IsPointIn(rect, point, accuracy);

        public static Vector2 Center(this RectTransform rect) {
            Vector3[] corners = new Vector3[4];
            rect.GetWorldCorners(corners);

            return MiddlePoint(corners);
        }

        private static float QuadArea(Vector3[] corners, Vector2 middlePoint) {
            float[] areas = new float[4];
            for (int i = 0; i < 4; i++) {
                Vector3 first = corners[i % 4];
                Vector3 second = corners[(i + 1) % 4];
                Vector3 third = middlePoint;

                areas[i] = TriangleArea(second, first, third);
            }

            return areas.Sum();
        }

        private static float TriangleArea(Vector2 second, Vector2 first, Vector2 third) {
            return 1f / 2f * Mathf.Abs((second.x - first.x) * (third.y - first.y) - (third.x - first.x) * (second.y - first.y));
        }

        private static Vector3 MiddlePoint(Vector3[] points) {
            Vector3 sum = Vector3.zero;
            foreach (var point in points) {
                sum += point;
            }

            return sum / points.Length;
        }
    }
}