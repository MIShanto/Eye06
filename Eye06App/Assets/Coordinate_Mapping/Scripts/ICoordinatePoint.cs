using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoordinateMapper {
    public interface ICoordinatePoint {
        Location location { get; set; }
        GameObject pointPrefab { get; set; }

        GameObject Plot(Transform planet, Transform container, int layer);
        GameObject Plot(Transform planet, Transform container, int layer, bool alreadyExists, GameObject existingObject, float elevation);
    }
}
