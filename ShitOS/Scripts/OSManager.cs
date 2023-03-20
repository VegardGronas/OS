using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShitOS.Manager
{
    public enum OSWindows { Login, UserCreation, Dashboard}
    public class OSManager : MonoBehaviour
    {
        public static OSManager Instance { get; private set; }
        [SerializeField] private UIDocument _UIDocument;
        [SerializeField] private OSWindows _DefaultWindow;
        private VisualElement _loginWindow;
        private VisualElement _userCreationWindow;
        private VisualElement _dashboardWindow;

        private List<VisualElement> _elements = new List<VisualElement>();

        private void Awake()
        {
            if(Instance != null && Instance != this) Destroy(Instance);
            else Instance = this;
        }

        private void OnEnable()
        {
            _loginWindow = _UIDocument.rootVisualElement.Q<VisualElement>("Login");
            _userCreationWindow = _UIDocument.rootVisualElement.Q<VisualElement>("CreateUser");
            _dashboardWindow = _UIDocument.rootVisualElement.Q<VisualElement>("Dashboard");
        }

        private void Start()
        {
            _elements.Add(_loginWindow);
            _elements.Add(_userCreationWindow);
            _elements.Add(_dashboardWindow);
            ChangeMenu(_DefaultWindow);
        }

        public void ChangeMenu(OSWindows window)
        {
            switch(window)
            {
                case OSWindows.Login:
                    EnableWindow(_loginWindow);
                    break;
                case OSWindows.UserCreation:
                    EnableWindow(_userCreationWindow);
                    break;
                case OSWindows.Dashboard:
                    EnableWindow(_dashboardWindow);
                    break;
            }
        }

        private void EnableWindow(VisualElement element)
        {
            foreach(VisualElement ele in _elements)
            {
                if (ele == element) ele.style.display = DisplayStyle.Flex;
                else ele.style.display = DisplayStyle.None;
            }
        }
    }
}