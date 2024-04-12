using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    ///<summary>컴포넌트가 없으면 만들어서 캐싱함</summary>
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }
    ///<summary>자식 객체를 이름으로 찾음</summary>
    ///<param name = "go">자식을 찾을 객체</param><param name = "name">찾을 자식객체의 이름</param><param name = "recursive">재귀 여부</param>
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }
    ///<summary>자식 객체의 컴포넌트를 자식 객체의 이름으로 찾음</summary>
    ///<param name = "go">자식을 찾을 객체</param><param name = "name">찾을 자식객체의 이름</param><param name = "recursive">재귀 여부</param>
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }
    ///<summary></summary>
    public static T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);
        }

        return Resources.Load<T>(path);
    }
    ///<summary>프리팹 폴더의 프리팹을 이름으로 찾아 생성하고, 부모를 할당할 수 있음</summary>
    ///<param name = "path">프리팹 이름</param> <param name = "parent">부모 트랜스폼</param>
    public static GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.LogError($"Failed to load prefab : {path}");
            return null;
        }

        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        return go;
    }
    ///<summary></summary>
    public static void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Object.Destroy(go);
    }

    ///<summary></summary>
    public static Material[] SetSkinnedMat(GameObject go, int element, Material mat)
    {
        SkinnedMeshRenderer skin = go.GetComponentInChildren<SkinnedMeshRenderer>();
        if (skin == null)
            skin = go.GetComponent<SkinnedMeshRenderer>();


        Material[] mats = skin.materials;
        mats[element] = mat;
        skin.materials = mats;
        return mats;
    }

    ///<summary>중복되지 않는 난수를 원소로 지닌 n크기의 int형 배열을 반환함</summary>
    public static int[] Shuffle(int n)
    {
        int[] arr = new int[n];

        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = Random.Range(0, arr.Length);
            for (int j = 0; j < i; j++)
            {
                if (arr[i] == arr[j])
                    i--;
            }
        }
        return arr;
    }

    ///<summary>값 매핑</summary>
    ///<param name = "x">원래 값</param><param name = "in_min">원래 최소값</param><param name = "in_max">원래 최대값</param>
    ///<param name = "out_min">스케일 범위 최소값</param><param name = "out_max">스케일 범위 최대값</param>
    public float Map(float x, float in_min, float in_max, float out_min, float out_max, bool clamp = false)
    {
        if (clamp) x = System.Math.Max(in_min, System.Math.Min(x, in_max));
        float res = (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        return res;
        // 두 집합 원소들 간의 대응관계
        //http://www.ktword.co.kr/test/view/view.php?m_temp1=608

    }



}
