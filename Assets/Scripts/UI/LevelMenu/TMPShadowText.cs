using TMPro;
using UnityEngine;

namespace TheGame.UI
{
    public class TMPShadowText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _shadowText;
        [SerializeField] private TMP_Text _contentText;

        public void Set(string levelName)
        {
            _shadowText.text = levelName;
            _contentText.text = levelName;
        }
    }
}