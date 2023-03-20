using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShitOS.Window
{
    public class Dashboard : MonoBehaviour
    {
        [SerializeField] private UIDocument _UIDocument;
        private VisualElement _background;
        private Sprite[] _backgroundImages;

        private void OnEnable()
        {
            _background = _UIDocument.rootVisualElement.Q<VisualElement>("Background");
        }
        private void Start()
        {
            _backgroundImages = Resources.LoadAll<Sprite>("Backgrounds");
            _background.style.backgroundImage = new StyleBackground(_backgroundImages[0]);
        }
    }
}