public class Top3Cities
{
    public string firstPlaceName = "Not Enough cities";
    public int firstPlaceEarnings;
    public string secondPlaceName = "Not Enough cities";
    public int secondPlaceEarnings;
    public string thirdPlaceName = "Not Enough cities";
    public int thirdPlaceEarnings;

    public void CheckIfTop3(string cityName, int earnings)
    {
        if (earnings > firstPlaceEarnings)
        {
            SwitchSecondPlaceToThird();
            SwitchFirstPlaceToSecond();
            SetAsFirstPlace(cityName, earnings);
        } else if (earnings > secondPlaceEarnings)
        {
            SwitchSecondPlaceToThird();
            SetAsSecondPlace(cityName, earnings);
        } else if (earnings > thirdPlaceEarnings)
        {
            SetAsThirdPlace(cityName, earnings);
        }
        
    }

    private void SetAsFirstPlace(string cityName, int earnings)
    {
        firstPlaceName = cityName;
        firstPlaceEarnings = earnings;
    }

    private void SetAsSecondPlace(string cityName, int earnings)
    {
        secondPlaceName = cityName;
        secondPlaceEarnings = earnings;
    }

    private void SetAsThirdPlace(string cityName, int earnings)
    {
        thirdPlaceName = cityName;
        thirdPlaceEarnings = earnings;
    }

    private void SwitchFirstPlaceToSecond()
    {
        secondPlaceName = firstPlaceName;
        secondPlaceEarnings = firstPlaceEarnings;
    }

    private void SwitchSecondPlaceToThird()
    {
        thirdPlaceName = secondPlaceName;
        thirdPlaceEarnings = secondPlaceEarnings;
    }
}