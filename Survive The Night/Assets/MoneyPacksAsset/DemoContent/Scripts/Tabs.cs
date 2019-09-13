using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MoneyAsset
{
    public class Tabs : MonoBehaviour
    {
        [SerializeField] Button[] buttons;
        [SerializeField] GameObject[] contents;

        public int CurrentTab { get; set; }

        private void Awake()
        {
            if (buttons.Length != contents.Length)
            {
                Debug.LogError("buttons and contents numbers have to be equal");

                return;
            }

            for (int i = 0; i < buttons.Length; i++)
            {
                var local = i;

                buttons[i].onClick.AddListener(() =>
                {
                    hideAll();

                    show(local);
                });
            }
        }

        void hideAll()
        {
            foreach (var c in contents)
            {
                c.SetActive(false);
            }

            foreach (var b in buttons)
            {
                b.interactable = true;
            }
        }

        void show(int i)
        {
            CurrentTab = i;
            buttons[i].interactable = false;
            contents[i].SetActive(true);
        }

        private void OnEnable()
        {
            ShowCurrentTab();
        }

        public void ShowCurrentTab()
        {
            buttons[CurrentTab].onClick.Invoke();
        }
    }
}
