using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace HuskVR
{
    public static class UnityUtils
    {
        public static GameObject ChildByName(this GameObject from, string name)
        {
            List<GameObject> children = new List<GameObject>();
            int count = 0;
            while (count < from.transform.childCount)
            {
                children.Add(from.transform.GetChild(count).gameObject);
                count++;
            }

            if (count == 0)
            {
                return null;
            }

            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].name == name)
                {
                    return children[i];
                }
            }
            return null;
        }
    }
}
