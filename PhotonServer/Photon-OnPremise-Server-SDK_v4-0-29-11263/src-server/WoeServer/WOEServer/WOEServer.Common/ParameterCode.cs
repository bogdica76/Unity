namespace WOEServer.Common
{
    public enum ParameterCodes:byte
    {
        SubOperationCode = 0,
        //login operation params
        User,
        Pass,
        ChatMessage,
        PosX,
        PosY,
        PosZ,
        ListPlayers,
    }
}
