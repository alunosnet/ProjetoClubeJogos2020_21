using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        for (int a = index; a < arr.Length - 1; a++)
        {
            // moving elements downwards, to fill the gap at [index]
            arr[a] = arr[a + 1];
        }
        // finally, let's decrement Array's size by one
        Array.Resize(ref arr, arr.Length - 1);
    }

    public static void Remove<T>(ref T[] arr, T elemento)
    {
        //Debug.Log("Começar a remover");
        for (int a = 0; a <= arr.Length - 1; a++)
        {
          //      Debug.Log("Remove " + a);
            // moving elements downwards, to fill the gap at [index]
            if (arr[a].Equals(elemento))
            {
                RemoveAt(ref arr, a);
                break;
            }
        }
    }
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360; if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    /// <summary>
    /// Função que devolve um vetor com os componentes dos filhos sem o pai
    /// A função que existe do Unity devolve sempre o pai
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="obj">Parent game object</param>
    /// <returns>Devolve um vetor com os componentes dos filhos sem incluir o pai</returns>
    public static T[] GetComponentsInChildWithoutRoot<T>(GameObject obj) where T : Component
    {
        List<T> tList = new List<T>();
        if (obj == null) return null;
        foreach (Transform child in obj.transform)
        {
            T[] scripts = child.GetComponentsInChildren<T>();
            if (scripts != null)
            {
                foreach (T sc in scripts)
                    tList.Add(sc);
            }
        }
        return tList.ToArray();
    }
    /// <summary>
    /// Função que devolve um vetor com os componentes dos filhos sem o pai
    /// A função que existe do Unity devolve sempre o pai
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="obj">Parent Game Object</param>
    /// <param name="tag">Only returns objects with this tag</param>
    /// <returns>Devolve um vetor com os componentes dos filhos sem incluir o pai</returns>
    public static T[] GetComponentsInChildWithoutRoot<T>(GameObject obj, string tag) where T : Component
    {
        List<T> tList = new List<T>();
        foreach (Transform child in obj.transform.root)
        {
            T[] scripts = child.GetComponentsInChildren<T>();
            if (scripts != null)
            {
                foreach (T sc in scripts)
                    if (sc.tag == tag) tList.Add(sc);
            }
        }
        return tList.ToArray();
    }

    public static List<GameObject> GetGamesObjectsWithComponent<T>() where T : Component
    {
        var Objectos = GameObject.FindObjectsOfType<GameObject>();
        List<GameObject> ObjectosComComponente = new List<GameObject>();
        foreach (var obj in Objectos)
        {
            if (obj.GetComponent<T>() != null)
                ObjectosComComponente.Add(obj);
        }
        return ObjectosComComponente;
    }

    public static List<T> GetComponentsInGameObjects<T>() where T : Component
    {
        var Objectos = GameObject.FindObjectsOfType<GameObject>();
        List<T> ObjectosComComponente = new List<T>();
        foreach (var obj in Objectos)
        {
            if (obj.GetComponent<T>() != null)
                ObjectosComComponente.Add(obj.GetComponent<T>());
        }
        return ObjectosComComponente;
    }


    /// <summary>
    /// Função para indicar se um objeto está à direita >0 ou à esquerda <0 de outro
    /// </summary>
    /// <param name="fwd"></param>
    /// <param name="targetDir"></param>
    /// <param name="up"></param>
    /// <returns><0 para esquerda; >0 para direita; 0 para em frente</returns>
    public static float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);
        return dir;
    }

    public static Vector3 BallisticVelocity(Vector3 from, Vector3 to, float angle)
    {
        Vector3 dir = to - from;//.position; // get Target Direction
        float height = dir.y; // get height difference
        dir.y = 0; // retain only the horizontal difference
        float dist = dir.magnitude; // get horizontal direction
        float a = angle * Mathf.Deg2Rad; // Convert angle to radians
        dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle.
        dist += height / Mathf.Tan(a); // Correction for small height differences

        // Calculate the velocity magnitude
        float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * dir.normalized; // Return a normalized vector.
    }
    /// <summary>
    /// Instanciates an object optionally indicate the time to live>0 and if the current sound settings should by aplyed
    /// </summary>
    /// <param name="go">object to instanciate</param>
    /// <param name="timeToLive">Optional set to destroy de object automatically</param>
    /// <param name="applySoundSettings">Optional set true to apply the current sound settings</param>
    /// <returns></returns>
    public static GameObject SpawnObject(GameObject go, float timeToLive = 0, bool applySoundSettings = false)
    {
        GameObject e = (GameObject)Instantiate(go);//, hitPoint, Quaternion.identity);
                                                   //  if (applySoundSettings && AudioManager.instance != null && e.GetComponent<AudioSource>()) e.GetComponent<AudioSource>().volume = AudioManager.instance.sfxVolumePercent * AudioManager.instance.masterVolumePercent;
        if (timeToLive > 0) Destroy(e, timeToLive);
        return e;
    }
    /// <summary>
    /// Instanciates an object optionally indicate the time to live>0 and if the current sound settings should by aplyed
    /// </summary>
    /// <param name="go">object to instanciate</param>
    /// <param name="parent">The object for parenting the new object</param>
    /// <param name="timeToLive">Optional set to destroy de object automatically</param>
    /// <param name="applySoundSettings">Optional set true to apply the current sound settings</param>
    /// <returns></returns>
    public static GameObject SpawnObject(GameObject go, Transform parent, float timeToLive = 0, bool applySoundSettings = false)
    {
        GameObject e = (GameObject)Instantiate(go);//, hitPoint, Quaternion.identity);
                                                   // if (applySoundSettings && AudioManager.instance != null && e.GetComponent<AudioSource>()) e.GetComponent<AudioSource>().volume = AudioManager.instance.sfxVolumePercent * AudioManager.instance.masterVolumePercent;
        if (timeToLive > 0) Destroy(e, timeToLive);
        e.transform.parent = parent;
        return e;
    }
    /// <summary>
    /// Instanciates an object optionally indicate the time to live>0 and if the current sound settings should by aplyed
    /// </summary>
    /// <param name="go">object to instanciate</param>
    /// <param name="position">Position of the object</param>
    /// <param name="timeToLive">Optional set to destroy de object automatically</param>
    /// <param name="applySoundSettings">Optional set true to apply the current sound settings</param>
    /// <returns></returns>
    public static GameObject SpawnObject(GameObject go, Vector3 position, float timeToLive = 0, bool applySoundSettings = false)
    {
        GameObject e = (GameObject)Instantiate(go, position, Quaternion.identity);
        //    if (applySoundSettings && AudioManager.instance != null && e.GetComponent<AudioSource>()) e.GetComponent<AudioSource>().volume = AudioManager.instance.sfxVolumePercent * AudioManager.instance.masterVolumePercent;
        if (timeToLive > 0) Destroy(e, timeToLive);
        return e;
    }
    /// <summary>
    /// Instanciates an object optionally indicate the time to live>0 and if the current sound settings should by aplyed
    /// </summary>
    /// <param name="go">object to instanciate</param>
    /// <param name="position">Position of the object</param>
    /// <param name="parent">The object for parenting the new object</param>
    /// <param name="timeToLive">Optional set to destroy de object automatically</param>
    /// <param name="applySoundSettings">Optional set true to apply the current sound settings</param>
    /// <returns></returns>
    public static GameObject SpawnObject(GameObject go, Vector3 position, Transform parent, float timeToLive = 0, bool applySoundSettings = false)
    {
        GameObject e = (GameObject)Instantiate(go, position, Quaternion.identity);
        //   if (applySoundSettings && AudioManager.instance != null && e.GetComponent<AudioSource>()) e.GetComponent<AudioSource>().volume = AudioManager.instance.sfxVolumePercent * AudioManager.instance.masterVolumePercent;
        if (timeToLive > 0) Destroy(e, timeToLive);
        e.transform.parent = parent;
        return e;
    }
    /// <summary>
    /// Instanciates an object optionally indicate the time to live>0 and if the current sound settings should by aplyed
    /// </summary>
    /// <param name="go">object to instanciate</param>
    /// <param name="position">Position of the object</param>
    /// <param name="rotation">Rotation of the object</param>
    /// <param name="timeToLive">Optional set to destroy de object automatically</param>
    /// <param name="applySoundSettings">Optional set true to apply the current sound settings</param>
    /// <returns></returns>
    public static GameObject SpawnObject(GameObject go, Vector3 position, Quaternion rotation, float timeToLive = 0, bool applySoundSettings = false)
    {
        GameObject e = (GameObject)Instantiate(go, position, rotation);
        //    if (applySoundSettings && AudioManager.instance != null && e.GetComponent<AudioSource>()) e.GetComponent<AudioSource>().volume = AudioManager.instance.sfxVolumePercent * AudioManager.instance.masterVolumePercent;
        if (timeToLive > 0) Destroy(e, timeToLive);
        return e;
    }
    /// <summary>
    /// Instanciates an object optionally indicate the time to live>0 and if the current sound settings should by aplyed
    /// </summary>
    /// <param name="go">object to instanciate</param>
    /// <param name="position">Position of the object</param>
    /// <param name="rotation">Rotation of the object</param>
    /// <param name="parent">The object for parenting the new object</param>
    /// <param name="timeToLive">Optional set to destroy de object automatically</param>
    /// <param name="applySoundSettings">Optional set true to apply the current sound settings</param>
    /// <returns></returns>
    public static GameObject SpawnObject(GameObject go, Vector3 position, Quaternion rotation, Transform parent, float timeToLive = 0, bool applySoundSettings = false)
    {
        GameObject e = (GameObject)Instantiate(go, position, rotation);
        //    if (applySoundSettings && AudioManager.instance != null && e.GetComponent<AudioSource>()) e.GetComponent<AudioSource>().volume = AudioManager.instance.sfxVolumePercent * AudioManager.instance.masterVolumePercent;
        if (timeToLive > 0) Destroy(e, timeToLive);
        e.transform.parent = parent;
        return e;
    }

    public static Color ToColor(int value)
    {
        return new Color(((value & 0xFF0000) >> 16) / 255f,
                        ((value & 0xFF00) >> 8) / 255f,
                        (value & 0xFF) / 255f);
    }

    public static T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        Component copy;
        if (destination == null)
            destination = new GameObject();
        copy = destination.AddComponent(type);

        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
    }
    public static bool CanYouSeeThis(Vector3 positionFrom, Vector3 positionTo)
    {
        RaycastHit hit;
        var rayDirection = positionTo - positionFrom;
        Debug.DrawRay(positionFrom, rayDirection, Color.blue);
        if (Physics.Raycast(positionFrom, rayDirection, out hit))
        {
            return (hit.transform.position == positionTo);
        }
        return false;
    }
    public static bool CanYouSeeThis(Vector3 positionFrom, Vector3 positionTo, string tag)
    {
        RaycastHit hit;
        var rayDirection = positionTo - positionFrom;
        Debug.DrawRay(positionFrom, rayDirection, Color.blue);
        if (Physics.Raycast(positionFrom, rayDirection, out hit))
        {
            //Debug.Log("hit " + hit.transform.tag);
            if (hit.transform.tag.Equals(tag))
                return true;
            return false;// (hit.transform.position == positionTo);
        }
        return true;
    }
    public static bool CanYouSeeThis(Vector3 positionFrom, Vector3 positionTo, string tag, float fov)
    {
        RaycastHit hit;
        var rayDirection = positionTo - positionFrom;
        Debug.DrawRay(positionFrom, rayDirection, Color.blue);
        if (Physics.Raycast(positionFrom, rayDirection, out hit))
        {
            //Debug.Log("hit " + hit.transform.tag);
            if (hit.transform.tag.Equals(tag) && Vector3.Angle(positionFrom, positionTo) <= fov)
                return true;
            return false;// (hit.transform.position == positionTo);
        }
        return true;
    }
    public static bool CanYouSeeThis(Transform positionFrom, Transform positionTo, string tag, float fov)
    {
        RaycastHit hit;
        var rayDirection = positionTo.position - positionFrom.position;
        Debug.DrawRay(positionFrom.position, rayDirection, Color.blue);
        if (Physics.Raycast(positionFrom.position, rayDirection, out hit))
        {
            //Debug.Log("hit " + hit.transform.tag);
            float angle = Vector3.Angle(positionTo.position - positionFrom.position, positionFrom.forward);
            //Debug.Log("Angulo " + angle);
            if (hit.transform.tag.Equals(tag) && angle <= fov)
                return true;
            return false;// (hit.transform.position == positionTo);
        }
        return true;
    }
    //dont know if this works
    public static bool AreYouFacingThisWay(Transform positionFrom, Vector3 positionTo)
    {
        RaycastHit hit;
        if (Physics.Raycast(positionFrom.position, positionFrom.forward, out hit))
        {
            return (hit.transform.position == positionTo);
        }
        return false;
    }

    public static bool InCameraFov(Transform target, Transform camera, float fov)
    {
        if (Vector3.Angle(target.transform.position - camera.position, camera.forward) <= fov) return true;
        return false;
    }

    public static Vector3 UpdateXPositionWave(Vector3 pos, float amplitude, float period)
    {
        float theta = Time.timeSinceLevelLoad / period;
        float distance = amplitude * Mathf.Sin(theta);
        Vector3 newPos = pos + Vector3.right * distance;
        return newPos;
    }
    public static Vector3 UpdateYPositionWave(Vector3 pos, float amplitude, float period)
    {
        float theta = Time.timeSinceLevelLoad / period;
        float distance = amplitude * Mathf.Sin(theta);
        Vector3 newPos = pos + Vector3.up * distance;
        return newPos;
    }
    public static Vector3 UpdateZPositionWave(Vector3 pos, float amplitude, float period)
    {
        float theta = Time.timeSinceLevelLoad / period;
        float distance = amplitude * Mathf.Sin(theta);
        Vector3 newPos = pos + Vector3.forward * distance;
        return newPos;
    }

    public static GameObject FindNearestGameObject<T>(Vector3 me)
    {
        var Objectos = GameObject.FindObjectsOfType<GameObject>();
        List<GameObject> ObjectosComComponente = new List<GameObject>();
        foreach (var obj in Objectos)
        {
            if (obj.GetComponent<T>() != null)
                ObjectosComComponente.Add(obj);
        }
        var MaisPerto = ObjectosComComponente[0];
        float distanciaMinima = Vector3.Distance(me, ObjectosComComponente[0].transform.position);
        foreach(GameObject go in ObjectosComComponente)
        {
            float distancia= Vector3.Distance(me, ObjectosComComponente[0].transform.position);
            if (distancia < distanciaMinima)
            {
                distanciaMinima = distancia;
                MaisPerto = go;
            }
        }

        return MaisPerto;
    }
}
