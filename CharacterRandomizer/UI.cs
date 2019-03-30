using KKAPI.Maker.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CharacterRandomizer
{
    public class UI
    {
        public MakerSlider DeviationSlider;

        public MakerRadioButtons SkinColorRadio;
        public string[] skinColorOptions = {
            "White",
            "Brown",
            "Unchanged"
        };

        public MakerToggle randomizeBody;
        public MakerToggle randomizeBodySliders;
        public MakerToggle randomizeFaceEyes;
        public MakerToggle randomizeFaceEtc;
        public MakerToggle randomizeFaceSliders;
        public MakerToggle randomizeHair;
        public MakerToggle randomizeHairColor;
        public MakerToggle randomizePersonality;
    }
}
