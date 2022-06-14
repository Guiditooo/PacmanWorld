namespace GeneralFunctions
{
    public class InputFunctions
    {
        public static void ClampValues(ref string text, int maxValue, int minValue/*0 = no min*/)
        {
            int aux = 0;

            if (int.TryParse(text, out aux))
            {
                if (aux <= minValue)
                {
                    text = minValue.ToString();
                }
                else if (aux >= maxValue)
                {
                    text = maxValue.ToString();
                }
            }
        }

        public static void ClampCharacterCount(ref string text, int maxCharacterCount)
        {
            if(text.Length > maxCharacterCount)
            {
                string newText = "";
                for (int i = 0; i <= maxCharacterCount; i++)
                {
                    newText += text[i];
                }
                text = newText;
            }
        }

        public static void ClampCharacterCount(ref string text, int maxCharacterCount, int maxValue, int minValue)
        {
            if (text.Length > maxCharacterCount)
            {
                string newText = "";
                for (int i = 0; i <= maxCharacterCount; i++)
                {
                    newText += text[i];
                }
                text = newText;
            }
            ClampValues(ref text, maxValue, minValue);
        }

    }
}