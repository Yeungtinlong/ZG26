using TheGame.GM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MBF.UnitBehaviors
{
    public class ChaPie : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _gradeText;

        [SerializeField] private Slider _hpSlider;
        [SerializeField] private Slider _speedSlider;
        [SerializeField] private Slider _shieldSlider;

        [SerializeField] private TMP_Text _speedText;
        private CharacterState _cs;

        private void Awake()
        {
            _cs = GetComponentInParent<CharacterState>();
        }

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            _cs.OnResourceChanged += CharacterState_OnResourceChanged;
            _cs.OnDie += CharacterState_OnDie;
        }

        private void UnsubscribeFromEvents()
        {
            _cs.OnResourceChanged -= CharacterState_OnResourceChanged;
            _cs.OnDie -= CharacterState_OnDie;
        }

        private void CharacterState_OnResourceChanged(CharacterState cs)
        {
            _hpSlider.value = (float)_cs.resource.hp / _cs.Prop.hp;
            _speedSlider.value = (float)_cs.resource.speed / _cs.Prop.speed;
            _shieldSlider.value = (float)_cs.resource.shp / _cs.Prop.hp;
            
            _nameText.text = LuaToCsBridge.CharacterTable[_cs.id].Name;
            _gradeText.text = $"LV {_cs.grade}";
            _speedText.text = $"{_cs.resource.speed}/{_cs.Prop.speed}";
        }

        private void CharacterState_OnDie(CharacterState cs)
        {
            _hpSlider.gameObject.SetActive(false);
            _speedSlider.gameObject.SetActive(false);
            _shieldSlider.gameObject.SetActive(false);
        }
    }
}