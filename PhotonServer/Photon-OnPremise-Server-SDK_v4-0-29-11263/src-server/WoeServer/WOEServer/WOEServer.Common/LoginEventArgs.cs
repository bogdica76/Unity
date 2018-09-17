using System;

namespace WOEServer.Common
{
    public class LoginEventArgs:EventArgs
    {
        public ErrorCodes Error { get; private set; }

        public LoginEventArgs(ErrorCodes error)
        {
            Error = error;
        }
    }
}
