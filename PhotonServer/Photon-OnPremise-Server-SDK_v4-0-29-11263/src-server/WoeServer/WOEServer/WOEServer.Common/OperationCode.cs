namespace WOEServer.Common
{
    public enum OperationCode:byte
    {
        Login,
        GetRecentChatMessages,
        SendChatMessage,
        Move,
        WorldEnter,
        WorldExit,
        ListPlayers,
    }
}
