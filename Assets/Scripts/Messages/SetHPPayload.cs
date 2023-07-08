public class SetHPPayload
{
    public int HP { get; private set; }
    public bool Player2 { get; private set; }
    public SetHPPayload(int hp,bool player2 = false)
    {
        HP = hp;
        Player2 = player2;
    }
}
