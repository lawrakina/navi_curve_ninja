using System;
using System.Collections.Generic;
using System.Linq;
using Core.UI.Popups.Abstract;
using NavySpade.Modules.Configuration.Runtime.SO;
using NavySpade.UI.Popups.Abstract;
using UnityEngine;


namespace Core.UI.Popups{
    [CreateAssetMenu(fileName = "PopupsConfig", menuName = "Config/Popups", order = 51)]
    public class PopupsConfig : ObjectConfig<PopupsConfig>{
        [field: Min(0f)]
        [field: SerializeField]
        public float AfterWin{ get; private set; } = 3f;

        [field: Min(0f)]
        [field: SerializeField]
        public float AfterLose{ get; private set; } = 3f;

        public List<Popup> popups = new List<Popup>();

        [field: SerializeField]
        public PopupBackground PopupsBackground{ get; private set; }

        public T GetPopup<T>() where T : Popup{
            foreach (var popup in popups){
                if (popup is T popup1)
                    return popup1;
            }

            throw new NullReferenceException($"Popup With Type {typeof(T).FullName} not exist in config");
        }

        public T GetPopup<T>(string popupName) where T : Popup{
            var element = popups.FirstOrDefault(t => t != null && t.gameObject.name == popupName);

            if (element == null)
                throw new NullReferenceException($"Popup With Name {popupName} not exist in config");

            return element as T;
        }
    }
}