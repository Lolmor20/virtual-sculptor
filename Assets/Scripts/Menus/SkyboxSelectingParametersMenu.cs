using Assets.Scripts.Managers;
using Assets.Scripts.Menus.Icons;
using Assets.Scripts.Config;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class SkyboxSelectingParametersMenu : ParametersMenu
    {
        private readonly List<MenuIcon> skyboxes = new List<MenuIcon>();

        public SkyboxSelectingParametersMenu(GameObject gameObject) : base(gameObject)
        {
            var skyboxNames = AppConfig.SkyboxNames;
            var skyboxMaterials = AppConfig.SkyboxMaterials;

            if (skyboxNames.Length != skyboxMaterials.Length)
            {
                throw new BadConfigException();
            }

            for (int i = 0; i < skyboxNames.Length; i++)
            {
                skyboxes.Add(new SkyboxSelectingMenuIcon(GameObject.Find(skyboxNames[i]), GameManager.Instance.ActionsData.Selecting, Resources.Load<Material>(skyboxMaterials[i])));
            }

            icons = skyboxes;
        }
    }
}