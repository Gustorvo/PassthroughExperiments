using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

namespace VRoom
{
    public abstract class RoomItemBase : MonoBehaviour
    {
        // ----- INSPECTOR DRAWER ----- //
        [Dropdown("AllItemTypes"), OnValueChanged("SetType"), SerializeField]
        private string TypeName = ItemType.None.Name;
        private List<string> AllItemTypes => ItemType.AllTypes.ConvertAll(t => t.Name);
        private void SetType()
        {
            if (Type == null || Type.Value == 0)
            {
                Type = ItemType.FromString(TypeName);
            }
        }
        // ----- /////// ----- //

        private ItemType _type;
        public ItemType Type
        {
            get => _type;
            internal set
            {
                _type = value;
                TypeName = _type.Name;
            }
        }
        protected virtual void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public virtual void ConstructFromParameters(Parameters parameters)
        {

        }

        private void OnValidate()
        {
            SetType();
        }

    }
}