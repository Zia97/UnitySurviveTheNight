using System;
using HeroEditor.Common.Enums;
using UnityEngine;

namespace Assets.HeroEditor.Common.CharacterScripts
{
    /// <summary>
    /// Rotates arms and passes input events to child components like FirearmFire and BowShooting.
    /// </summary>
    public class WeaponControls : MonoBehaviour
    {
        public Character Character;
        public Transform ArmL;
        public Transform ArmR;
        public KeyCode FireButton;
        public KeyCode ReloadButton;
	    public bool FixHorizontal;
        public bool isNpc;
        public string location;

        private bool _locked;
        private bool _areControlsEnabled = true;

        public void Update()
        {
            _locked = !Character.Animator.GetBool("Ready") || Character.Animator.GetInteger("Dead") > 0;

            if (_locked) return;
            if (!_areControlsEnabled) return;

            switch (Character.WeaponType)
            {
                case WeaponType.Melee1H:
                case WeaponType.Melee2H:
                case WeaponType.MeleePaired:
                    if (Input.GetKeyDown(FireButton))
                    {
                        Character.Animator.SetTrigger(Time.frameCount % 2 == 0 ? "Slash" : "Jab"); // Play animation randomly
                    }
                    break;
                case WeaponType.Bow:
                    Character.BowShooting.ChargeButtonDown = Input.GetKeyDown(FireButton);
                    Character.BowShooting.ChargeButtonUp = Input.GetKeyUp(FireButton);
                    break;
                case WeaponType.Firearms1H:
                case WeaponType.Firearms2H:
                    var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    LateUpdate();
                    if (target.x >= -5.5)
                    {                 
                        Character.Firearm.Fire.FireButtonDown = Input.GetKeyDown(FireButton);
                        Character.Firearm.Fire.FireButtonPressed = Input.GetKey(FireButton);
                        Character.Firearm.Fire.FireButtonUp = Input.GetKeyUp(FireButton);
                        Character.Firearm.Reload.ReloadButtonDown = Input.GetKeyDown(ReloadButton);
                    }
                       
                    break;
	            case WeaponType.Supplies:
		            if (Input.GetKeyDown(FireButton))
		            {
			            Character.Animator.Play(Time.frameCount % 2 == 0 ? "UseSupply" : "ThrowSupply", 0); // Play animation randomly
		            }
		            break;
			}
        }

        /// <summary>
        /// Called each frame update, weapon to mouse rotation example.
        /// </summary>
        public void LateUpdate()
        {
            if (_locked) return;
            if (!_areControlsEnabled) return;

            Transform arm;
            Transform weapon;

            switch (Character.WeaponType)
            {
                case WeaponType.Bow:
                    arm = ArmL;
                    weapon = Character.BowRenderers[3].transform;
                    break;
                case WeaponType.Firearms1H:
                case WeaponType.Firearms2H:
                    arm = ArmR;
                    weapon = Character.Firearm.FireTransform;
                    break;
                default:
                    return;
            }

            if(isNpc)
            {
                Vector3 newVector = new Vector3(0, 0, 0);

                if (location.Equals("Top"))
                {
                    newVector = new Vector3(6.4f, 3.3f, -10);
                }
                else if(location.Equals("Mid"))
                {
                    newVector = new Vector3(1f, 1.1f, -10);
                }
                else if (location.Equals("Bot"))
                {
                    newVector = new Vector3(0.5f, -2.5f, -10);
                }

                RotateArm(arm, weapon, newVector, -40, 40);

            }
            else
            {
                RotateArm(arm, weapon, FixHorizontal ? arm.position + 1000 * Vector3.right : Camera.main.ScreenToWorldPoint(Input.mousePosition),-40, 40);
            }            
        }

        public void setLocation(String loc)
        {
            location = loc;
        }

        public void isNPC()
        {
            isNpc = true;
        }

        /// <summary>
        /// Selected arm to position (world space) rotation, with limits.
        /// </summary>
        public void RotateArm(Transform arm, Transform weapon, Vector2 target, float angleMin, float angleMax) // TODO: Very hard to understand logic
        {
            target = arm.transform.InverseTransformPoint(target);
            
            var angleToTarget = Vector2.SignedAngle(Vector2.right, target);
            var angleToFirearm = Vector2.SignedAngle(weapon.right, arm.transform.right) * Math.Sign(weapon.lossyScale.x);
            var angleFix = Mathf.Asin(weapon.InverseTransformPoint(arm.transform.position).y / target.magnitude) * Mathf.Rad2Deg;
            var angle = angleToTarget + angleToFirearm + angleFix;

            angleMin += angleToFirearm;
            angleMax += angleToFirearm;

            var z = arm.transform.localEulerAngles.z;

            if (z > 180) z -= 360;

            if (z + angle > angleMax)
            {
                angle = angleMax;
            }
            else if (z + angle < angleMin)
            {
                angle = angleMin;
            }
            else
            {
                angle += z;
            }

            if(angle<180)
            {
                try
                {
                    arm.transform.localEulerAngles = new Vector3(0, 0, angle);
                }
                catch (Exception e)
                {
                    throw;
                }
            }

        }

        public void areControlsEnabled(bool value)
        {
            _areControlsEnabled = value;
        }
    }
}