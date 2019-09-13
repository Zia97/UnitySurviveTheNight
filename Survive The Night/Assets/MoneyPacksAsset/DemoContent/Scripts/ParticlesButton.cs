using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MoneyAsset
{
    public class ParticlesButton : MonoBehaviour
    {
        [SerializeField] ParticleSystem ps;

        private void Awake()
        {
            var btn = GetComponent<Button>();

            if (btn != null)
            {
                btn.onClick.AddListener(()=>
                {
                    if (ps != null)
                    {
                        ps.Emit(20);
                    }
                });
            }
        }
    }
}