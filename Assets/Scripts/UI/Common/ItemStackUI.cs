using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TheGame.ResourceManagement;

namespace TheGame.UI
{
    public class ItemStackUI : MonoBehaviour
    {
        [SerializeField] private Image _bgImage;
        [SerializeField] private Image _spriteImage;
        [SerializeField] private Image _selectedObject;
        [SerializeField] private TMP_Text _valueText;
        [SerializeField] private Button _clickable;

        private string _id;
        public string Id => _id;
        
        public void Set(ItemStack itemStack)
        {
            _id = itemStack.id;
            _spriteImage.LoadAsyncForget($"Sprites/Items/ui_head_{_id}.png");
            _valueText.text = $"{itemStack.count}";
        }
    }
}