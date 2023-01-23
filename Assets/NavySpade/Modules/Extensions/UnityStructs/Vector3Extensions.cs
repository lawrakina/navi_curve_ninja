using UnityEngine;

namespace NavySpade.Modules.Extensions.UnityStructs
{
    public static class Vector3Extensions
    {
        /// <summary>
        /// Gets the square distance between two vector3 positions. this is much faster that Vector3.distance.
        /// </summary>
        /// <param name="first">First point</param>
        /// <param name="second">Second point</param>
        /// <returns>Squared distance</returns>
        public static float SqrDistance(this Vector3 first, Vector3 second)
        {
            return (first.x - second.x) * (first.x - second.x) +
                   (first.y - second.y) * (first.y - second.y) +
                   (first.z - second.z) * (first.z - second.z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>Middle point</returns>
        public static Vector3 MidPoint(this Vector3 first, Vector3 second)
        {
            return new Vector3((first.x + second.x) * 0.5f, (first.y + second.y) * 0.5f, (first.z + second.z) * 0.5f);
        }

        /// <summary>
        /// Get the square distance from a point to a line segment.
        /// </summary>
        /// <param name="point">Point to get distance to</param>
        /// <param name="lineP1">Line segment start point</param>
        /// <param name="lineP2">Line segment end point</param>
        /// <param name="closestPoint">Set to either 1, 2, or 4, determining which end the point is closest to (p1, p2, or the middle)</param>
        /// <returns></returns>
        public static float SqrLineDistance(this Vector3 point, Vector3 lineP1, Vector3 lineP2, out int closestPoint)
        {
            var v = lineP2 - lineP1;
            var w = point - lineP1;

            var c1 = Vector3.Dot(w, v);
            if (c1 <= 0) //closest point is p1
            {
                closestPoint = 1;
                return SqrDistance(point, lineP1);
            }

            var c2 = Vector3.Dot(v, v);
            if (c2 <= c1) //closest point is p2
            {
                closestPoint = 2;
                return SqrDistance(point, lineP2);
            }

            var b = c1 / c2;

            var pb = lineP1 + b * v;
            {
                closestPoint = 4;
                return SqrDistance(point, pb);
            }
        }

        /// <summary>
        /// Absolute value of components
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 Abs(this Vector3 v)
        {
            return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        }

        /// <summary>
        /// Vector3.Project, onto a plane
        /// </summary>
        /// <param name="v"></param>
        /// <param name="planeNormal"></param>
        /// <returns></returns>
        public static Vector3 ProjectOntoPlane(this Vector3 v, Vector3 planeNormal)
        {
            return v - Vector3.Project(v, planeNormal);
        }

        /// <summary>
        /// Gets the normal of the triangle formed by the 3 vectors
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <param name="vec3"></param>
        /// <returns></returns>
        public static Vector3 Vector3Normal(Vector3 vec1, Vector3 vec2, Vector3 vec3)
        {
            return Vector3.Cross((vec3 - vec1), (vec2 - vec1));
        }

        /// <summary>
        /// Using the magic of 0x5f3759df
        /// </summary>
        /// <param name="vec1"></param>
        /// <returns></returns>
        //public static Vector3 FastNormalized(this Vector3 vec1)
        //{
        //    var componentMult = MathExtensions.FastInvSqrt(vec1.sqrMagnitude);
        //    return new Vector3(vec1.x * componentMult, vec1.y * componentMult, vec1.z * componentMult);
        //}

        /// <summary>
        /// Gets the center of two points
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        public static Vector3 Center(Vector3 vec1, Vector3 vec2)
        {
            return new Vector3((vec1.x + vec2.x) / 2, (vec1.y + vec2.y) / 2, (vec1.z + vec2.z) / 2);
        }

        public static float DistanceX(this Vector3 curVec, Vector3 toVec) {
            return Mathf.Abs(toVec.x - curVec.x);
        }

        public static float DistanceY(this Vector3 curVec, Vector3 toVec) {
            return Mathf.Abs(toVec.y - curVec.y);
        }

        public static float DistanceZ(this Vector3 curVec, Vector3 toVec) {
            return Mathf.Abs(toVec.z - curVec.z);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static bool IsNaN(this Vector3 vec)
        {
            return float.IsNaN(vec.x * vec.y * vec.z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Vector3 Center(this Vector3[] points)
        {
            var ret = Vector3.zero;
            foreach (var p in points)
                ret += p;

            ret /= points.Length;

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction1"></param>
        /// <param name="direction2"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static float AngleAroundAxis(Vector3 direction1, Vector3 direction2, Vector3 axis)
        {
            direction1 = direction1 - Vector3.Project(direction1, axis);
            direction2 = direction2 - Vector3.Project(direction2, axis);

            var angle = Vector3.Angle(direction1, direction2);

            return angle * (Vector3.Dot(axis, Vector3.Cross(direction1, direction2)) < 0 ? -1 : 1);
        }

        /// <summary>
        /// Returns a random direction in a cone. a spread of 0 is straight, 0.5 is 180*
        /// </summary>
        /// <param name="spread"></param>
        /// <param name="forward">must be unit</param>
        /// <returns></returns>
        public static Vector3 RandomDirection(float spread, Vector3 forward)
        {
            return Vector3.Slerp(forward, Random.onUnitSphere, spread);
        }

        /// <summary>
        /// Find a point on the infinite line nearest to point.
        /// </summary>
        /// <param name="lineStart"></param>
        /// <param name="lineEnd"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Vector3 NearestPoint(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
        {
            var lineDirection = Vector3.Normalize(lineEnd - lineStart);
            var closestPoint = Vector3.Dot((point - lineStart), lineDirection) / Vector3.Dot(lineDirection, lineDirection);

            return lineStart + (closestPoint * lineDirection);
        }

        /// <summary>
        /// Find a point on the line segment nearest to point.
        /// </summary>
        /// <param name="lineStart"></param>
        /// <param name="lineEnd"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Vector3 NearestPointStrict(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
        {
            var fullDirection = lineEnd - lineStart;
            var lineDirection = Vector3.Normalize(fullDirection);
            var closestPoint = Vector3.Dot((point - lineStart), lineDirection) / Vector3.Dot(lineDirection, lineDirection);

            return lineStart + (Mathf.Clamp(closestPoint, 0.0f, Vector3.Magnitude(fullDirection)) * lineDirection);
        }

        public static Vector3 Sinerp(Vector3 from, Vector3 to, float value)
        {
            value = Mathf.Sin(value * Mathf.PI * 0.5f);
            return Vector3.Lerp(from, to, value);
        }

        /// <summary>
        /// Calculates the intersection line segment between 2 lines (not segments).
        /// Returns false if no solution can be found.
        /// </summary>
        /// <returns></returns>
        public static bool CalculateLineLineIntersection(Vector3 line1Point1, Vector3 line1Point2,
            Vector3 line2Point1, Vector3 line2Point2, out Vector3 resultSegmentPoint1, out Vector3 resultSegmentPoint2)
        {
            // Algorithm is ported from the C algorithm of 
            // Paul Bourke at http://local.wasp.uwa.edu.au/~pbourke/geometry/lineline3d/
            resultSegmentPoint1 = new Vector3(0, 0, 0);
            resultSegmentPoint2 = new Vector3(0, 0, 0);

            var p1 = line1Point1;
            var p2 = line1Point2;
            var p3 = line2Point1;
            var p4 = line2Point2;
            var p13 = p1 - p3;
            var p43 = p4 - p3;

            if (p4.sqrMagnitude < float.Epsilon)
                return false;

            var p21 = p2 - p1;
            if (p21.sqrMagnitude < float.Epsilon)
                return false;

            var d1343 = p13.x * p43.x + p13.y * p43.y + p13.z * p43.z;
            var d4321 = p43.x * p21.x + p43.y * p21.y + p43.z * p21.z;
            var d1321 = p13.x * p21.x + p13.y * p21.y + p13.z * p21.z;
            var d4343 = p43.x * p43.x + p43.y * p43.y + p43.z * p43.z;
            var d2121 = p21.x * p21.x + p21.y * p21.y + p21.z * p21.z;

            var denom = d2121 * d4343 - d4321 * d4321;
            if (Mathf.Abs(denom) < float.Epsilon)
                return false;

            var numer = d1343 * d4321 - d1321 * d4343;

            var mua = numer / denom;
            var mub = (d1343 + d4321 * (mua)) / d4343;

            resultSegmentPoint1.x = p1.x + mua * p21.x;
            resultSegmentPoint1.y = p1.y + mua * p21.y;
            resultSegmentPoint1.z = p1.z + mua * p21.z;
            resultSegmentPoint2.x = p3.x + mub * p43.x;
            resultSegmentPoint2.y = p3.y + mub * p43.y;
            resultSegmentPoint2.z = p3.z + mub * p43.z;

            return true;
        }
    
        /// <summary>
        /// calculate velocity for throw object by angle with mass equal one
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector3 CalculateVelocity(Vector3 from, Vector3 to, float angle)
        {
            var dir = to - from; // get Target Direction
            var height = dir.y; // get height difference
            dir.y = 0; // retain only the horizontal difference
            var dist = dir.magnitude; // get horizontal direction
            var a = angle * Mathf.Deg2Rad; // Convert angle to radians
            dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle.
            dist += height / Mathf.Tan(a); // Correction for small height differences

            // Calculate the velocity magnitude
            var velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
            var result = velocity * dir.normalized;
            return result; // Return a normalized vector.
        }
    
        public static float GetAngleFromVectorFloat(this Vector3 direction)
        {
            direction = direction.normalized;
            var n = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (n < 0)
                n += 360;

            return n;
        }

        public static Vector3 GetVectorFromAngle(float angle)
        {
            //var angleRadian = Mathf.Deg2Rad * angle;
            //return new Vector3(Mathf.Sin(angleRadian), 0, Mathf.Cos(angleRadian));

            var angleRadian = Mathf.Deg2Rad * (angle + 90f);
            return new Vector3(Mathf.Cos(angleRadian), 0, Mathf.Sin(angleRadian));
        }

        public static Vector3 GetVectorFromAngleInt(int angle)
        {
            var angleRadian = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Cos(angleRadian), 0, Mathf.Sin(angleRadian));
        }
    }
}
