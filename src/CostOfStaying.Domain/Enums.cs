namespace CostOfStaying.Domain
{
    /// <summary>
    /// Player lifecycle states. Per 02_GAME_RULES_SPECIFICATION.md section 6:
    /// Connecting -> Alive -> Dead/Spirit. Spectating and Disconnected are possible
    /// states whose start conditions are not yet defined. Death moves the player
    /// to Spirit first; it does not eject them from the match.
    /// </summary>
    public enum PlayerState
    {
        Connecting,
        Alive,
        Dead,
        Spirit,
        Disconnected,
        Spectating
    }

    /// <summary>
    /// Match lifecycle states. Per 02_GAME_RULES_SPECIFICATION.md section 6:
    /// Created -> Waiting -> Starting -> Running -> VoteOpen -> Running or Ending -> Completed.
    /// </summary>
    public enum MatchState
    {
        Created,
        Waiting,
        Starting,
        Running,
        VoteOpen,
        Ending,
        Completed
    }

    /// <summary>
    /// The five distinguishable outcomes of RULE-05. Draw is distinct from
    /// Time Limit (which has a winner) and from Mutual Collapse (all dead).
    /// </summary>
    public enum MatchOutcome
    {
        None,
        LastSurvivor,
        CollectiveSurrender,
        TimeLimit,
        Draw,
        MutualCollapse
    }

    /// <summary>Vote choice. Abstain is recorded but counts as No for unanimity (RULE-03).</summary>
    public enum VoteChoice
    {
        Abstain,
        Yes,
        No
    }
}
