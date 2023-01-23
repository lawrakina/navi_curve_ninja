using PathCreation;
using UnityEngine;


namespace pj99.Code.Extensions{
    public static class PathExtensions{
        public static Transform Transform{ get; set; }

        public static VertexPath CreatePath(Vector3[] points, bool closedPath = false){
            return GeneratePath(points, closedPath);

        }

        public static VertexPath CreatePath(Vector2[] points, bool closedPath = false){
            return GeneratePath(points, closedPath);

        }

        private static VertexPath GeneratePath(Vector2[] points, bool closedPath){
            // Create a closed, 2D bezier path from the supplied points array
            // These points are treated as anchors, which the path will pass through
            // The control points for the path will be generated automatically
            BezierPath bezierPath = new BezierPath(points, closedPath, PathSpace.xz);
            // Then create a vertex path from the bezier path, to be used for movement etc
            return new VertexPath(bezierPath, Transform);
        }

        private static VertexPath GeneratePath(Vector3[] points, bool closedPath){
            // Create a closed, 2D bezier path from the supplied points array
            // These points are treated as anchors, which the path will pass through
            // The control points for the path will be generated automatically
            BezierPath bezierPath = new BezierPath(points, closedPath, PathSpace.xyz);
            // Then create a vertex path from the bezier path, to be used for movement etc
            return new VertexPath(bezierPath, Transform);
        }
    }
}