using BepInEx;
using KKAPI.Maker;
using KKAPI.Maker.UI;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// Generates random characters in the character maker
/// </summary>
namespace CharacterRandomizer
{
    [BepInPlugin(GUID, PluginName, Version)]
    public class KK_RandomCharacterGenerator : BaseUnityPlugin
    {
        public const string GUID = "com.deathweasel.bepinex.randomcharactergenerator";
        public const string PluginName = "Random Character Generator";
        public const string Version = "1.0";

        UI ui;
        RandomizerBody randomizerBody;
        RandomizerFace randomizerFace;
        RandomizerHair randomizerHair;

        void Main()
        {
            MakerAPI.RegisterCustomSubCategories += MakerAPI_RegisterCustomSubCategories;
        }

        private void MakerAPI_RegisterCustomSubCategories(object sender, RegisterSubCategoriesEvent e)
        {
            ui = new UI();
            randomizerBody = new RandomizerBody(ui);
            randomizerFace = new RandomizerFace(ui);
            randomizerHair = new RandomizerHair(ui);


            var parentCat = MakerConstants.Body.All;
            var cat = new MakerCategory(parentCat.CategoryName, "RandomCharacterGeneratorCategory", parentCat.Position + 5, "Randomize");
            e.AddSubCategory(cat);

            e.AddControl(new MakerButton("Set current character as template", cat, this)).OnClick.AddListener(delegate {
                randomizerBody.SetTemplate();
                randomizerFace.SetTemplate();
            });

            e.AddControl(new MakerButton("Randomize!", cat, this)).OnClick.AddListener(delegate {
                if (ui.randomizeBody.Value) randomizerBody.RandomizeBody();
                if (ui.randomizeBodySliders.Value) randomizerBody.RandomizeSliders();
                if (ui.randomizeFaceEyes.Value) randomizerFace.RandomizeEyes();
                if (ui.randomizeFaceEtc.Value) randomizerFace.RandomizeEtc();
                if (ui.randomizeFaceSliders.Value) randomizerFace.RandomizeSliders();
                if (ui.randomizeHair.Value) randomizerHair.RandomizeType();
                if (ui.randomizeHair.Value) randomizerHair.RandomizeEtc();
                if (ui.randomizeHairColor.Value) randomizerHair.RandomizeColor();

                if (ui.randomizePersonality.Value)
                {
                    ChaRandom.RandomName(MakerAPI.GetCharacterControl(), true, true, true);
                    ChaRandom.RandomParameter(MakerAPI.GetCharacterControl());
                }

                MakerAPI.GetCharacterControl().Reload();
            });

            e.AddControl(new MakerSeparator(cat, this));
            ui.randomizeBody = e.AddControl(new MakerToggle(cat, "Randomize body type", this));
            ui.randomizeBodySliders = e.AddControl(new MakerToggle(cat, "Randomize body sliders", this));
            ui.SkinColorRadio = e.AddControl(new MakerRadioButtons(cat, this, "Skin color", ui.skinColorOptions));

            e.AddControl(new MakerSeparator(cat, this));
            ui.randomizeFaceEyes = e.AddControl(new MakerToggle(cat, "Randomize eyes", this));
            ui.randomizeFaceEtc = e.AddControl(new MakerToggle(cat, "Randomize face type", this));
            ui.randomizeFaceSliders = e.AddControl(new MakerToggle(cat, "Randomize face sliders", this));

            e.AddControl(new MakerSeparator(cat, this));
            ui.randomizeHair = e.AddControl(new MakerToggle(cat, "Randomize hair", this));
            ui.randomizeHairColor = e.AddControl(new MakerToggle(cat, "Randomize hair color", this));

            e.AddControl(new MakerSeparator(cat, this));
            ui.randomizePersonality = e.AddControl(new MakerToggle(cat, "Randomize personality", this));

            e.AddControl(new MakerSeparator(cat, this));
            ui.DeviationSlider = e.AddControl(new MakerSlider(cat, "Total Deviation", 0, 1, 0.1f, this));
        }
        
    }
}
