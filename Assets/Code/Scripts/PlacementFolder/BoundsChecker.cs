using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsChecker 
{
        /// <summary>
        /// Checks if the position is out of defined bounds.
        /// </summary>
        public static bool IsOutOfBounds(Vector3 position)
        {
            return position.x < -2 || position.x > 1 ||
                   position.z < -5 || position.z > 0 ||
                   position.y < 1 || position.y > 5;
        }

}
