using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ServerUtil {
    private static System.Random _rng;

    public static System.Random RNG {
        get {
            if (_rng == null) _rng = new System.Random();
            return _rng;
        }
    }
    public static Vector3 GetMidpointOfTransforms(Transform[] transforms) {
        float totalX = 0f;
        float totalY = 0f;
        float totalZ = 0f;

        foreach (Transform t in transforms) {
            Vector3 position = t.position;
            
            totalX += position.x;
            totalY += position.y;
            totalZ += position.z;
        }

        return new Vector3(
            totalX / transforms.Length,
            totalY / transforms.Length,
            totalZ / transforms.Length
        );
    }

    public static float ConvertGameRangeToUnityRange(float gameRange) {
        return gameRange / 10;
    }

    public static float ConvertUnityRangeToGameRange(float unityRange) {
        return unityRange * 10;
    }

    public static T GetRandomItemFromHashSet<T>(HashSet<T> hashSet) {
        foreach (T item in hashSet) {
            return item;
        }

        throw new NotFoundException("The hash set provided was empty!");
    }
    
    public static Bounds GetBounds(GameObject obj) {
        Bounds bounds = new Bounds();
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        if (renderers.Length > 0) {
            //Find first enabled renderer to start encapsulate from it
            foreach (Renderer renderer in renderers) {
                if (renderer.enabled) {
                    bounds = renderer.bounds;
                    break;
                }
            }

            //Encapsulate for all renderers
            foreach (Renderer renderer in renderers) {
                if (renderer.enabled) {
                    bounds.Encapsulate(renderer.bounds);
                }
            }
        }

        return bounds;
    }
}
