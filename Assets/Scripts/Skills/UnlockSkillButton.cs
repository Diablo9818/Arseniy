using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class UnlockSkillButton: MonoBehaviour
    {
        SkillManager skillManager;
        private void Awake()
        {
            skillManager = FindObjectOfType<SkillManager>();
        }
        public void UnlockDoubleShotForCrossbow()
        {
            skillManager.crossbowCanDoubleShot = true;
        }

        public void UnlockBoomYadro()
        {
            skillManager.MortitaCanBoom = true;
        }

        public void UnlockFireGunLeft()
        {
            skillManager.fireGunleftFireEnable = true;
        }        
        public void UnlockFireGunRight()
        {
            skillManager.fireGunrightFireEnable = true;
        }

        public void UnlockBigYadro()
        {
            skillManager.bigYadroEnable = true;
            skillManager.smallYadroEnable=false;
        }

        public void UnlockSmallYadro()
        {
            skillManager.bigYadroEnable = false;
            skillManager.smallYadroEnable = true;
        }

        public void UnlockOneArrow()
        {
            skillManager.crossbowPlusOneArrow = true;
        }

        public void UnlockTwoArrow()
        {
            skillManager.crossbowPlusTwoArrow = true;
        }

        public void UnlockThreeArrow()
        {
            skillManager.crossbowPlusThreeArrow = true;
        }

        public void UnlockFourArrow()
        {
            skillManager.crossbowPlusFourArrow = true;
        }

        public void UnlockFiveArrow()
        {
            skillManager.crossbowPlusFiveArrow = true;
        }
    }
}
