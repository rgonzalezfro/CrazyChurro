public class SetHPPayload
{
    public int HP { get; private set; }
    public Player Id { get; private set; }
    public SetHPPayload(int hp, Player playerId)
    {
        HP = hp;
        Id = playerId;
    }
}
