public class ScoreItem
{
    public int Count => _count;
    public int Rep => _rep;
    public int Kind => _kind;

    private int _count;
    private int _rep;
    private int _kind;

    private readonly int REP_COEF;
    private readonly int KIND_COEF;

    public ScoreItem(int reputation_coef, int kindness_coef)
    {
        REP_COEF = reputation_coef;
        KIND_COEF = kindness_coef;
        Zeroing();
    }

    public ScoreItem(ScoreItem item)
    {
        REP_COEF = item.REP_COEF;
        KIND_COEF = item.KIND_COEF;
        _count = item.Count;
        _rep = item.Rep;
        _kind = item.Kind;
    }

    public void Plus()
    {
        ChangeCount(Count + 1);
    }

    public void Minus()
    {
        ChangeCount(Count - 1);
    }

    public void Zeroing()
    {
        SetCount(0);
    }

    public void ChangeCount(int value)
    {
        int buf_rep = (value - Count) * REP_COEF;

        SetCount(value);
    }

    public void SetCount(int value)
    {
        _count = value;
        _rep = value * REP_COEF;
        _kind = value * KIND_COEF;
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
