namespace ClientV1.Extensions
{
    public static class ByteArrayExtensions
    {
        public static int GetStartIndex(this byte[] arr, string str)
        {
            byte[] strB = new byte[str.Length];
            for (int i = 0; i < str.Length; i++)
                strB[i] = (byte)str[i];

            int index = -1;
            for (int i = 0; i < arr.Length; i++)
            {
                bool match = true;
                for (int j = 0; j < strB.Length; j++)
                {
                    if (i + j > arr.Length)
                        break;
                    if (arr[i + j] == strB[j])
                    {
                        if (index == -1)
                            index = i;
                        continue;
                    }
                    match = false;
                    index = -1;
                    break;
                }
                if (match)
                    return index;
            }
            return -1;
        }
    }
}
