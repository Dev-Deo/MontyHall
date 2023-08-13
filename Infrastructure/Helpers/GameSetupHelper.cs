namespace Infrastructure.Helpers
{
    public static class GameSetupHelper
    {
        public static string GetDoorValue(string[] cInDoors, out string[] cOutDoors)
        {
            Random random = new Random();
            int indexToSkip = random.Next(0, cInDoors.Length);
            string val = cInDoors[indexToSkip];
            int newArraySize = cInDoors.Length - 1;

            string[] newDoorsArry = new string[newArraySize];

            for (int i = 0, j = 0; i < cInDoors.Length; i++)
            {
                if (i != indexToSkip)
                {
                    newDoorsArry[j] = cInDoors[i];
                    j++;
                }
            }
            cOutDoors = newDoorsArry;
            return val;
        }

        public static int GetOpenDoorIndex(string[] gDoors, int winningDoorIndex, int selectedDoorIndex)
        {
            int tmpDoorToOpen = 0;
            if (selectedDoorIndex == winningDoorIndex)
            {
                //Remove winning door from array
                GetDoorNo(winningDoorIndex, gDoors, out gDoors);
                Random random = new Random();
                tmpDoorToOpen = random.Next(0, gDoors.Length);
            }
            else
            {
                string tmpSelectedDoor = gDoors[selectedDoorIndex];
                string tmpWinningDoor = gDoors[winningDoorIndex];

                for (int i = 0; i < gDoors.Length; i++)
                {
                    string tmpDoor = gDoors[i];
                    if (tmpDoor != tmpSelectedDoor && tmpDoor != tmpWinningDoor)
                    {
                        tmpDoorToOpen = i;
                        break;
                    }
                }
            }

            return tmpDoorToOpen + 1;
        }



        private static void GetDoorNo(int cIndexToSkip, string[] cInDoors, out string[] cOutDoors)
        {
            int newArraySize = cInDoors.Length - 1;
            string[] newDoorsArry = new string[newArraySize];

            for (int i = 0, j = 0; i < cInDoors.Length; i++)
            {
                if (i != cIndexToSkip)
                {
                    newDoorsArry[j] = cInDoors[i];
                    j++;
                }
            }
            cOutDoors = newDoorsArry;
        }



    }
}
