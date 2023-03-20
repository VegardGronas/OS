using ShitOS.Manager;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShitOS.UserLogin
{
    public class Login : MonoBehaviour
    {
        [SerializeField] private UIDocument _UIDocument;
        private TextField _username;
        private TextField _password;
        private Button _loginBtn;

        private void OnEnable()
        {
            ReferenceElements();
            RegisterCallbacks(true);
        }

        private void OnDisable()
        {
            RegisterCallbacks(false);
        }

        private void ReferenceElements()
        {
            _username = _UIDocument.rootVisualElement.Q<TextField>("UUsername");
            _password = _UIDocument.rootVisualElement.Q<TextField>("UPassword");
            _loginBtn = _UIDocument.rootVisualElement.Q<Button>("ULogin");
        }
        private void RegisterCallbacks(bool register)
        {
            if(register)
            {
                _loginBtn.RegisterCallback<ClickEvent>(LoginEvent);
            }
            else
            {
                _loginBtn.UnregisterCallback<ClickEvent>(LoginEvent);
            }
        }

        private void LoginEvent(ClickEvent evnt)
        {
            if (!AuthorizeLogin()) return;
            OSManager.Instance.ChangeMenu(OSWindows.Dashboard);
        }

        private bool AuthorizeLogin()
        {
            string path = Path.Combine(Application.persistentDataPath, "Userdata.json");
            if (!File.Exists(path)) return false;
            string json = File.ReadAllText(path);
            UserData data = JsonUtility.FromJson<UserData>(json);
            if (_username.value != data.username) return false;
            if (_password.value != data.password) return false;
            return true;
        }
    }

    [System.Serializable] 
    public class UserData
    {
        public string username;
        public string password;
    }
}