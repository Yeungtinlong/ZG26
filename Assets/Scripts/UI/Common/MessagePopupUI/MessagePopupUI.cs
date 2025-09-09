using TMPro;
using UnityEngine;

namespace TheGame.UI
{
    public class MessagePopupUI : BaseUI, IDataContainer<string>, IOneShotUI
    {
        public override UILayer Layer => UILayer.Popup;
        [SerializeField] private TMP_Text _messageText;

        public void SetData(string message)
        {
            _messageText.text = message;
        }
    }
}