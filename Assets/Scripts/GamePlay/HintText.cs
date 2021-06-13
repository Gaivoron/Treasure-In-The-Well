using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public sealed class HintText : MonoBehaviour, IHintText
    {
        [SerializeField]
        private Text _text;

        public void Show(bool isVisble) => gameObject.SetActive(isVisble);
        public void SetText(string text) => _text.text = text;
    }
}