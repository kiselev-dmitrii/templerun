﻿using UnityEngine.EventSystems;

namespace TempleRun.View {
    public class ClickBinding : CommandBinding, IPointerClickHandler {
        public void OnPointerClick(PointerEventData eventData) {
            if (_command == null) return;

            _command.DynamicInvoke();
        }
    }
}