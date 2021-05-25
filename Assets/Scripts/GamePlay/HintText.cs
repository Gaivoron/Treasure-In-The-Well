using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    //TODO - should implement interface.
    public sealed class HintText : MonoBehaviour
    {
        [SerializeField]
        private Text _text;

        public void Show(string text)
        {
            _text.text = text;
            Show(true);
        }

        public void Hide()
        {
            Show(false);
        }

        private void Show(bool isVisble) => gameObject.SetActive(isVisble);
    }
}