public class ScoreItem
{
    public int count;
    public int rep;
    public int kind;
    private int rep_coef;
    private int kind_coef;

    public ScoreItem(int reputation_coef, int kindness_coef)
    {
        rep_coef = reputation_coef;
        kind_coef = kindness_coef;
        Zeroing();
    }

    public ScoreItem(ScoreItem item)
    {
        rep_coef = item.rep_coef;
        kind_coef = item.kind_coef;
        count = item.count;
        rep = item.rep;
        kind = item.rep;
    }

    public void Plus()
    {
        ChangeCount(count + 1);
    }

    public void Minus()
    {
        ChangeCount(count - 1);
    }

    public void Zeroing()
    {
        SetCount(0);
    }

    public void ChangeCount(int value)
    {
        int buf_rep = (value - count) * rep_coef;

        SetCount(value);

        HUDManager.get.ChangeReputationHUD(buf_rep);
    }

    public void SetCount(int value)
    {
        count = value;
        rep = value * rep_coef;
        kind = value * kind_coef;
    }



    public static ScoreItem operator ++(ScoreItem item)
    {
        item.Plus();
        return item;
    }

    public static ScoreItem operator --(ScoreItem item)
    {
        item.Minus();
        return item;
    }
}
