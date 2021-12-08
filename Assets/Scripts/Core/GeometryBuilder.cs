using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VRoom
{
    public struct Parameters
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;
        public Vector3[] VertsPositions;
        public Vector3 NormalDirection;

        public Parameters(Vector3 center, Quaternion rotation, Vector3 scale) : this()
        {
            Position = center;
            Rotation = rotation;
            Scale = scale;
        }

        public Parameters(Vector3 center, Quaternion rotation, Vector3 scale, Vector3[] vertsPositions, Vector3 normal) : this()
        {
            Position = center;
            Rotation = rotation;
            Scale = scale;
            VertsPositions = vertsPositions;
            NormalDirection = normal;
        }
    }

    public class GeometryBuilder : MonoBehaviour
    {
        [SerializeField] List<RoomItemBase> _roomItemPrefabs = new List<RoomItemBase>();
        private List<GameObject> _windows = new List<GameObject>();
        private List<GameObject> _windowFrames = new List<GameObject>(); // for visualization purpose only...holds GOs with linerenderers

        public RoomItem GetPrefab(ItemType itemType)
        {
            return (RoomItem)_roomItemPrefabs.FirstOrDefault(i => i.Type.Value.Equals(itemType.Value));
        }
        internal object BuildWindow(int index)
        {
            throw new NotImplementedException();
        }

        public RoomItem BuildRoomItem(ItemType type, Parameters paramenter)
        {
            var item = GetPrefab(type);
            if (!item)
            {
                Debug.LogError($"Prefab for {type.Name} is missing!");
                return null;
            }
            item = Instantiate(item, paramenter.Position, paramenter.Rotation);
            item.ConstructFromParameters(paramenter);
            item.name = $"{type.Name}";
            item.Type = type;
            item.SetLayer(type.Name);
            item.transform.localScale = paramenter.Scale;
            return item;
        }
    }
}
