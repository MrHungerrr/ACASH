using UnityEngine;

public class ActionsCount
{
    public int[] costs { get; private set; }
    public int count { get; set; }


    public ActionsCount(int easy, int normal, int hard)
    {
        costs = new int[3] {easy, normal, hard};
        count = easy + normal + hard;
    }


    public int GetRandomCost()
    {
        int rand = (int) Random.Range(1, costs.Length + 1);

        for(int i = 0; i < costs.Length; i++)
        {
            if (GetCost(rand))
                return rand;
            else
            {
                rand++;
                if (rand > costs.Length)
                    rand = 1;
            }
        }

        return 0;
    }

    private bool GetCost(int cost)
    {
        if (costs[cost - 1] > 0)
        {
            costs[cost - 1]--;
            count--;
            return true;
        }
        else
            return false;
    }
}
