
string[] gDoors = { "A", "B", "C" };
Random random = new Random();

int indexToSkip = random.Next(0,gDoors.Length);
string gWinnigDoor = gDoors[indexToSkip];
string gSelectedDoor = string.Empty;
string gIsSwitched = string.Empty;
string gOpenedDoor = string.Empty;


Console.WriteLine("Winning door: " + gWinnigDoor);

for (int i = 0; i < gDoors.Length; i++)
{
    Console.WriteLine(gDoors[i].ToUpper());
}

Console.WriteLine("Pick a door");
gSelectedDoor = Console.ReadLine() ?? "A";
Console.WriteLine("Selected door: " + gSelectedDoor.ToUpper());



if (gSelectedDoor.ToUpper() == gWinnigDoor.ToUpper())
{
    // Calculate the size of the new array (original size - 1)
    int newSize = gDoors.Length - 1;

    // Create a new array with the calculated size
    string[] newArray = new string[newSize];

    // Copy elements from the original array to the new array, skipping the specified index
    for (int i = 0, j = 0; i < gDoors.Length; i++)
    {
        if (i != indexToSkip)
        {
            newArray[j] = gDoors[i];
            j++;
        }
    }

    gOpenedDoor = newArray[random.Next(0,newArray.Length)];
    Console.WriteLine("Opened door: " + gOpenedDoor);
}
else
{
    for (int i = 0; i < gDoors.Length; i++)
    {
        string tmpDoor = gDoors[i].ToUpper();

        if (tmpDoor != gSelectedDoor.ToUpper() && tmpDoor != gWinnigDoor.ToUpper())
        {
            gOpenedDoor = tmpDoor;
            Console.WriteLine("Opened door: " + gOpenedDoor);
            break;
        }
    }
}

//for (int i = 0; i < gDoors.Length; i++)
//{
//    string tmpDoor = gDoors[i].ToUpper();

//    if (tmpDoor != gSelectedDoor.ToUpper() && tmpDoor != gWinnigDoor.ToUpper())
//    {
//        gOpenedDoor = tmpDoor;
//        Console.WriteLine("Opened door: " + gOpenedDoor);
//        break;
//    }
//}

Console.WriteLine("Do you wanna switch..? (Y/N)");
gIsSwitched = Console.ReadLine() ?? "N";

if (gIsSwitched.ToUpper() is "Y")
{
    for (int i = 0; i < gDoors.Length; i++)
    {
        string tmpDoor = gDoors[i].ToUpper();
        if (tmpDoor != gSelectedDoor.ToUpper() && tmpDoor != gOpenedDoor.ToUpper())
        {
            gSelectedDoor = tmpDoor;
            Console.WriteLine("Secondly Selected door: " + gSelectedDoor);
            break;
        }
    }
}

if (gSelectedDoor.ToUpper() == gWinnigDoor.ToUpper()) Console.WriteLine("You win..!");
else Console.WriteLine("You loose..!");

Console.WriteLine("Winning door: " + gWinnigDoor.ToUpper());

