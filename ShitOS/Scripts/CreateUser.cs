using ShitOS.Manager;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShitOS.UserLogin
{
    public class CreateUser : MonoBehaviour
    {
        [SerializeField] private UIDocument _UIDocument;
        private TextField _username;
        private TextField _password;
        private TextField _secondPassword;
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
            _username = _UIDocument.rootVisualElement.Q<TextField>("CUsername");
            _password = _UIDocument.rootVisualElement.Q<TextField>("CPassword");
            _secondPassword = _UIDocument.rootVisualElement.Q<TextField>("SecondPassword");
            _loginBtn = _UIDocument.rootVisualElement.Q<Button>("CreateUser");
        }
        private void RegisterCallbacks(bool register)
        {
            if (register)
            {
                _loginBtn.RegisterCallback<ClickEvent>(SaveUser);
            }
            else
            {
                _loginBtn.UnregisterCallback<ClickEvent>(SaveUser);
            }
        }

        private void SaveUser(ClickEvent evnt)
        {
            if (_password.value != _secondPassword.value) { Debug.Log("The two passwords are not the same");  return; } 
            string path = Path.Combine(Application.persistentDataPath, "Userdata.json");
            UserData data = new UserData();
            data.username = _username.value;
            data.password = _password.value;
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(path, json);
            Debug.Log("Data saved to file " + path);
            OSManager.Instance.ChangeMenu(OSWindows.Login);
        }
    }
}
