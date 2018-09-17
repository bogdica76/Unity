namespace WOEServer.Common
{
    public enum OperationCodes:byte
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
