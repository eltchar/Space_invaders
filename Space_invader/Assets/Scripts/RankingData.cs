
[System.Serializable]
public class RankingData
{
    public int[] scoreRanking = new int[10];
    public int gamesCount;

    public RankingData(int[] arr, int count)
    {
        if (arr.Length<10)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                scoreRanking[i] = arr[i];
            }
        }
        gamesCount = count;
        
    }

}
