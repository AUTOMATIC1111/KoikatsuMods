using BepInEx;
using KKAPI.Maker;
using KKAPI.Maker.UI;

namespace CharacterRandomizer
{
    [BepInPlugin(GUID: "info.jbcs.koikatsu.characterrandomizer", Name: "CharacterRandomizer", Version: "1.0.0")]
    public class KK_RandomCharacterGenerator : BaseUnityPlugin
    {
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

                MakerAPI.GetCharacterControl().Reload();
            });

            e.AddControl(new MakerSeparator(cat, this));
            ui.randomizeBodySliders = e.AddControl(new MakerToggle(cat, "Randomize body sliders", this));
            ui.randomizeBody = e.AddControl(new MakerToggle(cat, "Randomize body type", this));
            ui.SkinColorRadio = e.AddControl(new MakerRadioButtons(cat, this, "Skin color", ui.skinColorOptions));

            e.AddControl(new MakerSeparator(cat, this));
            ui.randomizeFaceSliders = e.AddControl(new MakerToggle(cat, "Randomize face sliders", this));
            ui.randomizeFaceEyes = e.AddControl(new MakerToggle(cat, "Randomize eyes", this));
            ui.randomizeFaceEtc = e.AddControl(new MakerToggle(cat, "Randomize face type", this));

            e.AddControl(new MakerSeparator(cat, this));
            ui.randomizeHair = e.AddControl(new MakerToggle(cat, "Randomize hair type", this));
            ui.randomizeHairColor = e.AddControl(new MakerToggle(cat, "Randomize hair color", this));

            e.AddControl(new MakerSeparator(cat, this));
            ui.DeviationSlider = e.AddControl(new MakerSlider(cat, "Deviation", 0, 1, 0.1f, this));

            ui.randomizeBodySliders.Value = true;
            ui.randomizeFaceSliders.Value = true;
        }

    }
}
