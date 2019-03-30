using KKAPI.Maker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CharacterRandomizer
{
    public class Randomizer
    {
        UI ui;

        public Randomizer(UI ui) {
            this.ui = ui;
        }

        public readonly System.Random Rand = new System.Random();

        public ChaFileControl Chararacter => MakerAPI.GetCharacterControl().chaFile;
        public ChaFileCustom Custom => Chararacter.custom;

        public float RandomFloatDeviation(float mean, float deviation)
        {
            double x1 = 1 - Rand.NextDouble();
            double x2 = 1 - Rand.NextDouble();
            double y1 = Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2);
            return (float)(y1 * deviation + mean);
        }

        public double RandomDouble(double minimum, double maximum) => Rand.NextDouble() * (maximum - minimum) + minimum;
        public float RandomFloat(double minimum, double maximum) => (float)RandomDouble(minimum, maximum);
        public float RandomFloat() => (float)Rand.NextDouble();
        public Color RandomColor() => new Color(RandomFloat(), RandomFloat(), RandomFloat());
        public bool RandomBool(int percentChance = 50) => Rand.Next(100) < percentChance;

        public void RandomSkinColor(Color currentColor, out float h, out float s, out float v)
        {
            switch (ui.SkinColorRadio.Value)
            {
                case 0:
                    h = 0.06f;
                    RandomPointInTriangle(0.02f, 1f, 0.1f, 0.91f, 0.11f, 1f, out s, out v);
                    break;
                case 1:
                    h = 0.06f;
                    s = RandomFloat(0.13, 0.39);
                    v = RandomFloat(0.66, 0.98);
                    break;
                default:
                case 2:
                    Color.RGBToHSV(currentColor, out h, out s, out v);
                    break;
            }
        }

        public List<float> RandomizeSliders(List<float> list)
        {
            List<float> res = new List<float>(list);
            float dev = ui.DeviationSlider.Value;

            for (int i = 0; i < list.Count; i++)
            {
                float v = RandomFloatDeviation(res[i], dev);
                if (v < -1) v = -1;
                if (v > 2) v = 2;

                res[i] = v;
            }

            return res;
        }


        public void RandomPointInTriangle(float x1, float y1, float x2, float y2, float x3, float y3, out float x, out float y)
        {
            float r1 = (float)Rand.NextDouble();
            float r2 = (float)Rand.NextDouble();
            float sqr1 = (float)Math.Sqrt(r1);

            x = (1 - sqr1) * x1 + sqr1 * (1 - r2) * x2 + sqr1 * r2 * x3;
            y = (1 - sqr1) * y1 + sqr1 * (1 - r2) * y2 + sqr1 * r2 * y3;
        }
    }
}
