using UnityEngine;

public class ActionsCount
{
    public int[] costs { get; private set; }
    public int count { get; private set; }


    public ActionsCount(int easy, int normal, int hard)
    {
        costs = new int[3] {easy, normal, hard};
        count = easy + normal + hard;
    }

    public ActionsCount(ActionsCount actions_count)
    {
        costs = new int[3] { actions_count.costs[0], actions_count.costs[1], actions_count.costs[2] };
        count = actions_count.costs[0] + actions_count.costs[1] + actions_count.costs[2];
    }


    public int GetRandomCost()
    {
        int rand = (int) Random.Range(1, costs.Length + 1);

        for(int i = 0; i < costs.Length; i++)
        {
            if (IsCostExist(rand))
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

    private bool IsCostExist(int cost)
    {
        try
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
        catch
        {
            Debug.LogError("Не правильная стоимость");
            return false;
        }
    }
}
